using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboAz.Core.Models;
using TurboAz.Core.RequestsModels;

namespace TurboAz.Repository.CQRS.Queries.Abstract
{
    public interface ICategoryQuery
    {
        Task<Category> GetById(int id);
        Task<IEnumerable<Category>> GetAll();
        Task<IEnumerable<Category>> GetAllPaging(PagingModel model);
    }
}
