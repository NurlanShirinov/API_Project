using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboAz.Core.Models;

namespace TurboAz.Service.Services.Abstract
{
    public interface IPaymentService
    {
        public bool Pay(CardNumber cardNumber, Email email);
    }
}
