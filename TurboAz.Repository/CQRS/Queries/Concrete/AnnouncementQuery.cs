using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboAz.Core.Models;
using TurboAz.Core.RequestsModels;
using TurboAz.Core.ResponseModels;
using TurboAz.Repository.CQRS.Queries.Abstract;

namespace TurboAz.Repository.CQRS.Queries.Concrete
{
    public class AnnouncementQuery: IAnnouncementQuery
    {
        private readonly IUnitOfWork _unitOfWork;

        public AnnouncementQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        private string _filteredSql = $@"SELECT A.*,C.Model CarName,CT.Name CategoryName,CI.Name CityName from Announcments A
                                            LEFT JOIN Cars C ON C.Id = A.CarId
                                            LEFT JOIN Categories CT ON CT.Id = A.CategoryId
                                            LEFT JOIN Cities CI ON CI.Id = A.CityId
                                         WHERE ";

        //public IEnumerable<Announcement> Filtered(GetFilteredDataRequestModel get)
        //{
        //    var announcementList = _unitOfWork.DeserializeFromJson<Announcement>();

      
       

        // 
        //    if (get.StartDate is not null)
        //    {
        //        announcementList = announcementList.Where(i => i.CreatedDate == get.StartDate).ToList();
        //    }
        //    if (get.EndDate is not null)
        //    {
        //        announcementList = announcementList.Where(i => i.AnnouncementDeadline == get.EndDate).ToList();
        //    }
        //    return announcementList;
        //}

        //public async Task<IEnumerable<Announcement>> Filtered(GetFilteredDataRequestModel get)
        //{
        //    throw new NotImplementedException();
        //}




        private string GetByIdSql = "SELECT * FROM ANNOUNCMENTS WHERE Id = @id";

        private string GetAllSql = "SELECT * FROM ANNOUNCMENTS";


        public async Task<IEnumerable<Announcement>> GetAll()
        {
            try
            {
                var result = await _unitOfWork.GetConnection().QueryAsync<Announcement>(GetAllSql, null, _unitOfWork.GetTransaction());
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Announcement> GetById(int id)
        {
            try
            {
                var param = new { id };
                var result = await _unitOfWork.GetConnection().QueryFirstOrDefaultAsync<Announcement>(GetByIdSql, param,_unitOfWork.GetTransaction());
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<AnnoncementResponseModel>> Filtered(GetFilteredDataRequestModel model)
        {
            
            if(model.IsVip is true)
            {
                _filteredSql += " A.IsVip = 1 AND ";
            }
            else
            {
                _filteredSql += " A.IsVip = 0 AND ";
            }

            if (!String.IsNullOrWhiteSpace(model.Model))
            {
                _filteredSql += $" C.Model LIKE '%{model.Model}%'  AND ";
            }

            if(model.IsActive is true)
            {
                _filteredSql += "A.IsActive = 1 AND ";
            }
            else
            {
                _filteredSql += "A.IsActive = 0 AND ";
            }

            if (model.CategoryId != null)
            {
                _filteredSql += $"CT.Id LIKE '%{model.CategoryId}%' AND ";
            }

            if (!String.IsNullOrWhiteSpace(model.Color))
            {
                _filteredSql += $"C.Color LIKE '%{model.Model}%' AND ";
            }

            if (model.MinPrice != 0)
            {
                _filteredSql += $"C.Price > {model.MinPrice} AND ";
            }

            if(model.MaxPrice != 0)
            {
                _filteredSql += $"C.Price < {model.MaxPrice} AND";
            }

            if(model.Year!=0 && model.Year > 1900 && model.Year <= 2024)
            {
                _filteredSql += $" C.Year LIKE '%{model.Year}%' AND";
            }


            try
            {
                var result = await _unitOfWork.GetConnection().QueryAsync<AnnoncementResponseModel>(_filteredSql, null, _unitOfWork.GetTransaction());
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
