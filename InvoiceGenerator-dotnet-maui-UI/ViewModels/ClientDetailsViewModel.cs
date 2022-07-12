using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InvoiceGenerator_dotnet_maui_UI.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace InvoiceGenerator_dotnet_maui_UI.ViewModels
{
    public partial class ClientDetailsViewModel : BaseViewModel
    {
        private readonly ClientService clientService;

        public ObservableCollection<ClientViewModel> Clients { get; } = new();

        [ObservableProperty]
        bool isRefreshing;

        public ClientDetailsViewModel(ClientService clientService)
        {
            this.clientService = clientService;
        }

        [RelayCommand]
        public async Task GetClients()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                var allClients = await clientService.GetClientsFromApi();

                if (Clients.Count != 0)
                    Clients.Clear();

                foreach (var c in allClients)
                {
                    Clients.Add(c);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Unable to get clients: {e.Message}");
                await Shell.Current.DisplayAlert("Error!", e.Message, "OK"); 
            }
            finally
            {
                IsRefreshing = false;
                IsBusy = false;
            }
        }

    }
}
