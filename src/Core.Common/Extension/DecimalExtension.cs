using System;
using System.Globalization;

namespace Core.Common.Utility.Extension
{
    public static class DecimalExtension
    {
        public static string ToPriceStringIe(this decimal? source)
        {
            return source.HasValue ? source.Value.ToString("c2", new CultureInfo("en-IE")) : string.Empty;
        }

        public static string ToPriceStringIe(this decimal source)
        {
            return source.ToString("c2", new CultureInfo("en-IE"));
        }
    }
}