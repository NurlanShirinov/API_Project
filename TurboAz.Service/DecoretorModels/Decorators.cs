using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurboAz.Service.DecoretorModels
{
    public class Decorators
    {
        [AttributeUsage(AttributeTargets.Method)]
        public class LimitRequest : Attribute
        {
            public int TimeWindow
            {
                get;
                set;
            }
            public int MaxRequests
            {
                get;
                set;
            }
        }
    }
}
