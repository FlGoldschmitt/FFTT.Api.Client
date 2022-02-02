using System;

namespace FG.FFTT.Api.Client.Exceptions
{
    /// <summary>
    /// Basic exception returned by the API client.
    /// </summary>
    public class ApiException : Exception
    {
        public ApiException(string message, Exception innerException = null)
            : base(message, innerException)
        { }

        /// <summary>
        /// Thrown when no results are found.
        /// </summary>
        /// <returns></returns>
        public static ApiNotFoundException NoResult()
        {
            return new ApiNotFoundException();
        }

        /// <summary>
        /// Thrown when the identification parameters (serie, tm, tmc and id) are missing or incorrect.
        /// </summary>
        /// <param name="message">Contains the error details/param>
        /// <returns></returns>
        public static ApiUnauthorizedException Unauthorized(string message)
        {
            return new ApiUnauthorizedException(message);
        }

        /// <summary>
        /// Thrown when a required parameter is empty or incorrect.
        /// </summary>
        /// <param name="message">Contains the error details</param>
        /// <returns></returns>
        public static ApiInvalidParametersException InvalidParameters(string message)
        {
            return new ApiInvalidParametersException(message);
        }
    }
}