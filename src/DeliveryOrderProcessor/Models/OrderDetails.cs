using System;
using System.Collections.Generic;

namespace DeliveryOrderProcessor.Models
{
    public class OrderDetails
    {
        public string BuyerId { get; set; }

        public DateTimeOffset OrderDate { get; set; }

        public Address ShipToAddress { get; set; }

        public IReadOnlyCollection<OrderItem> OrderItems { get; set; }

        public decimal TotalPrice { get; set; }
    }
}