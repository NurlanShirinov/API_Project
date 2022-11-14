using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboAz.Core.Models;

namespace TurboAz.Repository.CQRS.Commands.Abstract
{
    public interface ICityCommand
    {
        Task<int> AddCity(City city);
        Task<bool> DeleteCity(int id);
        Task<City> UpdateCity(City city);
    }
}
