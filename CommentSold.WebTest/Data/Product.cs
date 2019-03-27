using System;
using System.Collections.Generic;

namespace CommentSold.WebTest.Data
{
    public class Product
    {
        public int Id { get; private set; }
        public string ProductName { get; private set; }
        public string Description { get; private set; }
        public string Style { get; private set; }
        public string Brand { get; private set; }
        public string Url { get; private set; }
        public string ProductType { get; private set; }
        public uint ShippingPriceCents { get; private set; }
        public string Note { get; private set; }
        public ApplicationIdentityUser Admin { get; private set; }
        public IReadOnlyCollection<Inventory> Inventories { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
    }
}
