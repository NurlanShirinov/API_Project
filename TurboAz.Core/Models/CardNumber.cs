using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TurboAz.Core.Models
{
    public class CardNumber
    {
        private string? _cardNumber;

        public string? CardNumberValue
        {
            get { return _cardNumber; }
            set { SetCardNumber(value); }
        }
        private void SetCardNumber(string? cardNumber)
        {
            if (String.IsNullOrWhiteSpace(cardNumber))
            {
                throw new CustomExceptionClass("CardNumberCannotNullException", DateTime.Now, System.Reflection.Assembly.GetExecutingAssembly().Location);
            }

            if (CheckCardNumber(cardNumber))
            {
                _cardNumber = cardNumber;
            }
            else
            {
                throw new CustomExceptionClass("CardIsNotValidException", DateTime.Now, System.Reflection.Assembly.GetExecutingAssembly().Location);
            }
        }
        private bool CheckCardNumber(string cardNumber)
            => Regex.IsMatch(cardNumber, "^(?:4[0-9]{12}(?:[0-9]{3})?|[25][1-7][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\\d{3})\\d{11})$");

    }
}
