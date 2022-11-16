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
    public class CategoryQuery : ICategoryQuery
    {
        public readonly IUnitOfWork _unitOfWork;

        public CategoryQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private string GetAllSql = $@"SELECT * FROM CATEGORIES";
        private string GetByIdSql = $@"SELECT * FROM CATEGORIES WHERE Id=@id";

        public async Task<IEnumerable<Category>> GetAll()
        {
            try
            {
                var result = await _unitOfWork.GetConnection().QueryAsync<Category>(GetAllSql, null, _unitOfWork.GetTransaction());
                return result;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Category> GetById(int id)
        {
            var param = new { id };
            try
            {
               var result= await _unitOfWork.GetConnection().QueryFirstOrDefaultAsync<Category>(GetByIdSql, param, _unitOfWork.GetTransaction());
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
