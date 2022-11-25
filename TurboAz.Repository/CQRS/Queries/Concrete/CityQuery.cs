using Dapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TurboAz.Core.Models;
using TurboAz.Core.RequestsModels;
using TurboAz.Repository.CQRS.Queries.Abstract;
using TurboAz.Repository.Infrustructure;

namespace TurboAz.Repository.CQRS.Queries.Concrete
{
    public class CityQuery : ICityQuery
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IUnitOfWorkAdoNet _unitOfWorkAdoNet;
        public CityQuery(IUnitOfWork unitOfWork, IUnitOfWorkAdoNet unitOfWorkAdoNet)
        {
            _unitOfWork = unitOfWork;
            _unitOfWorkAdoNet = unitOfWorkAdoNet;
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
            var conn = _unitOfWorkAdoNet.OpenConnection();
            SqlDataReader reader = null;

            try
            {
                #region Dapper

                //var data = await _unitOfWork.GetConnection().QueryAsync<City>(getAllSql, null, _unitOfWork.GetTransaction());

                //return data;
                #endregion

                #region Ado.NET
                SqlCommand command = new SqlCommand(getAllSql, conn);

                reader = await command.ExecuteReaderAsync();
                var cityList = reader.Parse<City>();
                cityList = cityList.Cast<City>().ToList();

                return cityList;
                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<City> GetById(int id)
        {
            var conn = _unitOfWorkAdoNet.OpenConnection();
            SqlDataReader reader = null;

            var param = new { id };


            try
            {
                #region Dapper
                //var result = await _unitOfWork.GetConnection().QueryFirstOrDefaultAsync<City>(getByIdSql, param, _unitOfWork.GetTransaction());
                //return result;
                #endregion

                #region Ado.Net
                SqlCommand command = new SqlCommand(getByIdSql, conn);

                var paramId = new SqlParameter();
                paramId.ParameterName = "@id";
                paramId.SqlDbType = SqlDbType.Int;
                paramId.Value = id;

                command.Parameters.Add(paramId);

                reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    id = reader.GetInt32(0);
                    string cityName = reader.GetString(1);
                    var newCity = new City { Id = id, Name = cityName};
                    return newCity;
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
                return null;
            }
            return null;
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
