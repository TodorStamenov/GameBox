using Blazored.LocalStorage;
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

        public Task<T> GetAsync<T>(string url)
        {
            throw new System.NotImplementedException();
        }

        public async Task<T> PostAsync<T>(string url, object body)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);

            var token = await this.GetTokenAsync();

            if (token != null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);                
            }

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Content = new StringContent(JsonSerializer.Serialize(body));
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await this.http.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                await this.HandleSuccessResponse(response);
            }
            else
            {
                await this.HandleErrorResponse(response);
            }

            var responseAsString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(responseAsString);
        }

        public Task<T> PutAsync<T>(string url, object body)
        {
            throw new System.NotImplementedException();
        }

        public Task<T> DeleteAsync<T>(string url)
        {
            throw new System.NotImplementedException();
        }

        private async Task<string> GetTokenAsync()
        {
            if (await this.localStorage.ContainKeyAsync("currentUser"))
            {
                var user = await this.localStorage.GetItemAsync<UserModel>("currentUser");
                return user.Token;
            }

            return null;
        }

        private async Task HandleSuccessResponse(HttpResponseMessage responseMessage)
        {
            var responseAsString = await responseMessage.Content.ReadAsStringAsync();
            var response = JsonSerializer.Deserialize<SuccessResponseModel>(responseAsString);

            this.toastService.ShowSuccess(response.Message);
        }

        private async Task HandleErrorResponse(HttpResponseMessage responseMessage)
        {
            var statusCode = (int)responseMessage.StatusCode;
            var responseAsString = await responseMessage.Content.ReadAsStringAsync();
            var response = JsonSerializer.Deserialize<ErrorResponseModel>(responseAsString);

            switch (statusCode)
            {
                case 400:
                case 401:
                case 404:
                case 500:
                    this.toastService.ShowError(string.Join(string.Empty, response.Error));
                    break;
            }

            throw new Exception($"Requested API returned status code {statusCode}");
        }
    }
}