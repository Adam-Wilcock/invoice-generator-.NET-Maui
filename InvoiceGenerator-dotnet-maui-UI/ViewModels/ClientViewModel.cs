
namespace InvoiceGenerator_dotnet_maui_UI.ViewModels
{
    public class ClientViewModel
    {
        public Guid ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientAddress { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
    }

    public class ClientCreationViewModel
    {
        public string clientName { get; set; }
        public string clientAddress { get; set; }
        public string contactName { get; set; }
        public string contactEmail { get; set; }
    }

}
