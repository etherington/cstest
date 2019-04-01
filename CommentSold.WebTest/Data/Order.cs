using System;
using System.ComponentModel.DataAnnotations;

namespace CommentSold.WebTest.Data
{
    public class Order
    {
        public int Id { get; private set; }

        [Required][MaxLength(255, ErrorMessage = "The {0} shouldn't have more than {1} characters")]
        public string StreetAddress { get; private set; }

        [MaxLength(255, ErrorMessage = "The {0} shouldn't have more than {1} characters")]
        public string Apartment { get; private set; }

        [MaxLength(255, ErrorMessage = "The {0} shouldn't have more than {1} characters")]
        public string City { get; private set; } 

        [Required][StringLength(2, ErrorMessage = "The {0} must be {1} characters")]
        public string State { get; private set; }

        [Required][StringLength(2, ErrorMessage = "The {0} must be {1} characters")]
        public string CountryCode { get; private set; }

        [Required][MaxLength(20, ErrorMessage = "The {0} shouldn't have more than {1} characters")]
        public string Zip { get; private set; }

        [Required][MaxLength(255, ErrorMessage = "The {0} shouldn't have more than {1} characters")]
        public string Email { get; private set; }

        [Required][MaxLength(255, ErrorMessage = "The {0} shouldn't have more than {1} characters")]
        public string Name { get; private set; }

        [Required][MaxLength(50, ErrorMessage = "The {0} shouldn't have more than {1} characters")]
        public string OrderStatus { get; private set; }

       [MaxLength(255, ErrorMessage = "The {0} shouldn't have more than {1} characters")]
        public string TransactionId { get; private set; }

        [Range(0, Constants.MaxPriceInCents, ErrorMessage = "The {0} must be between {1} and {2}")]
        public ushort PaymentAmountCents { get; private set; }

        [Range(0, Constants.MaxPriceInCents, ErrorMessage = "The {0} must be between {1} and {2}")]
        public uint? ShipChargedCents { get; private set; }

        [Range(0, Constants.MaxPriceInCents, ErrorMessage = "The {0} must be between {1} and {2}")]
        public uint? ShipCostCents { get; private set; }

        [Range(0, Constants.MaxPriceInCents, ErrorMessage = "The {0} must be between {1} and {2}")]
        public uint SubTotalCents { get; private set; }

        [Range(0, Constants.MaxPriceInCents, ErrorMessage = "The {0} must be between {1} and {2}")]
        public uint TotalCents { get; private set; }

        [MaxLength(255, ErrorMessage = "The {0} shouldn't have more than {1} characters")]
        public string ShipperName { get; private set; }
        public DateTime? PaymentDate { get; private set; }
        public DateTime? ShippedDate { get; private set; }

        [MaxLength(50, ErrorMessage = "The {0} shouldn't have more than {1} characters")]
        public string TrackingNumber { get; private set; }
        [Range(0, Constants.MaxPriceInCents, ErrorMessage = "The {0} must be between {1} and {2}")]
        public uint TaxTotalCents { get; private set; }
        public DateTime CreatedAt { get; private set; }
        [ConcurrencyCheck]
        public DateTime UpdatedAt { get; private set; }
        
        [Required]
        public Inventory Inventory { get; private set; }
    }
}
