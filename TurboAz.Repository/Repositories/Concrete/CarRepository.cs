using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboAz.Core.Models;
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

        public int AddCar(Car car)
        {
            var res = _carCommand.AddCar(car);
            return res;
        }

        public bool DeleteCar(int id)
        {
            var res = _carCommand.DeleteCar(id);
            return res;
        }

        public IEnumerable<Car> GetAll()
        {
            var res = _carQuery.GetAll();
            return res;
        }

        public Car GetById(int id)
        {
            var res = _carQuery.GetById(id);
            return res;
        }

        public Car UpdateCar(Car car)
        {
            var res = _carCommand.UpdateCar(car);
            return res;
        }
    }
}
