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
    public class CategoryCommand : ICategoryCommand
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryCommand(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private string _sqlAdd = $@"INSERT INTO CATEGORIES ([Id],[Name])
                                   VALUES(@{nameof(Category.Id)},
                                          @{nameof(Category.Name)})";



        private string _sqlDelete = $@"DELETE FROM CATEGORIES WHERE Id=@id";

        private string _sqlUpdate = $@"UPDATE CATEGORIES
                                       SET Name = @name
                                       WHERE Id=@id";

 
       public async Task<int> AddCategory(Category category)
        {
            try
            {
                var result = await _unitOfWork.GetConnection().QueryFirstOrDefaultAsync<int>(_sqlAdd, category, _unitOfWork.GetTransaction());
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteCategory(int id)
        {

            try
            {
                var param = new { id };
                await _unitOfWork.GetConnection().QueryAsync(_sqlDelete, param, _unitOfWork.GetTransaction());
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }
        public async Task<Category> UpdateCategory(Category category)
        {
            try
            {
                await _unitOfWork.GetConnection().QueryAsync(_sqlUpdate, category, _unitOfWork.GetTransaction());
                return category;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
