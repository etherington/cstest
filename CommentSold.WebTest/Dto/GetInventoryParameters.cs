namespace CommentSold.WebTest.Dto
{
    public class GetInventoryParameters: GetPagedListParameters
    {
        public string Sku { get; set; }
        public uint? MaximumQuantity { get; set; } = null;
        public int? ProductId { get; set; }
    }
}
