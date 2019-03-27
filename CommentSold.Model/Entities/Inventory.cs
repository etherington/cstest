using System;
using System.Collections.Generic;

namespace CommentSold.Model.Entities
{
    public class Inventory: Entity
    {
        public string Sku { get; private set; }

        public ushort Quantity { get; private set; }

        public uint PriceCents { get; private set; }
        public uint SalePriceCents { get; private set; }

        public string Color { get; private set; }
        public string Size { get; private set; }
        public uint Weight { get; private set; }
        public uint Length { get; private set; }
        public uint Width { get; private set; }
        public uint Height { get; private set; }
        public string Note { get; private set; }

        public Product Product { get; private set; }
        public IReadOnlyCollection<Order> Orders { get; private set; }

        public Inventory(string sku, ushort quantity, uint priceCents, uint salePriceCents, string color, string size, uint weight, uint length, uint width, uint height, string note, Product product, IReadOnlyCollection<Order> orders)
        {
            Sku = sku ?? throw new ArgumentNullException(nameof(sku));
            Quantity = quantity;
            PriceCents = priceCents;
            SalePriceCents = salePriceCents;
            Color = color ?? throw new ArgumentNullException(nameof(color));
            Size = size ?? throw new ArgumentNullException(nameof(size));
            Weight = weight;
            Length = length;
            Width = width;
            Height = height;
            Note = note ?? throw new ArgumentNullException(nameof(note));
            Product = product ?? throw new ArgumentNullException(nameof(product));
            Orders = orders ?? throw new ArgumentNullException(nameof(orders));
        }
    }
}
