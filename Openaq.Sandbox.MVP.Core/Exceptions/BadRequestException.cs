using System;

namespace Openaq.Sandbox.MVP.Core.Exceptions
{
    public class BadRequestException : ApplicationException
    {
        public BadRequestException(string message) : base(message)
        {

        }
    }
}
