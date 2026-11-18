using Core.Common.Exception;

namespace App.Service.Exception
{
    public enum ServiceExceptionCode
    {
        SEx001, //General Error
        SEx002, //Validation Error

    }


    public class ServiceException : CoreException
    {
        public const string ExceptionType = "ServiceException";
        public ServiceException(System.Exception inner, string code = null)
            : base(ExceptionType, CheckDefaultCode(code), inner)
        {
        }

        private static string CheckDefaultCode(string code)
        {
            return string.IsNullOrEmpty(code) ? ServiceExceptionCode.SEx001.ToString() : code;
        }
    }
}