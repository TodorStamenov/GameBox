using Blazored.Toast.Services;
using GameBox.Admin.UI.Model;
using GameBox.Admin.UI.Services.Contracts;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace GameBox.Admin.UI.Services
{
    public class HttpClientService : IHttpClientService
    {
        private readonly HttpClient http;
        private readonly IToastService toastService;
        private readonly ILocalStorageService localStorage;

        public HttpClientService(
            HttpClient http,
            IToastService toastService,
            ILocalStorageService localStorage)
        {
            this.http = http;
            this.toastService = toastService;
            this.localStorage = localStorage;
        }

        public async Task<T> GetAsync<T>(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            return await this.SendRequest<T>(request);
        }

        public async Task<T> PostAsync<T>(string url, object body)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            return await this.SendRequest<T>(request, body);
        }

        public async Task<T> PutAsync<T>(string url, object body)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, url);
            return await this.SendRequest<T>(request, body);
        }

        public async Task<T> DeleteAsync<T>(string url) 
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, url);
            return await this.SendRequest<T>(request);
        }

        private async Task<T> SendRequest<T>(HttpRequestMessage request, object body = null)
        {
            var token = await this.GetTokenAsync();

            if (!string.IsNullOrWhiteSpace(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);                
            }

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (body != null)
            {
                request.Content = new StringContent(JsonSerializer.Serialize(body));
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");                
            }

            var response = await this.http.SendAsync(request);
            var responseAsString = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var errors = JsonSerializer.Deserialize<ErrorResponseModel>(responseAsString).Errors;
                var errorsAsString = string.Join(Environment.NewLine, errors);

                this.toastService.ShowError(errorsAsString);

                throw new Exception(responseAsString);
            }

            if (typeof(T).Name == typeof(string).Name && responseAsString.Length > 0)
            {
                this.toastService.ShowSuccess(responseAsString);
                return default;
            }

            return JsonSerializer.Deserialize<T>(responseAsString);
        }

        private async Task<string> GetTokenAsync()
        {
            if (await this.localStorage.ContainsKeyAsync("currentUser"))
            {
                var user = await this.localStorage.GetItemAsync<UserModel>("currentUser");
                return user.Token;
            }

            return string.Empty;
        }
    }
}