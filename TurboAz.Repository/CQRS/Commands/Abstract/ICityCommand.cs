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
        int AddCity(City city);
        bool DeleteCity(int id);
        City UpdateCity(City city);
    }
}
