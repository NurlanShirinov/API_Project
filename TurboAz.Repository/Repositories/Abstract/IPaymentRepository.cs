using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboAz.Core.Models;

namespace TurboAz.Repository.Repositories.Abstract
{
    public interface IPaymentRepository
    {
        Task<bool> Pay(CardNumber cardNumber, Email email);
    }
}
