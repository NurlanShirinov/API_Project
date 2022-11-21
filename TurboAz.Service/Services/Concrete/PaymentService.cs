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
            if (res)
            {
                await _emailService.SendEmailAsync(new EmailRequest()
                {
                    Body = "Succesfully accessed",
                    Subject = "Payment succeed",
                    ToEmail = email.EmailValue
                });
            }
            else
            {
                await _emailService.SendEmailAsync(new EmailRequest()
                {
                    Body = "Accessed denied",
                    Subject = "Payment failed",
                    ToEmail = email.EmailValue
                });
            }
            
            return res;
        }
    }
}
