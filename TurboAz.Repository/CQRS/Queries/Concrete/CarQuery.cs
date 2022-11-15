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
    public class CarQuery : ICarQuery
    {
        private readonly IUnitOfWork _unitOfWork;

        public CarQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private string _sqlGetAll = $@"SELECT * FROM CARS";

        private string _sqlGetById = $@"SELECT * FROM CARS WHERE Id=@id";

        public async Task<IEnumerable<Car>> GetAll()
        {
            try
            {
                var result = await _unitOfWork.GetConnection().QueryAsync<Car>(_sqlGetAll, null, _unitOfWork.GetTransaction());
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

       public async Task<Car> GetById(int id)
        {
            var param = new { id };
            try
            {
                var result = await _unitOfWork.GetConnection().QueryFirstOrDefaultAsync<Car>(_sqlGetById, param, _unitOfWork.GetTransaction());
                return result;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
