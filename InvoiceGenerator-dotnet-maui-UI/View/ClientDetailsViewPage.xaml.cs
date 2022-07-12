using InvoiceGenerator_dotnet_maui_UI.ViewModels;

namespace InvoiceGenerator_dotnet_maui_UI;

public partial class ClientDetailsViewPage : ContentPage
{
    public ClientDetailsViewPage(ClientDetailsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}