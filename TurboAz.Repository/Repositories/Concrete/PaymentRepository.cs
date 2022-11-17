using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboAz.Core.Models;
using TurboAz.Repository.CQRS.Commands.Abstract;
using TurboAz.Repository.Repositories.Abstract;

namespace TurboAz.Repository.Repositories.Concrete
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly IPaymentCommand _paymentCommand;

        public PaymentRepository(IPaymentCommand paymentCommand)
        {
            _paymentCommand = paymentCommand;
        }

        public async Task<bool> Pay(CardNumber cardNumber, Email email)
        {
            try
            {
                var res = await _paymentCommand.Pay(cardNumber, email);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
                return false;
            }
         
        }
    }
}
