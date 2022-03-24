using System;
using System.Collections.Generic;

namespace BeyondOneWebAPI.Models
{
    public partial class Product
    {
        public int Id { get; set; }
        public string ProductId { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public int StockAvailable { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }
    }
}
