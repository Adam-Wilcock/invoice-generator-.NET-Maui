using InvoiceGenerator_dotnet_maui_UI.ViewModels;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace InvoiceGenerator_dotnet_maui_UI;

public partial class ClientDetailsViewPage : ContentPage
{
    public ClientDetailsViewPage(ClientDetailsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}