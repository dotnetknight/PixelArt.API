using System;
using System.Net;

namespace PixelArt.Models.Exceptions
{
    public class BaseApiException : Exception
    {
        public int ResponseHttpStatusCode { get; }

        public string BackEndMessage { get; protected set; }

        public BaseApiException(HttpStatusCode responseHttpStatusCode)
        {
            ResponseHttpStatusCode = (int)responseHttpStatusCode;
        }

        public BaseApiException(HttpStatusCode responseHttpStatusCode, string message)
        {
            ResponseHttpStatusCode = (int)responseHttpStatusCode;
            BackEndMessage = message;
        }
    }
}
