using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Net.Http.Json;

namespace InvoiceGenerator_dotnet_maui_UI.ViewModels
{
    public partial class InvoiceGenerationViewModel : BaseViewModel
    {
        private readonly string _apiBaseUrl = "https://new-invoice-gen-webapi.azurewebsites.net";

        [ObservableProperty]
        private bool _areClientNamesLoading;

        [ObservableProperty]
        public LineItemDisplayModel _lineItemVm = new();

        [ObservableProperty]
        public string _vat;

        public ObservableCollection<ClientNameViewModel> ClientNames { get; } = new ObservableCollection<ClientNameViewModel>();

        public ObservableCollection<LineItemDisplayModel> LineItems { get; } = new ObservableCollection<LineItemDisplayModel>();

        public double CalculateTotalValue()
        {
            return LineItems.Sum(x => x.Total);
        }

        public double CalculateInvoiceTotal()
        {
            var totalValue = CalculateTotalValue();

            double invoiceTotal = Math.Round(totalValue + (totalValue * double.Parse(_vat) / 100), 2);

            return invoiceTotal;
        }

        [RelayCommand]
        public async Task GetClientNames()
        {
            _areClientNamesLoading = true;

            var allClientNames = await GetClientNamesFromApi();

            if (ClientNames.Count != 0)
            {
                ClientNames.Clear();
            }

            foreach (var clientName in allClientNames)
            {
                ClientNames.Add(clientName);
            }

            _areClientNamesLoading = false;
        }

        private async Task<List<ClientNameViewModel>> GetClientNamesFromApi()
        {
            return await GetMethod<List<ClientNameViewModel>>("/api/client");
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

        [RelayCommand]
        public void AddLineItems()
        {
            IsBusy = true;

            var newLineItem = new LineItemDisplayModel
            {
                Description = _lineItemVm.Description,
                Cost = _lineItemVm.Cost,
                Quantity = _lineItemVm.Quantity
            };
            LineItems.Add(newLineItem);

            IsBusy = false;
        }
    }

    public class ClientNameViewModel
    {
        public Guid Id { get; set; }
        public string ClientName { get; set; }
    }

    public class LineItemDisplayModel
    {
        public string Description { get; set; }
        public double Cost { get; set; }
        public double Quantity { get; set; }
        public double Total => Math.Round(Cost * Quantity, 2);
    }
}
