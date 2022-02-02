using System;
using FG.FFTT.Api.Client.Properties;

namespace FG.FFTT.Api.Client.Exceptions
{
    /// <summary>
    /// Type of exception thrown when a required parameter is empty or incorrect.
    /// </summary>
    public class ApiInvalidParametersException : ApiException
    {
        public ApiInvalidParametersException(string details, Exception innerException = null)
            : base($"{Resources.InvalidParametersMessage} {details}", innerException)
        { }
    }
}