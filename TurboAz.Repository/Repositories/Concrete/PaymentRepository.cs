using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboAz.Core.Models;
using TurboAz.Repository.Repositories.Abstract;

namespace TurboAz.Repository.Repositories.Concrete
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentRepository(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public bool Pay(CardNumber cardNumber, Email email)
        {
           var res = _paymentRepository.Pay(cardNumber, email);
            return res;
        }
    }
}
