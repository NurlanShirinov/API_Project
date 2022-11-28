using Dapper;
using Org.BouncyCastle.Ocsp;
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
    public class CityCommand : ICityCommand
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUnitOfWorkAdoNet _unitOfWorkAdoNet;
        public CityCommand(IUnitOfWork unitOfWork, IUnitOfWorkAdoNet unitOfWorkAdoNet)
        {
            _unitOfWork = unitOfWork;
            _unitOfWorkAdoNet = unitOfWorkAdoNet;
        }

        private string _addSql = $@"INSERT INTO CITIES([Name])
                                    VALUES(
                                           @{nameof(City.Name)})";

        private string _updateSql = $@"UPDATE CITIES 
                                       SET Name = @name
                                       WHERE Id = @id";

        private string _deleteSql = $@"DELETE FROM CITIES WHERE Id = @id";


        public async Task<int> AddCity(City city)
        {
            var conn = _unitOfWorkAdoNet.OpenConnection();

            try
            {
                #region Dapper
                //var result = await _unitOfWork.GetConnection().QueryFirstOrDefaultAsync<int>(_addSql, city, _unitOfWork.GetTransaction());
                //return result;
                #endregion

                #region Ado.Net
                SqlCommand command = new SqlCommand(_addSql, conn);
                var paramName = new SqlParameter();
                paramName.ParameterName = $"@{nameof(City.Name)}";
                paramName.SqlDbType = SqlDbType.NVarChar;
                paramName.Value = city.Name;
                command.Parameters.Add(paramName);
              
                var result = command.ExecuteNonQuery();
                return result;
                #endregion
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> DeleteCity(int id)
        {
            var conn = _unitOfWorkAdoNet.OpenConnection();

            try
            {
                #region Dapper
                //var param = new { id };
                //await _unitOfWork.GetConnection().QueryAsync(_deleteSql, param, _unitOfWork.GetTransaction());
                //return true;
                #endregion

                #region Ado.Net
                SqlCommand command = new SqlCommand(_deleteSql,conn);

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

        public async Task<City> UpdateCity(City city)
        {
            var conn = _unitOfWorkAdoNet.OpenConnection();

            try
            {
                #region Dapper
                //await _unitOfWork.GetConnection().QueryAsync(_updateSql, city, _unitOfWork.GetTransaction());
                //return city;
                #endregion

                #region Ado.Net
                SqlCommand command = new SqlCommand(_updateSql, conn);

                var paramId = new SqlParameter();
                paramId.ParameterName = "@id";
                paramId.SqlDbType = SqlDbType.Int;
                paramId.Value = city.Id;
                command.Parameters.Add(paramId);

                var paramName = new SqlParameter();
                paramName.ParameterName = "@name";
                paramName.SqlDbType = SqlDbType.NVarChar;
                paramName.Value = city.Name;
                command.Parameters.Add(paramName);

                command.ExecuteNonQuery();
                return city;

                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
                return null;
            }
        }
    }
}
