using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace CommentSold.WebTest.Data
{
    public class ApplicationIdentityUser : IdentityUser<int>
    {
        public string Name { get; private set; }
        public bool IsSuperAdmin { get; private set; }
        public string ShopName { get; private set; }
        public string CardBrand { get; private set; }
        public ushort CardLastFourDigits { get; private set; }
        public string ShopDomain { get; private set; }
        public bool IsEnabled { get; private set; }
        public string BillingPlan { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public DateTime TrialEndsAt { get; private set; }
        public DateTime TrialStartsAt { get; private set; }
        public IReadOnlyCollection<Product> Products { get; private set; }

        public ApplicationIdentityUser(): base() {}

    }
}
