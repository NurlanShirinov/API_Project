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


        public async Task<int> AddCity(City city)
        {
            var res = await _cityRepository.AddCity(city);
            return res;
        }

        public async Task<bool> Delete(int id)
        {
            var res = await _cityRepository.DeleteCity(id);
            return res;
        }

        public async Task<IEnumerable<City>> GetAll()
        {
           var res =await _cityRepository.GetAll();
            return res;
        }

        public async Task<City> GetById(int id)
        {
            var res = await _cityRepository.GetById(id);
            return res;
        }

        public async Task<City> Update(City city)
        {
            var res =await _cityRepository.UpdateCity(city);
            return res;
        }

    }
}
