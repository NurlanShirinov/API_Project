using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboAz.Core.Models;
using TurboAz.Repository.CQRS.Commands.Abstract;

namespace TurboAz.Repository.CQRS.Commands.Concrete
{
    public class CityCommand : ICityCommand
    {
        private readonly IUnitOfWork<City> _unitOfWork;
        public CityCommand(IUnitOfWork<City> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public int AddCity(City city)
        {
            var cityList = _unitOfWork.DeserializeFromJson<City>();
            var currentCity = cityList.OrderByDescending(i => i.Id).FirstOrDefault();
            if (currentCity is null)
            {
                city.Id = 0;
            }
            else
            {
                city.Id = currentCity.Id = 1;
            }
            cityList.Add(city);
            _unitOfWork.WriteToJson(cityList);
            return city.Id;
        }

        public bool DeleteCity(int id)
        {
            var cityList = _unitOfWork.DeserializeFromJson<City>();
            var currentCity=cityList.OrderByDescending(i => i.Id==id).FirstOrDefault();
            var result = cityList.Remove(currentCity);
            _unitOfWork.WriteToJson(cityList);
            return result;
        }

        public City UpdateCity(City city)
        {
            var cityList = _unitOfWork.DeserializeFromJson<City>();
            var currentCity = cityList.OrderByDescending(i => i.Id == city.Id).FirstOrDefault();
            if(currentCity is not null)
            {
                currentCity.Name = city.Name;
            }
            _unitOfWork.WriteToJson(cityList);
            return currentCity;
        }
    }
}
