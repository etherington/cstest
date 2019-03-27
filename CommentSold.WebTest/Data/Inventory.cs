using System.Collections.Generic;

namespace CommentSold.WebTest.Data
{
    public class Inventory
    {
        public int Id { get; private set; }
        public string Sku { get; private set; }

        public ushort Quantity { get; private set; }

        public uint PriceCents { get; private set; }
        public uint SalePriceCents { get; private set; }
        public uint CostCents { get; private set; }


        public string Color { get; private set; }
        public string Size { get; private set; }
        public uint Weight { get; private set; }
        public uint Length { get; private set; }
        public uint Width { get; private set; }
        public uint Height { get; private set; }
        public string Note { get; private set; }

        public Product Product { get; private set; }
        public IReadOnlyCollection<Order> Orders { get; private set; }
    }
}
