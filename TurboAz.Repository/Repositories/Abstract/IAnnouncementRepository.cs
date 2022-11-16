using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboAz.Core.Models;
using TurboAz.Core.RequestsModels;

namespace TurboAz.Repository.Repositories.Abstract
{
    public interface IAnnouncementRepository
    {
        Task<int> Add(Announcement announcement);
        Task<bool> Delete(int id);
        Task<Announcement> Update(Announcement announcement);
        Task<Announcement> GetById(int id);
        Task<IEnumerable<Announcement>> GetAll();
        Task SetVip(int announcmentId);

        //IEnumerable<Announcement> Filtered(GetFilteredDataRequestModel get);
    }
}
