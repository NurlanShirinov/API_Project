using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboAz.Core.Models;

namespace TurboAz.Service.Services.Abstract
{
    public interface ICityService
    {
        Task<IEnumerable<City>> GetAll();
        Task<City> GetById(int id);
        Task<int> AddCity(City city);
        Task<City> Update(City city);
        Task<bool> Delete(int id); 

    }
}
