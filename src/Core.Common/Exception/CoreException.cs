using System;
using Core.Common.Resources;

namespace Core.Common.Exception
{
    public enum CoreExceptionCode
    {
        Ex001
    }

    public class CoreException : System.Exception
    {
        public string Code { get; set; }
        public string Type { get; set; }
        public bool IsLoggable { get; set; }
        public string Identifier { get; set; }
        public string SessionId { get; set; }


        public CoreException(string type, string code, System.Exception inner = null)
            : base(ResourceHelper.GetLocaleString(code), inner)
        {
            Code = code;
            Type = type;
            IsLoggable = true;
            Identifier = Guid.NewGuid().ToString();
        }
    }
}