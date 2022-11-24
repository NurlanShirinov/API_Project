using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurboAz.Service.Infrastructure
{
    public class ClientStatistics
    {
        public DateTime LastSuccessfulResponseTime { get; set; }
        public int NumberofRequestsCompletedSuccessfully { get; set; }
    }
}
