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
        public Paged<ClientViewModel> PagedViewModel { get; set; } = new();

        [ObservableProperty]
        bool isRefreshing;

        public ClientDetailsViewModel(ClientService clientService)
        {
            this.clientService = clientService;
            PagedViewModel.PageNumber = 1;
        }

        [RelayCommand]
        public async Task GetClients()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                PagedViewModel = await clientService.GetPageFromApi(PagedViewModel.PageNumber);

                UpdateClientsListForViewModel();
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

        [RelayCommand]
        public async Task GetNextPage()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                var targetPageNumber = PagedViewModel.PageNumber >= PagedViewModel.TotalPages
                    ? PagedViewModel.TotalPages
                    : PagedViewModel.PageNumber + 1;

                PagedViewModel = await clientService.GetPageFromApi(targetPageNumber);

                UpdateClientsListForViewModel();
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

        [RelayCommand]
        public async Task GetPreviousPage()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                var targetPageNumber = PagedViewModel.PageNumber <= 1
                    ? 1
                    : PagedViewModel.PageNumber - 1;

                PagedViewModel = await clientService.GetPageFromApi(targetPageNumber);

                UpdateClientsListForViewModel();
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

        private void UpdateClientsListForViewModel()
        {
            if (Clients.Count != 0)
                Clients.Clear();

            foreach (var c in PagedViewModel.Data)
            {
                Clients.Add(c);
            }
        }

    }
}
