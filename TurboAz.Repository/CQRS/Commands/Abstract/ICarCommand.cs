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
        Task<int> AddCar(Car car);
        Task<bool> DeleteCar(int id);
        Task<Car> UpdateCar(Car car);
       
    }
}
