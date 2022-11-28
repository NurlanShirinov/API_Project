using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboAz.Core.Models;
using TurboAz.Repository.CQRS.Commands.Abstract;
using TurboAz.Repository.Infrustructure;
using static Dapper.SqlMapper;


namespace TurboAz.Repository.CQRS.Commands.Concrete
{
    public class CarCommand : ICarCommand
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUnitOfWorkAdoNet _unitOfWorkAdoNet;

        public CarCommand(IUnitOfWork unitOfWork, IUnitOfWorkAdoNet unitOfWorkAdoNet)
        {
            _unitOfWork = unitOfWork;
            _unitOfWorkAdoNet = unitOfWorkAdoNet;
        }

        private string _addSql = $@"INSERT INTO CARS (Id,[Model],[Vendor],[Color],[Year],[Price])
                                    VALUES (@{nameof(Car.Id)},
                                            @{nameof(Car.Model)},
                                            @{nameof(Car.Vendor)},
                                            @{nameof(Car.Color)},
                                            @{nameof(Car.Year)},
                                            @{nameof(Car.Price)})";

        private string _deleteSql = $@"DELETE FROM CARS WHERE Id=@id";

        private string _updateSql = $@"UPDATE CARS
                                       SET Model = @model,
                                        Vendor = @vendor,
                                        Color = @color,
                                        Year = @year,
                                        Price = @price
                                       WHERE Id= @id";

        public async Task<int> AddCar(Car car)
        {
            var conn = _unitOfWorkAdoNet.GetConnection();
            try
            {
                #region Dapper
                //var result = await _unitOfWork.GetConnection().QueryFirstOrDefaultAsync<int>(_addSql, car, _unitOfWork.GetTransaction());
                //return result;
                #endregion

                #region AdoNet
                var command = new SqlCommand(_addSql,conn);

                var paramId = new SqlParameter();
                paramId.ParameterName = $"@{nameof(Car.Id)}";
                paramId.SqlDbType = SqlDbType.Int;
                paramId.Value= car.Id;
                command.Parameters.Add(paramId);

                var paramModel = new SqlParameter();
                paramModel.ParameterName= $"@{nameof(Car.Model)}";
                paramModel.SqlDbType = SqlDbType.NVarChar;
                paramModel.Value = car.Model;
                command.Parameters.Add(paramModel);

                var paramVendor = new SqlParameter();
                paramVendor.ParameterName = $"@{nameof(Car.Vendor)}";
                paramVendor.SqlDbType = SqlDbType.NVarChar;

                #endregion
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
                await _unitOfWork.GetConnection().QueryAsync(_deleteSql, param, _unitOfWork.GetTransaction());
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
                await _unitOfWork.GetConnection().QueryAsync(_updateSql, car, _unitOfWork.GetTransaction());
                return car;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
