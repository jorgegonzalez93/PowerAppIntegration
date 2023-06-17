using Migration.Domain.Infrastructure.Exceptions;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace Migration.Domain.Infrastructure.Adapters
{
    public class GenericHttpRequest
    {
        private const int EXPONENT = 2;
        private const string REQUEST_ERROR_REST = "Error llamado";
        public async Task<T> PostDataAsync<T>(string path, object content, string ocpSubscriptionKey) where T : class, new()
        {
            using (var restClient = new HttpClient())
            {
                AddHeaderHttpClient(restClient, ocpSubscriptionKey);

                return await GetRetryPolicy(MethodBase.GetCurrentMethod().Name).ExecuteAsync(async () =>
                {
                    var response = await restClient.PostAsync(path, ObjectAsStringContent(content)).ConfigureAwait(false);
                    return await ProcessResponseAsync<T>(response).ConfigureAwait(false);
                }).ConfigureAwait(false);
            }

        }

        public async Task<T> DynamicFromPostDataAsync<T>(string path, object content)
        {
            using (var restClient = new HttpClient())
            {
                return await GetRetryPolicy(MethodBase.GetCurrentMethod().Name).ExecuteAsync(async () =>
                {
                    var response = await restClient.PostAsync(path, ObjectAsStringContent(content)).ConfigureAwait(false);
                    return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
                }).ConfigureAwait(false);
            }

        }

        public async Task<T> GetDataAsync<T>(string path, string ocpSubscriptionKey) where T : class, new()
        {
            using (var restClient = new HttpClient())
            {
                AddHeaderHttpClient(restClient, ocpSubscriptionKey);

                return await GetRetryPolicy(MethodBase.GetCurrentMethod().Name).ExecuteAsync(async () =>
                {
                    var response = await restClient.GetAsync(path).ConfigureAwait(false);
                    return await ProcessResponseAsync<T>(response).ConfigureAwait(false);
                }).ConfigureAwait(false);
            }
        }

        private static async Task<T> ProcessResponseAsync<T>(HttpResponseMessage response) where T : class, new()
        {
            var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRestClientException(string.Format(CultureInfo.InvariantCulture, REQUEST_ERROR_REST,
                    response.RequestMessage?.Method, response.RequestMessage?.RequestUri, response.StatusCode.GetHashCode(), result), response);
            }

            if (string.IsNullOrWhiteSpace(result))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<T>(result);
        }

        private static void AddHeaderHttpClient(HttpClient httpClient, string ocpSubscriptionKey)
        {
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Ocp-Apim-Subscription-Key", ocpSubscriptionKey);
        }


        private AsyncRetryPolicy GetRetryPolicy(string method)
        {
            int retryCountValue = GetRetryCount();

            double retrySecondsValue = GetRetrySeconds();

            return Policy.Handle<Exception>().
              WaitAndRetryAsync(retryCountValue, retryCount => TimeSpan.FromSeconds(Math.Pow(EXPONENT, retrySecondsValue)), onRetry: (timespan, retryCount) =>
              {

              });
        }

        private int GetRetryCount()
        {
            var retryCount = "3";

            if (!int.TryParse(retryCount, out int retryCountValue))
            {
                throw new InvalidCastException($"El valor de la variable de configuracion RetryCount: {retryCount} no es valido ");
            }

            return retryCountValue;
        }

        private double GetRetrySeconds()
        {
            var retrySeconds = "2";

            if (!double.TryParse(retrySeconds, out double retrySecondsValue))
            {
                throw new InvalidCastException($"El valor de la variable de configuracion RetrySeconds: {retrySeconds} no es valido");
            }

            return retrySecondsValue;
        }

        private static StringContent ObjectAsStringContent(object obj)
        {
            string objectJSON = JsonConvert.SerializeObject(obj);
            return new StringContent(objectJSON, Encoding.UTF8, "application/json");
        }

    }
}
