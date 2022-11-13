using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboAz.Core.Models;
using TurboAz.Repository.CQRS.Queries.Abstract;

namespace TurboAz.Repository.CQRS.Queries.Concrete
{
    public class CityQuery : ICityQuery
    {

        private readonly IUnitOfWork<City> _unitOfWork;

        public CityQuery(IUnitOfWork<City> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<City> GetAll()
        {
            var result = _unitOfWork.DeserializeFromJson<City>();
            return result;
        }

        public City GetById(int id)
        {
            var cityList = _unitOfWork.DeserializeFromJson<City>();
            var currentCity = cityList.FirstOrDefault(i=>i.Id==id);
            return currentCity;
        }
    }
}
