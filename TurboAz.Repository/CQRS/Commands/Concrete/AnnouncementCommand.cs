using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboAz.Core.Models;
using TurboAz.Repository.CQRS.Commands.Abstract;

namespace TurboAz.Repository.CQRS.Commands.Concrete
{
    public class AnnouncementCommand : IAnnouncementCommand
    {
        private readonly IUnitOfWork1<Announcement> _unitOfWork;

        public AnnouncementCommand(IUnitOfWork1<Announcement> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public int Add(Announcement announcement)
        {
            var annList = _unitOfWork.DeserializeFromJson<Announcement>();
            var lastAnnouncement = annList.OrderByDescending(i => i.Id).FirstOrDefault();
            if (lastAnnouncement is null)
            {
                announcement.Id = 0;
            }
            else
            {
                announcement.Id = lastAnnouncement.Id + 1;
            }
            annList.Add(announcement);
            _unitOfWork.WriteToJson(annList);
            return announcement.Id;
        }

        public bool Delete(int id)
        {
            var annList = _unitOfWork.DeserializeFromJson<Announcement>();
            var currentAnn = annList.FirstOrDefault(i => i.Id == id);
            var res = annList.Remove(currentAnn);
            _unitOfWork.WriteToJson(annList);
            return res;
        }

        public void SetVip(int announcmentId)
        {
            var announcementList = _unitOfWork.DeserializeFromJson<Announcement>();
            var currentAnnouncement = announcementList.FirstOrDefault(i => i.Id == announcmentId);
            if (currentAnnouncement is not null)
            {
                currentAnnouncement.IsVip = true;
                _unitOfWork.WriteToJson(announcementList);
            }
        }

        public Announcement Update(Announcement announcement)
        {
            var announcementList = _unitOfWork.DeserializeFromJson<Announcement>();
            var currentAnnouncement = announcementList.OrderByDescending(i => i.Id == announcement.Id).FirstOrDefault();
            if (currentAnnouncement is not null)
            {
                currentAnnouncement.CreatedDate = DateTime.Now;
                currentAnnouncement.AnnouncedCar = announcement.AnnouncedCar;
                currentAnnouncement.AnnouncedCity = announcement.AnnouncedCity;
            }
            _unitOfWork.WriteToJson(announcementList);
            return currentAnnouncement;
        }
    }
}
