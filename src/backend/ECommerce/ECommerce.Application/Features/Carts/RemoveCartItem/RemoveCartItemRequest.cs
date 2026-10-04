using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ECommerce.Application.Features.Carts.RemoveCartItem
{
    public class RemoveCartItemRequest
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
        [JsonIgnore]
        public int CartItemId { get; set; }
    }
}
