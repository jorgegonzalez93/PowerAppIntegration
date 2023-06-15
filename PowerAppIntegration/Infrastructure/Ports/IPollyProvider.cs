using Polly.Retry;

namespace Migration.Domain.Infrastructure.Ports
{
    public interface IPollyProvider
    {
        int GetCurentDateInFormatyyyyMMdd(DateTime date);
        AsyncRetryPolicy GetRetryPolicy(string method);
        AsyncRetryPolicy GetFasterRetryPolicy(string method);
    }
}
