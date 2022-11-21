using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboAz.Core.Models;
using TurboAz.Core.RequestsModels;
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

        public async  Task<int> AddCategory(Category category)
        {
            var res = await _categoryRepository.AddCategory(category);
            return res;
        }
        public async  Task<bool> DeleteCategory(int id)
        {
            var res = await _categoryRepository.DeleteCategory(id);
            return res;
        }
        public async Task<IEnumerable<Category>> GetAll()
        {
            var res = await _categoryRepository.GetAll();
            return res;
        }

        public async Task<IEnumerable<Category>> GetAllPaging(PagingModel model)
        {
            var res = await _categoryRepository.GetAllPaging(model);
            return res;
        }

        public async Task<Category> GetById(int id)
        {
            var res = await _categoryRepository.GetById(id);
            return res;
        }
        public async Task<Category> UpdateCategory(Category category)
        { 
            var res = await _categoryRepository.UpdateCategory(category);
            return res;
        }
    }
}
