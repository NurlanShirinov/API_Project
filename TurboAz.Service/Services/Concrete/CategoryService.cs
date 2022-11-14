using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboAz.Core.Models;
using TurboAz.Repository.Repositories.Abstract;
using TurboAz.Service.Services.Abstract;

namespace TurboAz.Service.Services.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public int AddCategory(Category category)
        {
            var res = _categoryRepository.AddCategory(category);
            return res;
        }
        public bool DeleteCategory(int id)
        {
            var res = _categoryRepository.DeleteCategory(id);
            return res;
        }
        public IEnumerable<Category> GetAll()
        {
            var res = _categoryRepository.GetAll();
            return res;
        }
        public Category GetById(int id)
        {
            var res = _categoryRepository.GetById(id);
            return res;
        }
        public Category UpdateCategory(Category category)
        {
            var res = _categoryRepository.UpdateCategory(category);
            return res;
        }
    }
}
