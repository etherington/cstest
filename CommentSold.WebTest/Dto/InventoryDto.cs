using System.ComponentModel;

namespace CommentSold.WebTest.Dto
{
    /// <summary>
    /// Data transfer object for Inventory queries.
    /// </summary>
    public class InventoryDto
    {
        public int Id { get; set; }
        [DisplayName("Product Name")]
        public string ProductName { get; set; }
        [DisplayName("Product Id")]
        public int ProductId { get; set; }

        public string Sku { get; set; }

        public ushort Quantity { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }

        public uint PriceCents { get; set; }
        public uint CostCents { get; set; }

        public string PriceInDollars
         => string.Format(new System.Globalization.CultureInfo("en-US"), "{0:C}", PriceCents/100.0);
        public string CostInDollars
            => string.Format(new System.Globalization.CultureInfo("en-US"), "{0:C}", CostCents / 100.0);
    }
}
