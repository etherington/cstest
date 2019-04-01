namespace CommentSold.WebTest.Dto
{
    /// <summary>
    /// Data transfer objects for Inventory items collection for the product data transfer object
    /// </summary>
    public class SkuDto
    {
        public int Id { get; set; }
        public string SKU { get; set; }
        public uint Quantity { get; set; }
    }
}
