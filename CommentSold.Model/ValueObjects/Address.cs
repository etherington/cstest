using System;
using System.Collections.Generic;
using System.Text;

namespace CommentSold.Model.ValueObjects
{
    public class Address: ValueObject<Address>
    {
        public string StreetAddress { get; private set; }
        public string Apartment { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string CountryCode { get; private set; }
        public string Zip { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}
