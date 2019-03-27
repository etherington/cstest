using System;

namespace CommentSold.Model.Entities
{
    public class Order : Entity
    {
        public string StreetAddress { get; private set; }
        public string Apartment { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string CountryCode { get; private set;}
        public string Zip { get; private set; }
        public string Email { get; private set; }
        public string Name { get; private set;}
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


        //  public Product Product { get; private set; }
        public Inventory Inventory { get; private set; }

        public Order(string streetAddress, string apartment, string city, string state, string countryCode, string zip, string email, string name, string orderStatus, string transactionId, ushort paymentAmountCents, uint? shipChargedCents, uint? shipCostCents, uint subTotalCents, uint totalCents, string shipperName, DateTime? paymentDate, DateTime? shippedDate, string trackingNumber, uint taxTotalCents, DateTime createdAt, DateTime updatedAt, Inventory inventory)
        {
            StreetAddress = streetAddress ?? throw new ArgumentNullException(nameof(streetAddress));
            Apartment = apartment ?? throw new ArgumentNullException(nameof(apartment));
            City = city ?? throw new ArgumentNullException(nameof(city));
            State = state ?? throw new ArgumentNullException(nameof(state));
            CountryCode = countryCode ?? throw new ArgumentNullException(nameof(countryCode));
            Zip = zip ?? throw new ArgumentNullException(nameof(zip));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            OrderStatus = orderStatus ?? throw new ArgumentNullException(nameof(orderStatus));
            TransactionId = transactionId ?? throw new ArgumentNullException(nameof(transactionId));
            PaymentAmountCents = paymentAmountCents;
            ShipChargedCents = shipChargedCents ?? throw new ArgumentNullException(nameof(shipChargedCents));
            ShipCostCents = shipCostCents ?? throw new ArgumentNullException(nameof(shipCostCents));
            SubTotalCents = subTotalCents;
            TotalCents = totalCents;
            ShipperName = shipperName ?? throw new ArgumentNullException(nameof(shipperName));
            PaymentDate = paymentDate ?? throw new ArgumentNullException(nameof(paymentDate));
            ShippedDate = shippedDate ?? throw new ArgumentNullException(nameof(shippedDate));
            TrackingNumber = trackingNumber ?? throw new ArgumentNullException(nameof(trackingNumber));
            TaxTotalCents = taxTotalCents;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
        }
    }
}
