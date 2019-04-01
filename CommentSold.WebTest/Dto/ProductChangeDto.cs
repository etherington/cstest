using System.ComponentModel.DataAnnotations;

namespace CommentSold.WebTest.Dto
{
    /// <summary>
    /// Base class for data transfer object for Product change (update/create) commands.
    /// </summary>
    public class ProductChangeDto
    {
        [Required]
        [MaxLength(255, ErrorMessage = "The {0} shouldn't have more than {1} characters")]
        public string ProductName { get; set; }

        [Required]
        [MaxLength(255, ErrorMessage = "The {0} shouldn't have more than {1} characters")]
        public string Description { get; set; }

        [Required]
        [MaxLength(255, ErrorMessage = "The {0} shouldn't have more than {1} characters")]
        public string Style { get; set; }

        [Required]
        [MaxLength(255, ErrorMessage = "The {0} shouldn't have more than {1} characters")]
        public string Brand { get; set; }

        [MaxLength(2000, ErrorMessage = "The {0} shouldn't have more than {1} characters")]
        public string Url { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "The {0} shouldn't have more than {1} characters")]
        public string ProductType { get; set; }

        [Required]
        [Range(0, Constants.MaxPriceInCents, ErrorMessage = "The {0} must be between {1} and {2}")]
        public uint ShippingPriceCents { get; set; }

        [MaxLength(1000, ErrorMessage = "The {0} shouldn't have more than {1} characters")]
        public string Note { get; set; }
    }
}
