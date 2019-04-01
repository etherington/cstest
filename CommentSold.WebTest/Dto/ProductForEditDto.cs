using System;

namespace CommentSold.WebTest.Dto
{
    /// <summary>
    /// Data transfer object for Product update command.
    /// </summary>
    public class ProductForEditDto: ProductChangeDto
    {
         public int Id { get; set; }
         public DateTime UpdatedAt { get; set; }
    }
}
