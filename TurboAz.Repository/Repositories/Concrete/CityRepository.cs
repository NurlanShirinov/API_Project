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
    public class CityRepository : ICityRepository
    {
        private readonly ICityCommand _cityCommand;
        private readonly ICityQuery _cityQuery;

        public CityRepository(ICityCommand cityCommand, ICityQuery cityQuery)
        {
            _cityCommand = cityCommand;
            _cityQuery = cityQuery;
        }

        public async Task<int> AddCity(City city)
        {
            var res = await _cityCommand.AddCity(city);
            return res;
        }

        public async Task<bool> DeleteCity(int id)
        {
            var res = await _cityCommand.DeleteCity(id);
            return res;
        }

        public async Task<IEnumerable<City>> GetAll()
        {
            var result = await _cityQuery.GetAll();
            return result;
        }

        public async Task<City> UpdateCity(City city)
        {
            var res = await _cityCommand.UpdateCity(city);
            return res;
        }

        public async Task<City> GetById(int id)
        {
            var res = await _cityQuery.GetById(id);
            return res;
        }

        public async Task<IEnumerable<City>> GetAllPaging(PagingModel model)
        {
            var res = await _cityQuery.GetAllPaging(model);
            return res;
        }
    }
}
