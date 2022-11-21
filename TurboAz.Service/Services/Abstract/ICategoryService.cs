using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboAz.Core.Models;
using TurboAz.Core.RequestsModels;

namespace TurboAz.Service.Services.Abstract
{
    public interface ICategoryService
    {
        Task<int> AddCategory(Category category);
        Task<bool> DeleteCategory(int id);
        Task<Category> UpdateCategory(Category category);
        Task<Category> GetById(int id);
        Task<IEnumerable<Category>> GetAll();
        Task<IEnumerable<Category>> GetAllPaging(PagingModel model);

    }
}
