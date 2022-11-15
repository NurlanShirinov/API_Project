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
    public class CarCommand : ICarCommand
    {
        private readonly IUnitOfWork _unitOfWork;

        public CarCommand(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private string _addSql = $@"INSERT INTO CARS (Id,[Name]
                                    VALUES (@{nameof(Car.Id)})
                                            @{nameof(Car.Model)}
                                            @{nameof(Car.Vendor)}
                                            @{nameof(Car.Color)}
                                            @{nameof(Car.Year)}
                                            @{nameof(Car.Price)}";

        private string _deleteSql = $@"DELETE 
                                      FROM CARS 
                                      WHERE Id=@id";

        private string _updateSql = $@"UPDATE CARS
                                       SET Name = @name
                                            Model=@model
                                            Vendor= @vendor
                                            Color = color
                                            Year = year
                                            Price=price
                                       WHER Id= @id";

        public async Task<int> AddCar(Car car)
        {
            try
            {
                var result = await _unitOfWork.GetConnection().QueryFirstOrDefaultAsync<int>(_addSql, car, _unitOfWork.GetTransaction());
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> DeleteCar(int id)
        {
            try
            {
                var param = new { id };
                await _unitOfWork.GetConnection().QueryAsync(_addSql, param, _unitOfWork.GetTransaction());
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
                return false;
            }
        }

        public async Task<Car> UpdateCar(Car car)
        {
            try
            {
                await _unitOfWork.GetConnection().QueryAsync(_updateSql, car,_unitOfWork.GetTransaction());
                return car;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
