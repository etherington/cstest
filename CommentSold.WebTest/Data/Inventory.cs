using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CommentSold.WebTest.Data
{
    public class Inventory
    {
        public int Id { get; private set; }

        [Required][StringLength(6, ErrorMessage = "The {0} must be {1} characters")]
        public string Sku { get; private set; }

        [Required][Range(0,Constants.MaxQuantity, ErrorMessage = "The {0} must be between {1} and {2}")]
        public ushort Quantity { get; private set; }

        [Required][Range(0, Constants.MaxPriceInCents, ErrorMessage = "The {0} must be between {1} and {2}")]
        public uint PriceCents { get; private set; }

        [Range(0, Constants.MaxPriceInCents, ErrorMessage = "The {0} must be between {1} and {2}")]
        public uint SalePriceCents { get; private set; }

        [Required]
        [Range(0, Constants.MaxPriceInCents, ErrorMessage = "The {0} must be between {1} and {2}")]
        public uint CostCents { get; private set; }

        [MaxLength(50, ErrorMessage = "The {0} shouldn't have more than {1} characters")]
        public string Color { get; private set; }

        [MaxLength(50, ErrorMessage = "The {0} shouldn't have more than {1} characters")]
        public string Size { get; private set; }

        [Required]
        [Range(0, Constants.MaxWeight, ErrorMessage = "The {0} must be between {1} and {2}")]
        public uint Weight { get; private set; }
        [Required]
        [Range(0, Constants.MaxDimension, ErrorMessage = "The {0} must be between {1} and {2}")]
        public uint Length { get; private set; }
        [Required]
        [Range(0, Constants.MaxDimension, ErrorMessage = "The {0} must be between {1} and {2}")]
        public uint Width { get; private set; }
        [Required]
        [Range(0, Constants.MaxDimension, ErrorMessage = "The {0} must be between {1} and {2}")]
        public uint Height { get; private set; }

        [MaxLength(1000, ErrorMessage = "The {0} shouldn't have more than {1} characters")]
        public string Note { get; private set; }

        [Required]
        public Product Product { get; private set; }
        public IReadOnlyCollection<Order> Orders { get; private set; }
    }
}
