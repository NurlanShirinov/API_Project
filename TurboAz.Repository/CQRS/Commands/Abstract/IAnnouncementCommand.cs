using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboAz.Core.Models;

namespace TurboAz.Repository.CQRS.Commands.Abstract
{
    public interface IAnnouncementCommand
    {
        Task<int> Add(Announcement announcement);
        Task<bool> Delete(int id);
        Task<Announcement> Update(Announcement announcement);
        Task SetVip(int announcmentId);
       
    }
}
