using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Net.Http.Json;

namespace InvoiceGenerator_dotnet_maui_UI.ViewModels
{
    public partial class ClientDetailsViewModel : BaseViewModel
    {
        private readonly string _apiBaseUrl = "https://new-invoice-gen-webapi.azurewebsites.net";

        public ObservableCollection<ClientViewModel> Clients { get; } = new();

        [ObservableProperty]
        bool isRefreshing;

        [RelayCommand]
        public async Task GetClients()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                var allClients = await GetClientsFromApi();
                if (Clients.Count != 0)
                    Clients.Clear();

                foreach (var c in allClients)
                {
                    Clients.Add(c);
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                IsRefreshing = false;
                IsBusy = false;
            }
        }

        private async Task<List<ClientViewModel>> GetClientsFromApi()
        {
            return await GetMethod<List<ClientViewModel>>("/api/client");
        }

        private async Task<T> GetMethod<T>(string apiAddress)
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
