using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace CommentSold.Model.Entities
{
    public class User : IdentityUser
    {
         public string Name {get; private set;}
        // public string Email {get; private set;}
        // public string Password {get; private set;}
         //public string PasswordPlain {get; private set;}
         public bool IsSuperAdmin {get; private set;}
         public string ShopName {get; private set;}
        // public bool RememberMe {get; private set;}
         public string CardBrand {get; private set;}
         public ushort CardLastFourDigits {get; private set;}
         public string ShopDomain {get; private set;}
         public bool IsEnabled {get; private set;}
         public string BillingPlan {get; private set;}
         public DateTime CreatedAt{ get; private set; }
         public DateTime UpdatedAt{ get; private set; }
         public DateTime TrialEndsAt{ get; private set; }
         public DateTime TrialStartsAt{ get; private set; }

        public IReadOnlyCollection<Product> Products { get; private set; }

        public User(string name, string email, bool isSuperAdmin, string shopName, string cardBrand, ushort cardLastFourDigits, string shopDomain, bool isEnabled, string billingPlan, DateTime createdAt, DateTime updatedAt, DateTime trialEndsAt, DateTime trialStartsAt, IReadOnlyCollection<Product> products)
        : base(email)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
           // Email = email ?? throw new ArgumentNullException(nameof(email));
            IsSuperAdmin = isSuperAdmin;
            ShopName = shopName ?? throw new ArgumentNullException(nameof(shopName));
            CardBrand = cardBrand ?? throw new ArgumentNullException(nameof(cardBrand));
            CardLastFourDigits = cardLastFourDigits;
            ShopDomain = shopDomain ?? throw new ArgumentNullException(nameof(shopDomain));
            IsEnabled = isEnabled;
            BillingPlan = billingPlan ?? throw new ArgumentNullException(nameof(billingPlan));
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            TrialEndsAt = trialEndsAt;
            TrialStartsAt = trialStartsAt;
            Products = products ?? throw new ArgumentNullException(nameof(products));
        }
    }
}
