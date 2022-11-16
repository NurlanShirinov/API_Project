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
    public class AnnouncementRepository : IAnnouncementRepository
    {
        private readonly IAnnouncementCommand _announcementCommand;
        private readonly IAnnouncementQuery _announcementQuery;

        public AnnouncementRepository(IAnnouncementCommand announcementCommand, IAnnouncementQuery announcementQuery)
        {
            _announcementCommand = announcementCommand;
            _announcementQuery = announcementQuery;
        }
        public async Task<int> Add(Announcement announcement)
        {
            var res = await _announcementCommand.Add(announcement);
            return res;
        }
        public async Task<bool> Delete(int id)
        {
            var res = await _announcementCommand.Delete(id);
            return res;
        }
        //public IEnumerable<Announcement> Filtered(GetFilteredDataRequestModel get)
        //{
        //    var res = _announcementQuery.Filtered(get);
        //    return res;
        //}
        public async Task<IEnumerable<Announcement>> GetAll()
        {
            var res = await _announcementQuery.GetAll();
            return res;
        }
        public async Task<Announcement> GetById(int id)
        {
            var res = await _announcementQuery.GetById(id);
            return res;
        }
        public async Task SetVip(int announcmentId)
        {
           await _announcementCommand.SetVip(announcmentId);
        }
        public async Task<Announcement> Update(Announcement announcement)
        {
            var res = await _announcementCommand.Update(announcement);
            return res;
        }
    }
}
