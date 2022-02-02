using System;

namespace FG.FFTT.Api.Client.Extensions
{
    /// <summary>
    /// Extension methods for the DateTime.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Returns the DateTime as a timestamp "yyyyMMddHHmmssFFF".
        /// </summary>
        /// <param name="dateTime">Instance of DateTime</param>
        /// <returns></returns>
        public static string GetTimestamp(this DateTime dateTime)
        {
            return dateTime.ToString("yyyyMMddHHmmssFFF");
        }
    }
}