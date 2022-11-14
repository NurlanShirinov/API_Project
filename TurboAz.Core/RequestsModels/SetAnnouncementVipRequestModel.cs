using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboAz.Core.Models;

namespace TurboAz.Core.RequestsModels
{
    public class SetAnnouncmentVipRequestModel
    {
        public int AnnouncmentId { get; set; }
        public Email? Email { get; set; }
        public CardNumber? CardNumber { get; set; }
    }
}
