using System.Globalization;

namespace Migration.Infrastructure.Adapters
{
    public class PollyProvider : IPollyProvider
    {
        private readonly IConfiguration configuration;
        private const int EXPONENT = 2;

        public PollyProvider(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public int GetCurentDateInFormatyyyyMMdd(DateTime date)
        {
            if (!int.TryParse(date.ToString("yyyyMMdd", CultureInfo.InvariantCulture), out int result))
            {
                throw new InvalidCastException($"No fue posible convertir la fecha {date} a un entero");
            }

            return result;
        }

        public AsyncRetryPolicy GetFasterRetryPolicy(string method)
        {
            int retryCountValue = GetRetryCount();
            double retrySecondsValue = GetRetrySeconds();

            return Policy.Handle<Exception>().
            WaitAndRetryAsync(retryCountValue, retryCount => TimeSpan.FromMilliseconds(Math.Pow(EXPONENT, retrySecondsValue * 10)), onRetry: (timespan, retryCount) =>
            {

            });
        }

        public AsyncRetryPolicy GetRetryPolicy(string method)
        {
            int retryCountValue = GetRetryCount();

            double retrySecondsValue = GetRetrySeconds();

            return Policy.Handle<Exception>().
              WaitAndRetryAsync(retryCountValue, retryCount => TimeSpan.FromSeconds(Math.Pow(EXPONENT, retrySecondsValue)), onRetry: (timespan, retryCount) =>
              {

              });
        }

        private double GetRetrySeconds()
        {
            var retrySeconds = configuration.GetSection("RetryPolicy:RetrySeconds").Value;

            if (!double.TryParse(retrySeconds, out double retrySecondsValue))
            {
                throw new InvalidCastException($"El valor de la variable de configuracion RetrySeconds: {retrySeconds} no es valido");
            }

            return retrySecondsValue;
        }

        private int GetRetryCount()
        {
            var retryCount = configuration.GetSection("RetryPolicy:RetryCount").Value;

            if (!int.TryParse(retryCount, out int retryCountValue))
            {
                throw new InvalidCastException($"El valor de la variable de configuracion RetryCount: {retryCount} no es valido ");
            }

            return retryCountValue;
        }
    }
}
