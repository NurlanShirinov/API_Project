using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboAz.Core.Models;
using TurboAz.Repository.CQRS.Commands.Abstract;
using TurboAz.Repository.CQRS.Queries.Abstract;
using TurboAz.Repository.Repositories.Abstract;

namespace TurboAz.Repository.Repositories.Concrete
{
    public class CategoryRepository:ICategoryRepository
    {
        private readonly ICategoryQuery _categoryQuery;
        private readonly ICategoryCommand _categoryCommand;

        public CategoryRepository(ICategoryQuery categoryQuery, ICategoryCommand categoryCommand)
        {
            _categoryQuery = categoryQuery;
            _categoryCommand = categoryCommand;
        }

        public int AddCategory(Category category)
        {
            var res = _categoryCommand.AddCategory(category);
            return res;
        }

        public bool DeleteCategory(int id)
        {
            var res = _categoryCommand.DeleteCategory(id);
            return res;
        }

        public IEnumerable<Category> GetAll()
        {
            var res = _categoryQuery.GetAll();
            return res;

        }

        public Category GetById(int id)
        {
            var res = _categoryQuery.GetById(id);
            return res;
        }

        public Category UpdateCategory(Category category)
        {
            var res = _categoryCommand.UpdateCategory(category);
            return res;
        }
    }
}
