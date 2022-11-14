using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboAz.Core.Models;
using TurboAz.Core.RequestsModels;
using TurboAz.Repository.Repositories.Abstract;
using TurboAz.Service.Services.Abstract;

namespace TurboAz.Service.Services.Concrete
{
    public class AnnouncementService : IAnnouncementService
    {

        private readonly IAnnouncementRepository _announcementRepository;

        public AnnouncementService(IAnnouncementRepository announcementRepository)
        {
            _announcementRepository = announcementRepository;
        }

        public int Add(Announcement announcement)
        {
            var res = _announcementRepository.Add(announcement);
            return res;
        }

        public bool Delete(int id)
        {
            var res = _announcementRepository.Delete(id);
            return res;
        }

        public IEnumerable<Announcement> Filtered(GetFilteredDataRequestModel get)
        {
            var res = _announcementRepository.Filtered(get);
            return res;
        }

        public IEnumerable<Announcement> GetAll()
        {
            var res = _announcementRepository.GetAll();
            return res;
        }

        public Announcement GetById(int id)
        {
            var res = _announcementRepository.GetById(id);
            return res;
        }

        public void SetVip(int announcmentId)
        {
            _announcementRepository.SetVip(announcmentId);
        }

        public Announcement Update(Announcement announcement)
        {
            var res = _announcementRepository.Update(announcement);
            return res;
        }
    }
}
