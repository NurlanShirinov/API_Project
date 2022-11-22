using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboAz.Core.Models;
using TurboAz.Core.RequestsModels;
using TurboAz.Core.ResponseModels;
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


        public async Task<int> Add(Announcement announcement)
        {
            var res = await _announcementRepository.Add(announcement);
            return res;
        }

        public async Task<bool> Delete(int id)
        {
            var res = await _announcementRepository.Delete(id);
            return res;
        }

        public async Task<IEnumerable<AnnoncementResponseModel>> Filtered(GetFilteredDataRequestModel get)
        {
            var res =await _announcementRepository.Filtered(get);
            return res;
        }

        public async Task<IEnumerable<Announcement>> GetAll()
        {
            var res = await _announcementRepository.GetAll();
            return res;
        }

        public async Task<IEnumerable<Announcement>> GetAllPagining(PagingModel model)
        {
            var res = await _announcementRepository.GetAllPaging(model);
            return res;
        }

        public async Task<Announcement> GetById(int id)
        {
            var res = await _announcementRepository.GetById(id);
            return res;
        }

        public async Task SetVip(int announcmentId)
        {
            await _announcementRepository.SetVip(announcmentId);
        }

        public async Task<Announcement> Update(Announcement announcement)
        {
            var res = await _announcementRepository.Update(announcement);
            return res;
        }
    }
}
