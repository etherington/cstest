namespace CommentSold.WebTest.Dto
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
  
        public string Style { get;  set; }
        public string Brand { get;  set; }

        //optional: available SKUs

        //  public string Description { get;  set; }
        //  public string Url { get;  set; }
        //   public string ProductType { get;  set; }
        //  public uint ShippingPriceCents { get;  set; }
        //    public string Note { get;  set; }
        //     public DateTime CreatedAt { get;  set; }
        //    public DateTime UpdatedAt { get;  set; }
    }
}
