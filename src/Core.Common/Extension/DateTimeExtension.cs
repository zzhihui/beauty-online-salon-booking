using System;

namespace Core.Common.Utility.Extension
{
    public static class DateTimeExtension
    {
        public static string ToDateUtcFormat(this DateTime? source)
        {
            return source.HasValue ? source.Value.ToString("yyyy-MM-dd") : string.Empty;
        }
    }
}