using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboAz.Core.Models;
using TurboAz.Repository.CQRS.Commands.Abstract;

namespace TurboAz.Repository.CQRS.Commands.Concrete
{
    public class PaymentCommand:IPaymentCommand
    {
        private readonly IUnitOfWork1<Payment> _unitOfWork;

        public PaymentCommand(IUnitOfWork1<Payment> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool Pay(CardNumber cardNumber, Email email)
        {
            var paymentList = _unitOfWork.DeserializeFromJson<Payment>();
            var currentPayment = new Payment
            {
                CardNumber = cardNumber.CardNumberValue,
                Email = email.EmailValue
            };
            paymentList.Add(currentPayment);
            _unitOfWork.WriteToJson(paymentList);
            return true;
        }
    }
}
