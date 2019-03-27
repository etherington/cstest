namespace CommentSold.WebTest.Dto
{
    public class InventoryDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
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
        
        // public uint SalePriceCents { get; set; }
        //public uint Weight { get; set; }
        //public uint Length { get; set; }
        //public uint Width { get; set; }
        //public uint Height { get; set; }
        //public string Note { get; set; }
    }
}
