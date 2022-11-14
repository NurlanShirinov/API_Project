using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboAz.Core.Models;
using TurboAz.Service.Services.Abstract;

namespace TurboAz.Service.Services.Concrete
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentService _paymentService;

        public PaymentService(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public bool Pay(CardNumber cardNumber, Email email)
        {
            var res = _paymentService.Pay(cardNumber,email);
            return res;
        }
    }
}
