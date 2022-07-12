
namespace InvoiceGenerator_dotnet_maui_UI.ViewModels
{
    public class LineItemDisplayModel
    {
        public string Description { get; set; }
        public double Cost { get; set; }
        public double Quantity { get; set; }
        public double Total => Math.Round(Cost * Quantity, 2);
    }
}
