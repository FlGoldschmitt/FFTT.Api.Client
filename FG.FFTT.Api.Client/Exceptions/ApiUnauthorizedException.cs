using System;
using FG.FFTT.Api.Client.Properties;

namespace FG.FFTT.Api.Client.Exceptions
{
    /// <summary>
    /// Type of exception thrown when the identification parameters (serie, tm, tmc and id) are missing or incorrect.
    /// </summary>
    public class ApiUnauthorizedException : ApiException
    {
        public ApiUnauthorizedException(string details, Exception innerException = null)
            : base($"{Resources.UnauthorizedMessage} {details}", innerException)
        { }
    }
}