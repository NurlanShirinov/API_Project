using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboAz.Core.Models;
using TurboAz.Repository.CQRS.Commands.Abstract;
using TurboAz.Repository.CQRS.Queries.Abstract;
using TurboAz.Repository.Repositories.Abstract;

namespace TurboAz.Repository.Repositories.Concrete
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ICategoryQuery _categoryQuery;
        private readonly ICategoryCommand _categoryCommand;

        public CategoryRepository(ICategoryQuery categoryQuery, ICategoryCommand categoryCommand)
        {
            _categoryQuery = categoryQuery;
            _categoryCommand = categoryCommand;
        }

        public async Task<int> AddCategory(Category category)
        {
            var result = await _categoryCommand.AddCategory(category);
            return result;
        }

        public async Task<bool> DeleteCategory(int id)
        {
            var result = await _categoryCommand.DeleteCategory(id);
            return result;
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            var result = await _categoryQuery.GetAll();
            return result;
        }

        public async Task<Category> GetById(int id)
        {
            var result = await _categoryQuery.GetById(id);
            return result;

        }
        public async Task<Category> UpdateCategory(Category category)
        {
            var result = await _categoryCommand.UpdateCategory(category);
            return result;
        }
    }
}
