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
        if (CheckEmpty(txt_lineItemDescription.Text))
        {
            ((InvoiceGenerationViewModel)this.BindingContext)._lineItemVm.Description = txt_lineItemDescription.Text;
        }
    }

    private void txt_lineItemCost_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (CheckEmpty(txt_lineItemCost.Text) && TryParseToDouble(txt_lineItemCost.Text))
        {
            ((InvoiceGenerationViewModel)this.BindingContext)._lineItemVm.Cost = Convert.ToDouble(txt_lineItemCost.Text);
        }
    }

    private void txt_lineItemQuantity_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (CheckEmpty(txt_lineItemQuantity.Text) && TryParseToDouble(txt_lineItemQuantity.Text))
        {
            ((InvoiceGenerationViewModel)this.BindingContext)._lineItemVm.Quantity = Convert.ToDouble(txt_lineItemQuantity.Text);
        }
    }

    private bool CheckEmpty(string Value)
    {
        bool result;

        if (string.IsNullOrEmpty(Value) && string.IsNullOrWhiteSpace(Value))
        {
            result = false;
        }
        else
        {
            result = true;
        }

        return result;
    }

    private bool TryParseToDouble(string Value)
    {
        double outcome;
        bool result;

        if (double.TryParse(Value, out outcome))
        {
            result = true;
        }
        else
        {
            result = false;
        }

        return result;
    }

    private async void btn_Generate_Clicked(object sender, EventArgs e)
    {
        string caption;

        caption = "Line Item Details:";

        var invoiceStringBuilder = new StringBuilder();

        var viewModel = ((InvoiceGenerationViewModel)this.BindingContext);

        foreach(var Item in viewModel.LineItems)
        {
            invoiceStringBuilder
                .Append("Line Item Description:  ")
                .AppendLine(Item.Description)
                .Append("Line Item Cost:  ")
                .AppendLine("£" + Item.Cost.ToString())
                .Append("Line Item Quantity:  ")
                .AppendLine(Item.Quantity.ToString())
                .Append("Line Item Total:  ")
                .AppendLine("£" + Item.Total.ToString());

            await DisplayAlert(caption, invoiceStringBuilder.ToString(), "OK");
            invoiceStringBuilder.Clear();
        }

        caption = "Invoice:";

        string invoiceReference = txt_invoiceReference.Text;
        string VAT = txt_VATSalesTax.Text;
        string issueDate = DateTime.Today.ToString("dd-MM-yyyy");
        string dueDate = DateTime.Today.AddMonths(1).ToString("dd-MM-yyyy");

        invoiceStringBuilder
            .Append("Invoice Reference:  ")
            .AppendLine(invoiceReference)
            .Append("Total Value:  ")
            .AppendLine("£" + viewModel.CalculateTotalValue().ToString())
            .Append("VAT Rate:  ")
            .AppendLine(VAT + "%")
            .Append("Issue Date:  ")
            .AppendLine(issueDate)
            .Append("Due Date:  ")
            .AppendLine(dueDate)
            .Append("Invoice Total:  ")
            .AppendLine("£" + viewModel.CalculateInvoiceTotal().ToString());

        await DisplayAlert(caption, invoiceStringBuilder.ToString(), "OK");
        invoiceStringBuilder.Clear();

        string message = "Do you wish to create another invoice?";
        caption = "Create Another Invoice?";

        bool answer = await DisplayAlert(caption, message, "Yes", "No");

        if (answer == true) // Yes selected
        {
            txt_VATSalesTax.Text = string.Empty;
            ((InvoiceGenerationViewModel)this.BindingContext).LineItems.Clear();
            txt_lineItemDescription.Focus();
            btn_Generate.IsEnabled = false;
        }
        else // No selected
        {

        }
    }

    private void txt_VATSalesTax_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (CheckEmpty(txt_VATSalesTax.Text) && TryParseToDouble(txt_VATSalesTax.Text))
        {
            ((InvoiceGenerationViewModel)this.BindingContext)._vat = txt_VATSalesTax.Text;
        }
    }

    private void btn_addLineItem_Clicked(object sender, EventArgs e)
    {
        txt_lineItemDescription.Text = string.Empty;
        txt_lineItemCost.Text = string.Empty;
        txt_lineItemQuantity.Text = string.Empty;
    }
}