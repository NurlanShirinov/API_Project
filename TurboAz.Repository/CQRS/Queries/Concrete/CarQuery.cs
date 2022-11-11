using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboAz.Core.Models;
using TurboAz.Repository.CQRS.Queries.Abstract;

namespace TurboAz.Repository.CQRS.Queries.Concrete
{
    public class CarQuery : ICarQuery
    {
        private readonly IUnitOfWork<Car> _unitOfWork;

        public CarQuery(IUnitOfWork<Car> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Car> GetAll()
        {
            var carList = _unitOfWork.DeserializeFromJson<Car>();
            return carList;
        }

        public Car GetById(int id)
        {
            var carList = _unitOfWork.DeserializeFromJson<Car>();
            var currentCar = carList.OrderByDescending(i => i.Id == id).FirstOrDefault();
            return currentCar;
        }
    }
}
