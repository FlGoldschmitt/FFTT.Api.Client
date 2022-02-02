using System;
using FG.FFTT.Api.Client.Properties;

namespace FG.FFTT.Api.Client.Exceptions
{
    /// <summary>
    /// Type of exception thrown when no results are found.
    /// </summary>
    public class ApiNotFoundException : ApiException
    {
        public ApiNotFoundException(Exception innerException = null)
            : base(Resources.NotFoundMessage, innerException)
        { }
    }
}