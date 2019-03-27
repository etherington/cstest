using System;

namespace CommentSold.WebTest.Data
{
    public class Order
    {
        public int Id { get; private set; }
        public string StreetAddress { get; private set; }
        public string Apartment { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string CountryCode { get; private set; }
        public string Zip { get; private set; }
        public string Email { get; private set; }
        public string Name { get; private set; }
        public string OrderStatus { get; private set; } //
        public string TransactionId { get; private set; }
        public ushort PaymentAmountCents { get; private set; }
        public uint? ShipChargedCents { get; private set; }
        public uint? ShipCostCents { get; private set; }
        public uint SubTotalCents { get; private set; }
        public uint TotalCents { get; private set; }
        public string ShipperName { get; private set; }
        public DateTime? PaymentDate { get; private set; }
        public DateTime? ShippedDate { get; private set; }
        public string TrackingNumber { get; private set; }
        public uint TaxTotalCents { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        
        public Inventory Inventory { get; private set; }
    }
}
