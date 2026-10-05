using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ECommerce.Application.Features.Orders.CancelOrder
{
    public class CancelOrderRequest
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
        public Guid OrderId { get; set; }
    }
}
