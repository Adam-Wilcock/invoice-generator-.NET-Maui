using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InvoiceGenerator.Maui.BusinessLogic;
using System.Collections.ObjectModel;

namespace InvoiceGenerator_dotnet_maui_UI.ViewModels
{
    public partial class InvoiceGenerationViewModel : BaseViewModel
    {
        [ObservableProperty]
        private bool _areClientNamesLoading;

        [ObservableProperty]
        public LineItemDisplayModel _lineItemVm = new();

        [ObservableProperty]
        public string _vat;

        public ObservableCollection<ClientNameViewModel> ClientNames { get; } = new ObservableCollection<ClientNameViewModel>();

        public ObservableCollection<LineItemDisplayModel> LineItems { get; } = new ObservableCollection<LineItemDisplayModel>();

        private readonly IClientService _clientService;
        public InvoiceGenerationViewModel(IClientService clientService)
        {
            _clientService = clientService;
            GetClientNames();
        }

        public double CalculateTotalValue()
        {
            return LineItems.Sum(x => x.Total);
        }

        public double CalculateInvoiceTotal()
        {
            var totalValue = CalculateTotalValue();

            double invoiceTotal = totalValue + (totalValue * double.Parse(_vat) / 100);

            return invoiceTotal;
        }

        [RelayCommand]
        public void GetClientNames()
        {
            _areClientNamesLoading = true;

            var allClientNames = _clientService.GetClientNames();

            if (ClientNames.Count != 0)
            {
                ClientNames.Clear();
            }

            foreach (var clientName in allClientNames)
            {
                var newVm = new ClientNameViewModel
                {
                    ClientName = clientName.ClientName,
                    Id = clientName.ClientId
                };
                ClientNames.Add(newVm);
            }

            _areClientNamesLoading = false;
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
        public int Quantity { get; set; }
        public double Total => Cost * Quantity;
    }
}
