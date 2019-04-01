using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CommentSold.WebTest.Dto;

namespace CommentSold.WebTest.Data
{
    public class Product
    {
        public int Id { get; private set; }

        [Required][MaxLength(255, ErrorMessage = "The {0} shouldn't have more than {1} characters")]
        public string ProductName { get; private set; }

        [Required][MaxLength(255, ErrorMessage = "The {0} shouldn't have more than {1} characters")]
        public string Description { get; private set; }

        [Required][MaxLength(255, ErrorMessage = "The {0} shouldn't have more than {1} characters")]
        public string Style { get; private set; }

        [Required][MaxLength(255, ErrorMessage = "The {0} shouldn't have more than {1} characters")]
        public string Brand { get; private set; }

        [MaxLength(2000, ErrorMessage = "The {0} shouldn't have more than {1} characters")]
        public string Url { get; private set; }

        [Required][MaxLength(50, ErrorMessage = "The {0} shouldn't have more than {1} characters")]
        public string ProductType { get; private set; }

        [Required][Range(0,Constants.MaxPriceInCents, ErrorMessage = "The {0} must be between {1} and {2}")]
        public uint ShippingPriceCents { get; private set; }

        [MaxLength(1000, ErrorMessage = "The {0} shouldn't have more than {1} characters")]
        public string Note { get; private set; }

        [Required]
        public ApplicationIdentityUser Admin { get; private set; }

        public IReadOnlyCollection<Inventory> Inventories { get; private set; }
        public DateTime CreatedAt { get; private set; }

        [ConcurrencyCheck]
        public DateTime UpdatedAt { get; private set; }

        private Product() {}

        public Product(string productName, string description, string style, string brand, string url, string productType, uint shippingPriceCents, string note, ApplicationIdentityUser admin)
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
            Inventories = new List<Inventory>();
        }

        public void Update(ProductChangeDto p)
        {
            ProductName = p.ProductName;
            ProductType = p.ProductType;
            Description = p.Description;
            Style = p.Style;
            Brand = p.Brand;
            Url = p.Url;
            ShippingPriceCents = p.ShippingPriceCents;
            Note = p.Note;
            return;
        }
    }
}
