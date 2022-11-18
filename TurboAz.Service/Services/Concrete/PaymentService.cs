using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboAz.Core.Models;
using TurboAz.Repository.Repositories.Abstract;
using TurboAz.Service.Services.Abstract;

namespace TurboAz.Service.Services.Concrete
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IEmailService _emailService;

        public PaymentService(IPaymentRepository paymentRepository, IEmailService emailService)
        {
            _paymentRepository = paymentRepository;
            _emailService = emailService;
        }
        public async  Task<bool> Pay(CardNumber cardNumber, Email email)
        {
            var res = await _paymentRepository.Pay(cardNumber,email);

            await _emailService.SendEmailAsync(new EmailRequest()
            {
                Body = "Aue",
                Subject = "Salam",
                ToEmail = "nurlan.shirinov1998@gmail.com"
            });
            
            return res;
        }
    }
}
