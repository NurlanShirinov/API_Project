using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboAz.Core.Models;

namespace TurboAz.Repository.CQRS.Commands.Abstract
{
    public interface ICarCommand
    {
        int AddCar(Car car);
        bool DeleteCar(int id);
        Car UpdateCar(Car car);
       
    }
}
