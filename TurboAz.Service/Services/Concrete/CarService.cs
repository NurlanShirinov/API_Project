using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboAz.Core.Models;
using TurboAz.Repository.Repositories.Abstract;
using TurboAz.Service.Services.Abstract;

namespace TurboAz.Service.Services.Concrete
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public int AddCar(Car car)
        {
            var res = _carRepository.AddCar(car);
            return res;
        }

        public bool DeleteCar(int id)
        {
            var res = _carRepository.DeleteCar(id);
            return res;
        }

        public IEnumerable<Car> GetAll()
        {
            var res = _carRepository.GetAll();
            return res;
        }

        public Car GetById(int id)
        {
            var res = _carRepository.GetById(id);
            return res;
        }

        public Car UpdateCar(Car car)
        {
            var res = _carRepository.UpdateCar(car);
            return car;
        }
    }
}
