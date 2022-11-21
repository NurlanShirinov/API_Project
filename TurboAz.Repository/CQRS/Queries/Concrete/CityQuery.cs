using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboAz.Core.Models;
using TurboAz.Core.RequestsModels;
using TurboAz.Repository.CQRS.Queries.Abstract;

namespace TurboAz.Repository.CQRS.Queries.Concrete
{
    public class CityQuery : ICityQuery
    {

        private readonly IUnitOfWork _unitOfWork;

        public CityQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private string getAllSql = $@"SElECT * FROM CITIES";

        private string getByIdSql = $@"SELECT * FROM CITIES WHERE ID = @id";

        private string getAllPagingSql = $@"SELECT C.[Name]
                                FROM Cities AS C
                                Order by C.[Name]
                                OFFSET @Offset ROWS
                                FETCH NEXT @Limit ROWS ONLY ";

        public async Task<IEnumerable<City>> GetAll()
        {
            try
            {
                var data = await _unitOfWork.GetConnection().QueryAsync<City>(getAllSql, null, _unitOfWork.GetTransaction());

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<City> GetById(int id)
        {
            var param = new
            {
                id
            };
            try
            {
                var result = await _unitOfWork.GetConnection().QueryFirstOrDefaultAsync<City>(getByIdSql, param, _unitOfWork.GetTransaction());
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<City>> GetAllPaging(PagingModel model)
        {

            int offset = (model.PageNumber - 1) * model.RowOfPage;
            int limit = model.RowOfPage;

            try
            {
                var param = new
                {
                    offset,
                    limit
                };

                var data = await _unitOfWork.GetConnection().QueryAsync<City>(getAllPagingSql, param, _unitOfWork.GetTransaction());
                return data;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
