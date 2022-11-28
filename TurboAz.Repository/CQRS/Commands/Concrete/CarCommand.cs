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

        private string _addSql = $@"INSERT INTO CARS ([Model],[Vendor],[Color],[Year],[Price])
                                    VALUES (@{nameof(Car.Model)},
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
                var command = new SqlCommand(_addSql, conn);

                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                var paramModel = new SqlParameter();
                paramModel.ParameterName = $"@{nameof(Car.Model)}";
                paramModel.SqlDbType = SqlDbType.NVarChar;
                paramModel.Value = car.Model;
                command.Parameters.Add(paramModel);

                var paramVendor = new SqlParameter();
                paramVendor.ParameterName = $"@{nameof(Car.Vendor)}";
                paramVendor.SqlDbType = SqlDbType.NVarChar;
                paramVendor.Value = car.Vendor;
                command.Parameters.Add(paramVendor);

                var paramColor = new SqlParameter();
                paramColor.ParameterName = $"@{nameof(Car.Color)}";
                paramColor.SqlDbType = SqlDbType.NVarChar;
                paramColor.Value = car.Color;
                command.Parameters.Add(paramColor);

                var paramYear = new SqlParameter();
                paramYear.ParameterName = $"@{nameof(Car.Year)}";
                paramYear.SqlDbType = SqlDbType.Int;
                paramYear.Value = car.Year;
                command.Parameters.Add(paramYear);

                var paramPrice = new SqlParameter();
                paramPrice.ParameterName = $"@{nameof(Car.Price)}";
                paramPrice.SqlDbType = SqlDbType.Int;
                paramPrice.Value = car.Price;
                command.Parameters.Add(paramPrice);

                var result = command.ExecuteNonQuery();
                return result;
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteCar(int id)
        {

            var conn = _unitOfWorkAdoNet.GetConnection();

            try
            {
                #region Dapper
                //var param = new { id };
                //await _unitOfWork.GetConnection().QueryAsync(_deleteSql, param, _unitOfWork.GetTransaction());
                //return true;
                #endregion

                #region AdoNet
                var command = new SqlCommand(_deleteSql, conn);
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                var paramId = new SqlParameter();
                paramId.ParameterName = "@id";
                paramId.SqlDbType = SqlDbType.Int;
                paramId.Value = id;
                command.Parameters.Add(paramId);
                command.ExecuteNonQuery();
                return true;

                #endregion
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
