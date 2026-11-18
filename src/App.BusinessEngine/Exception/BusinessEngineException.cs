using Core.Common.Exception;

namespace App.BusinessEngine.Exception
{
    public enum BusinessEngineExceptionCode
    {
        BeEx001
    }

    public class BusinessEngineException : CoreException
    {
        public const string ExceptionType = "BusinessEngineException";
        public BusinessEngineException(System.Exception inner, string code = null)
            : base(ExceptionType, CheckDefaultCode(code), inner)
        {

        }

        private static string CheckDefaultCode(string code)
        {
            return string.IsNullOrEmpty(code) ? BusinessEngineExceptionCode.BeEx001.ToString() : code;
        }
    }
}