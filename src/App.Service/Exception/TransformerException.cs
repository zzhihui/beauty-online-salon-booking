using Core.Common.Exception;

namespace App.Service.Exception
{
    public enum TransformerExceptionCode
    {
        TrEx001, //General Error
    }

    public class TransformerException : CoreException
    {
        public const string ExceptionType = "TransformerException";
        public TransformerException(System.Exception inner, string code = null)
            : base(ExceptionType, CheckDefaultCode(code), inner)
        {
        }

        private static string CheckDefaultCode(string code)
        {
            return string.IsNullOrEmpty(code) ? TransformerExceptionCode.TrEx001.ToString() : code;
        }
    }
}