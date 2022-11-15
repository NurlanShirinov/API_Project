using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboAz.Core.Models;

namespace TurboAz.Repository.CQRS.Queries.Abstract
{
    public interface ICategoryQuery
    {
        Task<Category> GetById(int id);
        Task<IEnumerable<Category>> GetAll();
    }
}
