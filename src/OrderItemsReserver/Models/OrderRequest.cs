using System;
using System.Collections.Generic;

namespace OrderItemsReserver.Models
{
    public class OrderRequest
    {
        public string BuyerId { get; set; }

        public DateTimeOffset OrderDate { get; set; }

        public Address ShipToAddress { get; set; }

        public IReadOnlyCollection<OrderItem> OrderItems { get; set; }
    }
}