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
    public class CategoryCommand : ICategoryCommand
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUnitOfWorkAdoNet _unitOfWorkAdoNet;

        public CategoryCommand(IUnitOfWork unitOfWork, IUnitOfWorkAdoNet unitOfWorkAdoNet)
        {
            _unitOfWork = unitOfWork;
            _unitOfWorkAdoNet = unitOfWorkAdoNet;
        }

        private string _sqlAdd = $@"INSERT INTO CATEGORIES ([Name])
                                   VALUES(@{nameof(Category.Name)})";



        private string _sqlDelete = $@"DELETE FROM CATEGORIES WHERE Id=@id";

        private string _sqlUpdate = $@"UPDATE CATEGORIES
                                       SET Name = @name
                                       WHERE Id=@id";


        public async Task<int> AddCategory(Category category)
        {
            var conn = _unitOfWorkAdoNet.GetConnection();
            try
            {
                #region Dapper
                //var result = await _unitOfWork.GetConnection().QueryFirstOrDefaultAsync<int>(_sqlAdd, category, _unitOfWork.GetTransaction());
                //return result;
                #endregion

                #region AdoNet
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlCommand command = new SqlCommand(_sqlAdd, conn);
                var paramName = new SqlParameter();
                paramName.ParameterName = $"@{nameof(Category.Name)}";
                paramName.SqlDbType = SqlDbType.NVarChar;
                paramName.Value = category.Name;
                command.Parameters.Add(paramName);
                var result = command.ExecuteNonQuery();
                return result;
                #endregion
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteCategory(int id)
        {
            var conn = _unitOfWorkAdoNet.GetConnection();
            try
            {
                #region Dapper
                //var param = new { id };
                //await _unitOfWork.GetConnection().QueryAsync(_sqlDelete, param, _unitOfWork.GetTransaction());
                //return true;
                #endregion

                #region AdoNet
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlCommand command = new SqlCommand(_sqlDelete, conn);
                var param = new SqlParameter();
                param.ParameterName = "@id";
                param.SqlDbType = SqlDbType.Int;
                param.Value = id;
                command.Parameters.Add(param);

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
        public async Task<Category> UpdateCategory(Category category)
        {
            var conn = _unitOfWorkAdoNet.GetConnection();
            try
            {
                #region Dapper
                //await _unitOfWork.GetConnection().QueryAsync(_sqlUpdate, category, _unitOfWork.GetTransaction());
                //return category;
                #endregion

                #region Ado.Net
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlCommand command = new SqlCommand(_sqlUpdate, conn);
                var paramId = new SqlParameter();
                paramId.ParameterName = "@id";
                paramId.SqlDbType = SqlDbType.Int;
                paramId.Value = category.Id;
                command.Parameters.Add(paramId);

                var paramName = new SqlParameter();
                paramName.ParameterName = "@name";
                paramName.SqlDbType = SqlDbType.NVarChar;
                paramName.Value = category.Name;
                command.Parameters.Add(paramName);
                command.ExecuteNonQuery();
                return category;


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
