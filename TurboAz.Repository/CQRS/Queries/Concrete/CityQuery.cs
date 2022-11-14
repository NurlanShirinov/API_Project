using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboAz.Core.Models;
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
    }
}
