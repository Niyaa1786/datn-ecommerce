using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ECommerce.Application.Features.Orders.GetOrder
{
    public class GetOrderRequest
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
        public Guid OrderId { get; set; }
    }
}
