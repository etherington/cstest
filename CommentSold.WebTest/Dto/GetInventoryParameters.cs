namespace CommentSold.WebTest.Dto
{
    /// <summary>
    /// Specifies the filter parameters for the GetInventoryForUser query.
    /// </summary>
    public class GetInventoryParameters: GetPagedListParameters
    {
        public string Sku { get; set; }
        public uint? MaximumQuantity { get; set; } = null;
        public int? ProductId { get; set; }
    }
}
