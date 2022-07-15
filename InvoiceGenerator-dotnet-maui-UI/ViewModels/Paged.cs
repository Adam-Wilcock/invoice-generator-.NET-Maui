
namespace InvoiceGenerator_dotnet_maui_UI.ViewModels
{
    public class Paged<T> where T : class
    {
        public List<T> Data { get; set; } = new List<T>();
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
    }
}
