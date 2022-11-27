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

namespace TurboAz.Repository.CQRS.Queries.Concrete
{
    public class CategoryQuery : ICategoryQuery
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly IUnitOfWorkAdoNet _unitOfWorkAdoNet;


        public CategoryQuery(IUnitOfWork unitOfWork, IUnitOfWorkAdoNet unitOfWorkAdoNet)
        {
            _unitOfWork = unitOfWork;
            _unitOfWorkAdoNet = unitOfWorkAdoNet;
        }

        private string GetAllSql = $@"SELECT * FROM CATEGORIES";
        private string GetByIdSql = $@"SELECT * FROM CATEGORIES WHERE Id=@id";

        private string GetAllPagingSql = $@"SELECT C.[Name]
                                            FROM CATEGORIES AS C
                                            ORDER BY C.[Name]
                                            OFFSET @Offset ROWS
                                            FETCH NEXT @Limit ROWS ONLY";

        public async Task<IEnumerable<Category>> GetAll()
        {
            var conn = _unitOfWorkAdoNet.OpenConnection();
            SqlDataReader reader = null;

            try
            {
                #region Dapper
                //var result = await _unitOfWork.GetConnection().QueryAsync<Category>(GetAllSql, null, _unitOfWork.GetTransaction());
                //return result;
                #endregion

                #region Ado.Net
                SqlCommand command = new SqlCommand(GetAllSql, conn);
                reader = await command.ExecuteReaderAsync();
                var categoryList = reader.Parse<Category>();
                categoryList = categoryList.Cast<Category>().ToList();

                return categoryList;
                #endregion
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Category> GetById(int id)
        {

            var conn = _unitOfWorkAdoNet.OpenConnection();
            SqlDataReader reader = null;
            var param = new { id };
            try
            {
                #region Dapper
                //var result = await _unitOfWork.GetConnection().QueryFirstOrDefaultAsync<Category>(GetByIdSql, param, _unitOfWork.GetTransaction());
                //return result;
                #endregion

                #region AdoNet
                SqlCommand command = new SqlCommand(GetByIdSql, conn);
                var paramId = new SqlParameter();
                paramId.ParameterName = "@id";
                paramId.SqlDbType = SqlDbType.Int;
                paramId.Value = id;

                command.Parameters.Add(paramId);

                reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    id = reader.GetInt32(0);
                    string categoryName = reader.GetString(1);
                    var newCategory = new Category { Id = id, Name = categoryName };
                    return newCategory;
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

        public async Task<IEnumerable<Category>> GetAllPaging(PagingModel model)
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

                var data = await _unitOfWork.GetConnection().QueryAsync<Category>(GetAllPagingSql, param, _unitOfWork.GetTransaction());
                return data;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
