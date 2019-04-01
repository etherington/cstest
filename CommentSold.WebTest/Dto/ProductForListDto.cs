using System.Collections.Generic;
using System.ComponentModel;

namespace CommentSold.WebTest.Dto
{
    /// <summary>
    /// Data transfer object for Product create command.
    /// </summary>
    public class ProductForListDto
    {
        public int Id { get; set; }
        [DisplayName("Product Name")]
        public string ProductName { get; set; }
        public string Style { get;  set; }
        public string Brand { get;  set; }
        
        public List<SkuDto> Inventories { get; set; }
       
    }
}
