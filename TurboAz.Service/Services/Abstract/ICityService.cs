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
        City GetById(int id);
        IEnumerable<City> GetAll();
        int AddCity(City city);
        City Update(City city);
        bool Delete(int id); 

    }
}
