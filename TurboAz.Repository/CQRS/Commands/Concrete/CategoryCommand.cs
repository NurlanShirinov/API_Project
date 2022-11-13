using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboAz.Core.Models;
using TurboAz.Repository.CQRS.Commands.Abstract;

namespace TurboAz.Repository.CQRS.Commands.Concrete
{
    public class CategoryCommand : ICategoryCommand
    {
        private readonly IUnitOfWork<Category> _unitOfWork;

        public CategoryCommand(IUnitOfWork<Category> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public int AddCategory(Category category)
        {
            var categoryList = _unitOfWork.DeserializeFromJson<Category>();
            var currentCategory = categoryList.OrderByDescending(i => i.Id == category.Id).FirstOrDefault();
            if (currentCategory is null)
            {
                category.Id = 0;
            }
            else
            {
                category.Id = currentCategory.Id + 1;
            }
            categoryList.Add(category);
            _unitOfWork.WriteToJson(categoryList);
            return category.Id;
        }

        public bool DeleteCategory(int id)
        {
            var categoryList = _unitOfWork.DeserializeFromJson<Category>();
            var currentCategory = categoryList.FirstOrDefault(i => i.Id == id);
            var result = categoryList.Remove(currentCategory);
            _unitOfWork.WriteToJson(categoryList);
            return result;
        }

        public Category UpdateCategory(Category category)
        {
            var categoryList = _unitOfWork.DeserializeFromJson<Category>();
            var currentCategory = categoryList.FirstOrDefault(i => i.Id == category.Id);
            currentCategory.Name = category.Name;
            _unitOfWork.WriteToJson(categoryList);
            return currentCategory;
        }
    }
}
