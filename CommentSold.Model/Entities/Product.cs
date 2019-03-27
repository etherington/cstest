using System;
using System.Collections.Generic;

namespace CommentSold.Model.Entities
{
    public class Product: Entity
    {
        public string ProductName { get; private set; }
        public string Description { get; private set; }
        public string Style { get; private set; }
        public string Brand { get; private set; }
        public string Url { get; private set; }
        public string ProductType { get; private set;}
        public uint ShippingPriceCents { get; private set; }
        public string Note { get; private set; }
        public User Admin { get; private set; }
        public IReadOnlyCollection<Inventory> Inventories { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public Product(string productName, string description, string style, string brand, string url, string productType, uint shippingPriceCents, string note, User admin, IReadOnlyCollection<Inventory> inventories, DateTime createdAt, DateTime updatedAt)
        {
            ProductName = productName ?? throw new ArgumentNullException(nameof(productName));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Style = style ?? throw new ArgumentNullException(nameof(style));
            Brand = brand ?? throw new ArgumentNullException(nameof(brand));
            Url = url ?? throw new ArgumentNullException(nameof(url));
            ProductType = productType ?? throw new ArgumentNullException(nameof(productType));
            ShippingPriceCents = shippingPriceCents;
            Note = note ?? throw new ArgumentNullException(nameof(note));
            Admin = admin ?? throw new ArgumentNullException(nameof(admin));
            Inventories = inventories ?? throw new ArgumentNullException(nameof(inventories));
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}

