using InvoiceGenerator_dotnet_maui_UI.ViewModels;

namespace InvoiceGenerator_dotnet_maui_UI;

public partial class ClientDetailsEntryPage : ContentPage
{
	public ClientDetailsEntryPage(ClientEntryViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
	}

    private void txt_clientName_TextChanged(object sender, TextChangedEventArgs e)
    {
        ((ClientEntryViewModel)this.BindingContext).ClientDetails.clientName = txt_clientName.Text;
    }

    private void txt_clientAddress_TextChanged(object sender, TextChangedEventArgs e)
    {
        ((ClientEntryViewModel)this.BindingContext).ClientDetails.clientAddress = txt_clientAddress.Text;
    }

    private void txt_contactName_TextChanged(object sender, TextChangedEventArgs e)
    {
        ((ClientEntryViewModel)this.BindingContext).ClientDetails.contactName = txt_contactName.Text;
    }

    private void txt_contactEmail_TextChanged(object sender, TextChangedEventArgs e)
    {
        ((ClientEntryViewModel)this.BindingContext).ClientDetails.contactEmail = txt_contactEmail.Text;
    }

    private void btn_Create_Clicked(object sender, EventArgs e)
    {
        txt_clientName.Text = string.Empty;
        txt_clientAddress.Text = string.Empty;
        txt_contactName.Text = string.Empty;
        txt_contactEmail.Text = string.Empty;
    }
}