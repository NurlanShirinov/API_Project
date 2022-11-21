using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboAz.Core.Models;
using TurboAz.Core.RequestsModels;

namespace TurboAz.Repository.Repositories.Abstract
{
    public interface ICityRepository
    {
        Task<int> AddCity(City city);
        Task<bool> DeleteCity(int id);
        Task<City> UpdateCity(City city);
        Task<IEnumerable<City>> GetAll();
        Task<City> GetById(int id);
        Task<IEnumerable<City>> GetAllPaging(PagingModel model);

    }
}
