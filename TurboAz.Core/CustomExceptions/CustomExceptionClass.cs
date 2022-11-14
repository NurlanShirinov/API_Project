using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurboAz.Core.CustomExceptions
{
    public class CustomExceptionClass : ApplicationException
    {
        public CustomExceptionClass(string message, DateTime dateTime, string errorFilePath)
        {
            Message = message;
            this.dateTime = dateTime;
            ErrorFilePath = errorFilePath;
        }
        public string Message { get; set; }
        public DateTime dateTime { get; set; }
        public string ErrorFilePath { get; set; }
        public override string ToString()
        {
            return $@"Message : {Message}
Error Time : {dateTime}
Erro File Path {ErrorFilePath}";
        }
    }
}
