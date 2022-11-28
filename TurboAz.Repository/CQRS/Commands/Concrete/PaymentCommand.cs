using Dapper;
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
        private readonly IUnitOfWork _unitOfWork;

        public PaymentCommand(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private string _paySql = $@"INSERT INTO PAYMENTS([CardNumber],[Email])
                                            VALUES(@{nameof(Payment.CardNumber)},
                                                  @{nameof(Payment.Email)})";

        public async Task<bool> Pay(CardNumber cardNumber, Email email)
        {
            try
            {

                var param = new
                {
                    email= email.EmailValue,
                    cardNumber= cardNumber.CardNumberValue,
                };
                await _unitOfWork.GetConnection().QueryAsync( _paySql, param , _unitOfWork.GetTransaction());
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                return false;
            }
        }
    }
}
