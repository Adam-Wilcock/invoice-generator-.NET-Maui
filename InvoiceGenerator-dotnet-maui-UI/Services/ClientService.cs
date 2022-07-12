using InvoiceGenerator_dotnet_maui_UI.ViewModels;
using System.Net.Http.Json;

namespace InvoiceGenerator_dotnet_maui_UI.Services
{
    public class ClientService
    {
        private readonly string _apiBaseUrl = "https://new-invoice-gen-webapi.azurewebsites.net";

        public async Task<List<ClientViewModel>> GetClientsFromApi()
        {
            return await GetClientsMethod<List<ClientViewModel>>("/api/client");
        }

        private async Task<T> GetClientsMethod<T>(string apiAddress)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_apiBaseUrl);
                var clientsResponse = await client.GetAsync(apiAddress);

                clientsResponse.EnsureSuccessStatusCode();
                return await clientsResponse.Content.ReadFromJsonAsync<T>();
            }
        }

        public async Task<List<ClientNameViewModel>> GetClientNamesFromApi()
        {
            return await GetClientNamesMethod<List<ClientNameViewModel>>("/api/client");
        }

        private async Task<T> GetClientNamesMethod<T>(string apiAddress)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_apiBaseUrl);
                var clientsResponse = await client.GetAsync(apiAddress);

                clientsResponse.EnsureSuccessStatusCode();
                return await clientsResponse.Content.ReadFromJsonAsync<T>();
            }
        }
    }
}
