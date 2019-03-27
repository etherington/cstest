namespace CommentSold.Model.EnumTypes
{
    public class BillingPlan: Enumeration
    {
        public static readonly BillingPlan Startup = new BillingPlan(0, "Startup");
        public static readonly BillingPlan Boutique = new BillingPlan(1, "Boutique");
        public static readonly BillingPlan Enterprise = new BillingPlan(2, "Enterprise");

        private BillingPlan(){}
        private BillingPlan(int value, string displayName) : base(value, displayName) {}
    }
}
