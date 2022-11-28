using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboAz.Core.Models;
using TurboAz.Repository.CQRS.Commands.Abstract;
using TurboAz.Repository.Infrustructure;

namespace TurboAz.Repository.CQRS.Commands.Concrete
{
    public class PaymentCommand : IPaymentCommand
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUnitOfWorkAdoNet _unitOfWorkAdoNet;

        public PaymentCommand(IUnitOfWork unitOfWork, IUnitOfWorkAdoNet unitOfWorkAdoNet)
        {
            _unitOfWork = unitOfWork;
            _unitOfWorkAdoNet = unitOfWorkAdoNet;
        }

        private string _paySql = $@"INSERT INTO PAYMENTS([CardNumber],[Email])
                                            VALUES(@{nameof(Payment.CardNumber)},
                                                  @{nameof(Payment.Email)})";

        public async Task<bool> Pay(CardNumber cardNumber, Email email)
        {
            var conn = _unitOfWorkAdoNet.GetConnection();
            try
            {
                #region Dapper
                var param = new
                {
                    email = email.EmailValue,
                    cardNumber = cardNumber.CardNumberValue,
                };
                await _unitOfWork.GetConnection().QueryAsync(_paySql, param, _unitOfWork.GetTransaction());
                return true;
                #endregion

                #region AdoNet
                var command = new SqlCommand(_paySql, conn);
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                var paramCard = new SqlParameter();
                paramCard.ParameterName = $"@{nameof(Payment.CardNumber)}";
                paramCard.SqlDbType = SqlDbType.NVarChar;
                paramCard.Value = cardNumber;
                command.Parameters.Add(paramCard);

                var paramEmail = new SqlParameter();
                paramEmail.ParameterName = $"@{nameof(Payment.Email)}";
                paramEmail.SqlDbType = SqlDbType.NVarChar;
                paramEmail.Value = email;
                command.Parameters.Add(paramEmail);

                command.ExecuteNonQuery();
                return true;
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
                return false;
            }
        }
    }
}
