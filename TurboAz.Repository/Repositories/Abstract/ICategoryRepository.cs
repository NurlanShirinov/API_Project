using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboAz.Core.Models;

namespace TurboAz.Repository.Repositories.Abstract
{
    public interface ICategoryRepository
    {
        int AddCategory(Category category);
        bool DeleteCategory(int id);
        Category UpdateCategory(Category category);
        Category GetById(int id);
        IEnumerable<Category> GetAll();
    }
}
