using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboAz.Core.Models;
using TurboAz.Repository.CQRS.Commands.Abstract;
using static Dapper.SqlMapper;

namespace TurboAz.Repository.CQRS.Commands.Concrete
{
    public class CityCommand : ICityCommand
    {
        private readonly IUnitOfWork _unitOfWork;

        public CityCommand(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private string _addSql = $@"INSERT INTO CITIES(Id,[Name])
                                    VALUES(@{nameof(City.Id)},
                                           @{nameof(City.Name)})";

        private string _updateSql = $@"UPDATE CITIES 
                                       SET Name = @name
                                       WHERE Id = @id";

        private string _deleteSql = $@"DELETE FROM CITIES WHERE Id = @id";

        public async Task<int> AddCity(City city)
        {
            try
            {
                var result = await _unitOfWork.GetConnection().QueryFirstOrDefaultAsync<int>(_addSql, city, _unitOfWork.GetTransaction());
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> DeleteCity(int id)
        {
            try
            {
                var param = new { id };
                await _unitOfWork.GetConnection().QueryAsync(_deleteSql, param, _unitOfWork.GetTransaction());
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                return false;
            }
        }

        public async Task<City> UpdateCity(City city)
        {
            try
            {
                await _unitOfWork.GetConnection().QueryAsync(_updateSql, city, _unitOfWork.GetTransaction());
                return city;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
