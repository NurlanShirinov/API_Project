using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TurboAz.Core.CustomExceptions;

namespace TurboAz.Core.Models
{
    public class Email
    {

        private string? _email;
        public string? EmailValue
        {
            get { return _email; }
            set { SetEmail(value); }
        }

        private void SetEmail(string? email)
        {
            if (String.IsNullOrWhiteSpace(email))
            {
                throw new CustomExceptionClass("Added E-Mail is not excist", DateTime.Now, System.Reflection.Assembly.GetExecutingAssembly().Location);
            }

            if (CheckEmail(email))
            {
                _email = email;
            }
            else
            {
                throw new CustomExceptionClass("Email cannot be null", DateTime.Now, System.Reflection.Assembly.GetExecutingAssembly().Location);
            }
        }

        private bool CheckEmail(string? email) => Regex.IsMatch(email, "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$");
    }
}
