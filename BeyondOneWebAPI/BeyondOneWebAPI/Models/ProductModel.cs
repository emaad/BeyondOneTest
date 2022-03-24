namespace BeyondOneWebAPI.Models
{
    /// <summary>
    /// This class created to get values from frontend
    /// </summary>
    public class ProductModel
    {
        public string ProductId { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public int StockAvailable { get; set; }
    }
}
