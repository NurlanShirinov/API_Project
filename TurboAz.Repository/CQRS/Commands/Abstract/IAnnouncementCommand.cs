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
        int Add(Announcement announcement);
        bool Delete(int id);
        Announcement Update(Announcement announcement);
        void SetVip(int announcmentId);
       
    }
}
