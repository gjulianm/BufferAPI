using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BufferAPI
{
    class TimeConversion
    {
        public static DateTime UnixTimeStampToDateTime(double? unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp.Value).ToLocalTime();
            return dtDateTime;
        }

        public static double DateTimeToUnixTimestamp(DateTime dateTime)
        {
            if (dateTime == null)
            {
                throw new Exception("Date Time is null, cannot convert.");
            }
            else
            {
                return (dateTime - new DateTime(1970, 1, 1).ToLocalTime()).TotalSeconds;
            }
        }
    }

}