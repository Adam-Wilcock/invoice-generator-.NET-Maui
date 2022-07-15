namespace InvoiceGenerator_dotnet_maui_UI;

public partial class ClientDetailsPage : ContentPage
{
    private readonly ClientDetailsEntryPage _clientDetailsEntryPage;
    private readonly ClientDetailsViewPage _clientDetailsViewPage;
	public ClientDetailsPage(ClientDetailsViewPage clientDetailsViewPage, ClientDetailsEntryPage clientDetailsEntryPage)
    {
        InitializeComponent();
        _clientDetailsViewPage = clientDetailsViewPage;
        _clientDetailsEntryPage = clientDetailsEntryPage;
    }

    private async void btn_enterClientDetails_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(_clientDetailsEntryPage);
    }

    private async void btn_viewClientDetails_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(_clientDetailsViewPage);
    }
}