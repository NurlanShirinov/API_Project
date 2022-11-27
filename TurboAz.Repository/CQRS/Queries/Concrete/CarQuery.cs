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
using TurboAz.Repository.CQRS.Queries.Abstract;
using TurboAz.Repository.Infrustructure;
using static System.Net.Mime.MediaTypeNames;

namespace TurboAz.Repository.CQRS.Queries.Concrete
{
    public class CarQuery : ICarQuery
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUnitOfWorkAdoNet _unitOfWorkAdoNet;
        public CarQuery(IUnitOfWork unitOfWork, IUnitOfWorkAdoNet unitOfWorkAdoNet)
        {
            _unitOfWork = unitOfWork;
            _unitOfWorkAdoNet = unitOfWorkAdoNet;
        }

        private string _sqlGetAll = $@"SELECT * FROM CARS";

        private string _sqlGetById = $@"SELECT * FROM CARS WHERE Id=@id";

        private string _sqlGetAllPaging = $@" Select C.[Model],C.[Vendor],C.[Id],C.[Color],C.[Year],C.[Price]
                                              FROM CARS AS C
                                              ORDER BY C.[Model] 
                                              OFFSET @Offset ROWS
                                              FETCH NEXT @Limit ROWS ONLY";


        public async Task<IEnumerable<Car>> GetAll()
        {
            var conn = _unitOfWorkAdoNet.OpenConnection();
            SqlDataReader reader = null;
            try
            {
                #region Dapper
                //var result = await _unitOfWork.GetConnection().QueryAsync<Car>(_sqlGetAll, null, _unitOfWork.GetTransaction());
                //return result;
                #endregion

                #region Ado.Net
                SqlCommand command = new SqlCommand(_sqlGetAll, conn);
                reader = await command.ExecuteReaderAsync();
                var carList = reader.Parse<Car>();
                carList = carList.Cast<Car>().ToList();
                return carList;
                #endregion
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
                var conn = _unitOfWorkAdoNet.OpenConnection();
                SqlDataReader reader = null;

                #region Dapper
                //var result = await _unitOfWork.GetConnection().QueryFirstOrDefaultAsync<Car>(_sqlGetById, param, _unitOfWork.GetTransaction());
                //return result;
                #endregion

                #region AdoNet
                SqlCommand command = new SqlCommand(_sqlGetById, conn);
                var paramId = new SqlParameter();
                paramId.ParameterName = "@id";
                paramId.SqlDbType = SqlDbType.Int;
                paramId.Value = id;
                command.Parameters.Add(paramId);
                reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    id = reader.GetInt32(0);
                    string vendor = reader.GetString(2);
                    string model = reader.GetString(1);
                    int year = reader.GetInt32(4);
                    int price = reader.GetInt32(5);
                    string color = reader.GetString(3);

                    var newCar = new Car { Id = id, Model = model, Vendor =vendor, Color = color, Year=year , Price = price ,};
                    return newCar;
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


        public async Task<IEnumerable<Car>> GetAllPaging(PagingModel model)
        {
            int limit = (model.PageNumber - 1) * model.RowOfPage;
            int offset = model.RowOfPage;

            try
            {
                var param = new
                {
                    limit,
                    offset
                };
                var data = await _unitOfWork.GetConnection().QueryAsync<Car>(_sqlGetAllPaging, param, _unitOfWork.GetTransaction());
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
