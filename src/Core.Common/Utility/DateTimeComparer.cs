using System;

namespace Core.Common.Utility
{
    public class DateTimeComparer
    {
        public static int CompareTimeOnly(DateTime source, DateTime destination)
        {

            var sourceOnlyTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, source.Hour,
                source.Minute, source.Second);

            var destinationOnlyTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, destination.Hour,
                destination.Minute, destination.Second);


            return DateTime.Compare(sourceOnlyTime, destinationOnlyTime);




        }  
    }
}