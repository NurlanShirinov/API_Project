using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboAz.Core.Models;

namespace TurboAz.Service.Services.Abstract
{
    public interface ICarService
    {
        int AddCar(Car car);
        bool DeleteCar(int id);
        Car UpdateCar(Car car);
        Car GetById(int id);
        IEnumerable<Car> GetAll();
    }
}
