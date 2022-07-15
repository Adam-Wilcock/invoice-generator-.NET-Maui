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

        [ObservableProperty]
        bool canPage = false;

        [ObservableProperty]
        string currentPage = string.Empty;

        public ClientDetailsViewModel(ClientService clientService)
        {
            this.clientService = clientService;
        }

        [RelayCommand]
        public async Task GetClients(string choice)
        {
            if (IsBusy)
                return;

            if (!AllowPaging(choice))
                return;

            try
            {
                IsBusy = true;
                IsRefreshing = true;

                int targetPageNumber = GetPageNumber(choice);
                    
                PagedViewModel = await clientService.GetPageFromApi(targetPageNumber);
                UpdateClientsListForViewModel();

                CurrentPage = $"Page {PagedViewModel.PageNumber} of {PagedViewModel.TotalPages}";
                CanPage = true;
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

        private bool AllowPaging(string choice)
        {
            if (choice.Equals("first", StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }
            if (choice.Equals("next", StringComparison.InvariantCultureIgnoreCase))
            {
                return PagedViewModel.PageNumber == PagedViewModel.TotalPages
                        ? false
                        : true;
            }
            if (choice.Equals("previous", StringComparison.InvariantCultureIgnoreCase))
            {
                return PagedViewModel.PageNumber == 1
                        ? false
                        : true;
            }

            // Does not match above, return false as it should've at least matched one of the above
            return false;
        }

        private int GetPageNumber(string choice)
        {
            switch (choice.ToLower())
            {
                case "first":
                    return 1;
                case "next":
                    return PagedViewModel.PageNumber >= PagedViewModel.TotalPages
                            ? PagedViewModel.TotalPages
                            : PagedViewModel.PageNumber + 1;
                case "previous":
                    return PagedViewModel.PageNumber <= 1
                            ? 1
                            : PagedViewModel.PageNumber - 1;
                default:
                    return 1;
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
