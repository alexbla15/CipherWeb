using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace CipherData.ApiMode
{
    public class GeneralAPIRequest
    {
        private static readonly HttpClient client = new();

        private static readonly string protocol = "https";
        private static readonly string domain = "CipherWeb.com";

        private static string GetUrl(string path) => $"{protocol}://{domain}/{path}";

        public static Tuple<T?, ErrorResponse> TransfromResponse<T>(string responseBody, HttpStatusCode code)
        {
            T? responseDeserialized = JsonSerializer.Deserialize<T>(responseBody);

            HttpStatusCode statusCode = code;
            ErrorResponse errorResponse = ErrorResponse.GetErrorResponse(statusCode);

            return Tuple.Create(responseDeserialized, errorResponse);
        }

        public static StringContent GetStringContent(ICipherClass obj) => 
            new(obj.ToJson(), Encoding.UTF8, "application/json");

        /// <summary>
        /// General GET request
        /// </summary>
        public static async Task<Tuple<T?, ErrorResponse>> Get<T>(string path)
        {
            string url = GetUrl(path); 

            HttpResponseMessage response = await client.GetAsync(url);
            string responseBody = await response.Content.ReadAsStringAsync();

            return TransfromResponse<T>(responseBody, response.StatusCode);
        }

        /// <summary>
        /// General POST request
        /// </summary>
        public static async Task<Tuple<T?, ErrorResponse>> Post<T>(string path, ICipherClass newObject)
        {
            string url = GetUrl(path);
            StringContent content = GetStringContent(newObject);

            HttpResponseMessage response = await client.PostAsync(url, content);

            string responseBody = await response.Content.ReadAsStringAsync();

            return TransfromResponse<T>(responseBody, response.StatusCode);
        }

        /// <summary>
        /// General PUT request
        /// </summary>
        public static async Task<Tuple<T?, ErrorResponse>> Put<T>(string path, ICipherClass newObject)
        {
            string url = GetUrl(path);
            StringContent content = GetStringContent(newObject);

            HttpResponseMessage response = await client.PutAsync(url, content);

            string responseBody = await response.Content.ReadAsStringAsync();

            return TransfromResponse<T>(responseBody, response.StatusCode);
        }
    }
}
