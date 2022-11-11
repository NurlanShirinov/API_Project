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
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;

        public CityService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }


        public int AddCity(City city)
        {
            var res = _cityRepository.AddCity(city);
            return res;
        }

        public bool Delete(int id)
        {
            var res = _cityRepository.DeleteCity(id);
            return res;
        }

        public IEnumerable<City> GetAll()
        {
           var res = _cityRepository.GetAllCities();
            return res;
        }

        public City GetById(int id)
        {
            var res = _cityRepository.GetById(id);
            return res;
        }

        public City Update(City city)
        {
            var res = _cityRepository.UpdateCity(city);
            return res;
        }
    }
}
