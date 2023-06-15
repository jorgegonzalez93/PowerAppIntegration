using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Migration.Domain.Infrastructure.Exceptions
{
    [Serializable, ExcludeFromCodeCoverage]
    public class HttpRestClientException : Exception
    {
        public HttpResponseMessage HttpResponseMessage { get; }

        public HttpRestClientException()
        {
        }

        public HttpRestClientException(string message) : base(message)
        {
        }

        public HttpRestClientException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public HttpRestClientException(HttpResponseMessage httpResponseMessage)
        {
            HttpResponseMessage = httpResponseMessage;
        }

        public HttpRestClientException(string message, HttpResponseMessage httpResponseMessage) : base(message)
        {
            HttpResponseMessage = httpResponseMessage;
        }

        public HttpRestClientException(string message, Exception innerException, HttpResponseMessage httpResponseMessage) : base(message, innerException)
        {
            HttpResponseMessage = httpResponseMessage;
        }

        protected HttpRestClientException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
