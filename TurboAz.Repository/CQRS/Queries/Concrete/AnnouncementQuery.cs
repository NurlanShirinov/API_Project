using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboAz.Core.Models;
using TurboAz.Core.RequestsModels;
using TurboAz.Core.ResponseModels;
using TurboAz.Repository.CQRS.Queries.Abstract;
using TurboAz.Repository.Infrustructure;

namespace TurboAz.Repository.CQRS.Queries.Concrete
{
    public class AnnouncementQuery : IAnnouncementQuery
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUnitOfWorkAdoNet _unitOfWorkAdoNet;


        public AnnouncementQuery(IUnitOfWork unitOfWork, IUnitOfWorkAdoNet unitOfWorkAdoNet)
        {
            _unitOfWork = unitOfWork;
            _unitOfWorkAdoNet = unitOfWorkAdoNet;
        }

        private string _filteredSql = $@"SELECT A.*,C.Model CarName,CT.Name CategoryName,CI.Name CityName from Announcments A
                                            LEFT JOIN Cars C ON C.Id = A.CarId
                                            LEFT JOIN Categories CT ON CT.Id = A.CategoryId
                                            LEFT JOIN Cities CI ON CI.Id = A.CityId
                                         WHERE IsActive = 1 AND ";


        private string GetByIdSql = "SELECT * FROM ANNOUNCMENTS WHERE Id = @id";

        private string GetAllSql = "SELECT * FROM ANNOUNCMENTS";

        private string GetAllPaginingSql = $@"SELECT  A.[CreatedDate], A.[Price], A.[ViewCount],A.[IsActive],A.[IsVip],A.[Expired],A.[Id],A.[CarId],A.[CityId]
                                           FROM ANNOUNCMENTS AS A
                                           ORDER BY A.[CreatedDate]
                                           OFFSET @Offset ROWS
                                           FETCH NEXT @Limit ROWS ONLY";


        public async Task<IEnumerable<Announcement>> GetAll()
        {
            var conn = _unitOfWorkAdoNet.OpenConnection();
            SqlDataReader reader = null;
            try
            {
                #region Dapper
                //var result = await _unitOfWork.GetConnection().QueryAsync<Announcement>(GetAllSql, null, _unitOfWork.GetTransaction());
                //return result;
                #endregion

                #region Ado.Net
                SqlCommand command = new SqlCommand(GetAllSql, conn);
                reader = await command.ExecuteReaderAsync();
                var announcementList = reader.Parse<Announcement>();
                announcementList = announcementList.Cast<Announcement>().ToList();
                return announcementList;
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Announcement> GetById(int id)
        {
            var conn = _unitOfWorkAdoNet.OpenConnection();
            SqlDataReader reader = null;
            var param = new { id };
            try
            {
                #region Dapper
                var result = await _unitOfWork.GetConnection().QueryFirstOrDefaultAsync<Announcement>(GetByIdSql, param, _unitOfWork.GetTransaction());
                return result;
                #endregion

                #region Ado.Net
                SqlCommand command = new SqlCommand(GetByIdSql, conn);

                var paramId = new SqlParameter();
                paramId.ParameterName = "@id";
                paramId.SqlDbType = SqlDbType.Int;
                paramId.Value = id;

                command.Parameters.Add(paramId);

                reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    id = reader.GetInt32(0);
                    var newAnnouncement = new Announcement { Id = id };
                    return newAnnouncement;
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<AnnoncementResponseModel>> Filtered(GetFilteredDataRequestModel model)
        {

            if (model.IsVip is true)
            {
                _filteredSql += " A.IsVip = 1 AND ";
            }




            if (!String.IsNullOrWhiteSpace(model.Model))
            {
                _filteredSql += $" C.Model LIKE '%{model.Model}%'  AND ";
            }


            if (model.CategoryId != null)
            {
                _filteredSql += $" CT.Id = {model.CategoryId} AND ";
            }

            if (!String.IsNullOrWhiteSpace(model.Color))
            {
                _filteredSql += $" C.Color LIKE '%{model.Color}%' AND ";
            }

            if (model.MinPrice != 0)
            {
                _filteredSql += $" C.Price > {model.MinPrice} AND ";
            }

            if (model.MaxPrice != 0)
            {
                _filteredSql += $" C.Price < {model.MaxPrice} AND ";
            }

            if (model.Year != 0 && model.Year > 1900 && model.Year <= 2024)
            {
                _filteredSql += $" C.Year = {model.Year} AND ";
            }

            if (model.StartDate is not null)
            {
                _filteredSql += $" A.CreatedDate > '{model.StartDate}' AND ";
            }
            if (model.EndDate is not null)
            {
                _filteredSql += $" A.CreatedDate < '{model.EndDate}' AND ";
            }

            if (!String.IsNullOrWhiteSpace(model.CategoryName))
            {
                _filteredSql += $" CT.Name LIKE '%{model.CategoryName}%' AND";
            }

            if (!String.IsNullOrWhiteSpace(model.Vendor))
            {
                _filteredSql += $" C.Vendor LIKE '%{model.Vendor}%' AND";
            }


            _filteredSql = _filteredSql += " 1=1";

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

        public async Task<IEnumerable<Announcement>> GetAllPagining(PagingModel model)
        {
            int offset = (model.RowOfPage - 1) * model.PageNumber;
            int limit = model.PageNumber;

            try
            {
                var param = new
                {
                    offset,
                    limit
                };

                var data = await _unitOfWork.GetConnection().QueryAsync<Announcement>(GetAllPaginingSql, param, _unitOfWork.GetTransaction());
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
