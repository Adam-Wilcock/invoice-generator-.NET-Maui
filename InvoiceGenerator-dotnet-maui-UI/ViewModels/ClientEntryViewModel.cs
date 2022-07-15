using CommunityToolkit.Mvvm.Input;
using InvoiceGenerator_dotnet_maui_UI.Services;
using System.Collections.ObjectModel;

namespace InvoiceGenerator_dotnet_maui_UI.ViewModels
{
    public partial class ClientEntryViewModel   : BaseViewModel
    {
        private readonly ClientService clientService;

        public ClientCreationViewModel ClientDetails { get; } = new ClientCreationViewModel();

        public ClientEntryViewModel(ClientService clientService)
        {
            this.clientService = clientService;
        }

        [RelayCommand]
        public async Task CreateNewClient()
        {
            IsBusy = true;

            var response = await clientService.AddClientIntoApi(ClientDetails);

            IsBusy = false;
        }
    }
}
