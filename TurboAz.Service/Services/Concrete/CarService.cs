using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboAz.Core.Models;
using TurboAz.Core.RequestsModels;
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

        public async Task<int> AddCar(Car car)
        {
            var res = await _carRepository.AddCar(car);
            return res;
        }

        public async Task<bool> DeleteCar(int id)
        {
            var res = await _carRepository.DeleteCar(id);
            return res;
        }

        public async  Task<IEnumerable<Car>> GetAll()
        {
            var res = await _carRepository.GetAll();
            return res;
        }

        public async Task<IEnumerable<Car>> GetAllPaging(PagingModel model)
        {
            var res = await _carRepository.GetAllPaging(model);
            return res;
        }

        public async Task<Car> GetById(int id)
        {
            var res = await _carRepository.GetById(id);
            return res;
        }

        public async Task<Car> UpdateCar(Car car)
        {
            var res = await _carRepository.UpdateCar(car);
            return car;
        }
    }
}
