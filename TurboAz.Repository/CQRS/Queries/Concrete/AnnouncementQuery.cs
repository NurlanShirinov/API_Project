using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboAz.Core.Models;
using TurboAz.Core.RequestsModels;
using TurboAz.Repository.CQRS.Queries.Abstract;

namespace TurboAz.Repository.CQRS.Queries.Concrete
{
    public class AnnouncementQuery: IAnnouncementQuery
    {
        private readonly IUnitOfWork1<Announcement> _unitOfWork;

        public AnnouncementQuery(IUnitOfWork1<Announcement> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Announcement> Filtered(GetFilteredDataRequestModel get)
        {
            var announcementList = _unitOfWork.DeserializeFromJson<Announcement>();

            if (get.IsActive)
            {
                announcementList = announcementList.Where(i => i.IsActive == true).ToList();
            }
            if (get.MinPrice != 0)
            {
                announcementList = announcementList.Where(i => i.AnnouncedCar.Price > get.MinPrice).ToList();
            }
            if (get.MaxPrice != 0)
            {
                announcementList = announcementList.Where(i => i.AnnouncedCar.Price < get.MaxPrice).ToList();
            }
            if (get.CategoryId != 0)
            {
                announcementList = announcementList.Where(i => i.AnnouncedCarCategoryId == get.CategoryId).ToList();
            }
            if (!String.IsNullOrWhiteSpace(get.Color))
            {
                announcementList = announcementList.Where(i => i.AnnouncedCar.Color == get.Color).ToList();
            }
            if (!String.IsNullOrWhiteSpace(get.Model))
            {
                announcementList = announcementList.Where(i => i.AnnouncedCar.Model == get.Model).ToList();
            }
            if (get.Year != 0 && get.Year > 1900 && get.Year <= 2024)
            {
                announcementList = announcementList.Where(i => i.AnnouncedCar.Year == get.Year).ToList();
            }
            if (get.IsVip)
            {
                announcementList = announcementList.Where(i => i.IsVip == true).ToList();
            }
            if (get.StartDate is not null)
            {
                announcementList = announcementList.Where(i => i.CreatedDate == get.StartDate).ToList();
            }
            if (get.EndDate is not null)
            {
                announcementList = announcementList.Where(i => i.AnnouncementDeadline == get.EndDate).ToList();
            }
            return announcementList;
        }

        public IEnumerable<Announcement> GetAll()
        {
            var AnnList = _unitOfWork.DeserializeFromJson<Announcement>();
            return AnnList;
        }

        public Announcement GetById(int id)
        {
            var annList = _unitOfWork.DeserializeFromJson<Announcement>();
            var currentAnnouncement = annList.FirstOrDefault(i=>i.Id==id);
            return currentAnnouncement;
        }
    }
}
