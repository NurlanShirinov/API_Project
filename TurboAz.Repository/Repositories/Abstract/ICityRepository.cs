using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboAz.Core.Models;

namespace TurboAz.Repository.Repositories.Abstract
{
    public interface ICityRepository
    {
        int AddCity(City city);
        bool DeleteCity(int id);
        City UpdateCity(City city);
        IEnumerable<City> GetAllCities();
        City GetById(int id);
    }
}
