using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboAz.Core.Models;
using TurboAz.Repository.CQRS.Commands.Abstract;

namespace TurboAz.Repository.CQRS.Commands.Concrete
{
    public class CarCommand : ICarCommand
    {
        private readonly IUnitOfWork1<Car> _unitOfWork;

        public CarCommand(IUnitOfWork1<Car> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public int AddCar(Car car)
        {
            var carList = _unitOfWork.DeserializeFromJson<Car>();
            var currentCar = carList.OrderByDescending(i => i.Id).FirstOrDefault();
            if (currentCar is null)
            {
                car.Id = 0;
            }
            else
            {
                car.Id = currentCar.Id + 1;
            }
            carList.Add(car);
            _unitOfWork.WriteToJson(carList);
            return car.Id;

        }

        public bool DeleteCar(int id)
        {
            var carList = _unitOfWork.DeserializeFromJson<Car>();
            var latestRecord = carList.FirstOrDefault(i => i.Id == id);
            var result=carList.Remove(latestRecord);
            _unitOfWork.WriteToJson(carList);
            return result;
        }

        public Car UpdateCar(Car car)
        {
            var carList = _unitOfWork.DeserializeFromJson<Car>();
            var latestRecord = carList.FirstOrDefault(i => i.Id == car.Id);
            if(latestRecord is not null)
            {
                latestRecord.Color = car.Color;
                latestRecord.Model = car.Model;
                latestRecord.Year = car.Year;
                latestRecord.Price = car.Price;
                latestRecord.Vendor = car.Vendor;
            }
            _unitOfWork.WriteToJson(carList);
            return latestRecord;

        }
    }
}
