namespace CommentSold.Model.EnumTypes
{
    public class CardBrand : Enumeration
    {
        public static readonly CardBrand Amex = new CardBrand(0, "Amex");
        public static readonly CardBrand Discover = new CardBrand(1, "Discover");
        public static readonly CardBrand Mastercard = new CardBrand(2, "Mastercard");
        public static readonly CardBrand Visa = new CardBrand(3, "Visa");

        private CardBrand() { }
        private CardBrand(int value, string displayName) : base(value, displayName) { }
    }
}
