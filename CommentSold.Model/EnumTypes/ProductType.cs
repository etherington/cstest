namespace CommentSold.Model.EnumTypes
{
    public class ProductType : Enumeration
    {
        public static readonly ProductType Clothing = new ProductType(0, "Clothing");
        
        private ProductType() { }
        private ProductType(int value, string displayName) : base(value, displayName) { }
    }
}
