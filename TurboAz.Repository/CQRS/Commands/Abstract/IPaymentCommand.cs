using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboAz.Core.Models;

namespace TurboAz.Repository.CQRS.Commands.Abstract
{
    public interface IPaymentCommand
    {
        public bool Pay(CardNumber cardNumber, Email email);
    }
}
