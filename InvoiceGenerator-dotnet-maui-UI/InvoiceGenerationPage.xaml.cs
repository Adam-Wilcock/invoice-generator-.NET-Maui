using InvoiceGenerator_dotnet_maui_UI.ViewModels;
using System.Text;

namespace InvoiceGenerator_dotnet_maui_UI;

public partial class InvoiceGenerationPage : ContentPage
{
	public InvoiceGenerationPage(InvoiceGenerationViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    private void pcker_clientName_SelectedIndexChanged(object sender, EventArgs e)
    {
        pcker_clientName.TextColor = Color.FromRgb(255, 255, 255);

        var selectedClientViewModel = (ClientNameViewModel)pcker_clientName.SelectedItem;

		var clientName = selectedClientViewModel.ClientName;
		var todayAsString = DateTime.Today.ToString("dd-MM-yyyy");
		txt_invoiceReference.Text = $"RJJ-{clientName}-{todayAsString}"; // Create invoice reference
    }

    private void txt_lineItemDescription_TextChanged(object sender, TextChangedEventArgs e)
    {
		((InvoiceGenerationViewModel)this.BindingContext)._lineItemVm.Description = txt_lineItemDescription.Text;
    }

    private void txt_lineItemCost_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (!string.IsNullOrEmpty(txt_lineItemCost.Text))
        {
            ((InvoiceGenerationViewModel)this.BindingContext)._lineItemVm.Cost = Convert.ToDouble(txt_lineItemCost.Text);
        }
    }

    private void txt_lineItemQuantity_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (!string.IsNullOrEmpty(txt_lineItemQuantity.Text))
        {
            ((InvoiceGenerationViewModel)this.BindingContext)._lineItemVm.Quantity = Convert.ToInt32(txt_lineItemQuantity.Text);
        }
    }

    private async void btn_Generate_Clicked(object sender, EventArgs e)
    {
        var invoiceStringBuilder = new StringBuilder();

        var viewModel = ((InvoiceGenerationViewModel)this.BindingContext);

        foreach(var ItemIn in viewModel.LineItems)
        {
            invoiceStringBuilder
                .Append("Line Item Description: ")
                .AppendLine(ItemIn.Description)
                .Append("Line Item Cost: ")
                .AppendLine(ItemIn.Cost.ToString())
                .Append("Line Item Quantity: ")
                .AppendLine(ItemIn.Quantity.ToString())
                .Append("Line Item Total: ")
                .AppendLine(ItemIn.Total.ToString());

            await DisplayAlert("Line Item Details:", invoiceStringBuilder.ToString(), "OK");
            invoiceStringBuilder.Clear();
        }

        invoiceStringBuilder
            .Append(viewModel.CalculateTotalValue())
            .Append(viewModel.CalculateInvoiceTotal());

        await DisplayAlert("VAT Test:", invoiceStringBuilder.ToString(), "OK");
    }

    private void txt_VATSalesTax_TextChanged(object sender, TextChangedEventArgs e)
    {
        ((InvoiceGenerationViewModel)this.BindingContext)._vat = txt_VATSalesTax.Text;
    }

    private void btn_addLineItem_Clicked(object sender, EventArgs e)
    {
        txt_lineItemDescription.Text = string.Empty;
        txt_lineItemCost.Text = string.Empty;
        txt_lineItemQuantity.Text = string.Empty;
    }
}