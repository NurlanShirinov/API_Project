using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboAz.Core.Models;
using TurboAz.Repository.CQRS.Queries.Abstract;

namespace TurboAz.Repository.CQRS.Queries.Concrete
{
    public class CategoryQuery : ICategoryQuery
    {
        public readonly IUnitOfWork1<Category> _unitOfWork;

        public CategoryQuery(IUnitOfWork1<Category> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Category> GetAll()
        {
            var categoryList = _unitOfWork.DeserializeFromJson<Category>();
            return categoryList;
        }

        public Category GetById(int id)
        {
            var categoryList = _unitOfWork.DeserializeFromJson<Category>();
            var currentCategory = categoryList.FirstOrDefault(i => i.Id == id);
            return currentCategory;
        }
    }
}
