using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboAz.Core.Models;
using TurboAz.Core.RequestsModels;
using TurboAz.Repository.CQRS.Commands.Abstract;
using TurboAz.Repository.CQRS.Queries.Abstract;
using TurboAz.Repository.Repositories.Abstract;

namespace TurboAz.Repository.Repositories.Concrete
{
    public class CarRepository : ICarRepository
    {
        private readonly ICarCommand _carCommand;
        private readonly ICarQuery _carQuery;

        public CarRepository(ICarCommand carCommand, ICarQuery carQuery)
        {
            _carCommand = carCommand;
            _carQuery = carQuery;
        }

        public async Task<int> AddCar(Car car)
        {
            var res = await _carCommand.AddCar(car);
            return res;
        }

        public async Task<bool> DeleteCar(int id)
        {
            var res = await _carCommand.DeleteCar(id);
            return res;
        }

        public async Task<IEnumerable<Car>> GetAll()
        {
            var res = await _carQuery.GetAll();
            return res;
        }

        public async Task<IEnumerable<Car>> GetAllPaging(PagingModel model)
        {
            var res = await _carQuery.GetAllPaging(model);
            return res;
        }

        public async Task<Car> GetById(int id)
        {
            var res = await _carQuery.GetById(id);
            return res;
        }

        public async Task<Car> UpdateCar(Car car)
        {
            var res = await _carCommand.UpdateCar(car);
            return res;
        }

    }
}
