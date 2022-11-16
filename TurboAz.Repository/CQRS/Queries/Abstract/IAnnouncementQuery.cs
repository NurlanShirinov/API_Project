using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboAz.Core.Models;
using TurboAz.Core.RequestsModels;

namespace TurboAz.Repository.CQRS.Queries.Abstract
{
    public interface IAnnouncementQuery
    {
        Task<Announcement> GetById(int id); 
        Task<IEnumerable<Announcement>> GetAll(); 
        //Task<IEnumerable<Announcement>> Filtered(GetFilteredDataRequestModel get);
    }
}
