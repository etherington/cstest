namespace CommentSold.Model.EnumTypes
{
    public class Size : Enumeration
    {
        public static readonly Size ExtraSmall = new Size(0, "XS");
        public static readonly Size Small = new Size(1, "S");
        public static readonly Size Medium = new Size(2, "M");
        public static readonly Size Large = new Size(3, "L");
        public static readonly Size ExtraLarge = new Size(4, "XL");
        public static readonly Size ExtraExtraLarge = new Size(5, "XXL");

        private Size() { }
        private Size(int value, string displayName) : base(value, displayName) { }
    }
}
