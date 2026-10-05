using ECommerce.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ECommerce.Application.Features.Orders.GetOrdersByUserId
{
    public class GetOrdersByUserIdRequest
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
        public OrderStatus? Status { get; set; }
        public string? Keyword { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
