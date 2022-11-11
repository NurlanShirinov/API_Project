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
    public class CityRepository : ICityRepository
    {
        private readonly ICityCommand _cityCommand;
        private readonly ICityQuery _cityQuery;

        public CityRepository(ICityCommand cityCommand, ICityQuery cityQuery)
        {
            _cityCommand = cityCommand;
            _cityQuery = cityQuery;
        }

        public int AddCity(City city)
        {
            var res = _cityCommand.AddCity(city);
            return res;
        }

        public bool DeleteCity(int id)
        {
            var res = _cityCommand.DeleteCity(id);
            return res;
        }

        public IEnumerable<City> GetAllCities()
        {
            var result = _cityQuery.GetAll();
            return result;
        }

        public City GetById(int id)
        {
            var res = _cityQuery.GetById(id);
            return res;
        }

        public City UpdateCity(City city)
        {
            var res = _cityCommand.UpdateCity(city);
            return res;
        }
    }
}
