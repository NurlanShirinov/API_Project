using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboAz.Core.Models;

namespace TurboAz.Repository.CQRS.Commands.Abstract
{
    public interface ICategoryCommand
    {
        int AddCategory(Category category);
        bool DeleteCategory(int id);
        Category UpdateCategory(Category category);
    }
}
