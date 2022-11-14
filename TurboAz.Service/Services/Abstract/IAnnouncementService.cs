using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboAz.Core.Models;
using TurboAz.Core.RequestsModels;

namespace TurboAz.Service.Services.Abstract
{
    public interface IAnnouncementService
    {
        int Add(Announcement announcement);
        bool Delete(int id);
        Announcement Update(Announcement announcement);
        Announcement GetById(int id);
        IEnumerable<Announcement> GetAll();
        void SetVip(int announcmentId);
        IEnumerable<Announcement> Filtered(GetFilteredDataRequestModel get);
    }
}
