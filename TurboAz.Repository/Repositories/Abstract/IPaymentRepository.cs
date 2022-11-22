using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboAz.Core.Models;
using TurboAz.Core.RequestsModels;

namespace TurboAz.Repository.Repositories.Abstract
{
    public interface IPaymentRepository
    {
        Task<bool> Pay(CardNumber cardNumber, Email email);
    }
}
