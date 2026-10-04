using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ECommerce.Application.Features.Carts.UpdateCartItem
{
    public class UpdateCartItemRequest
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
        [JsonIgnore]
        public int CartItemId { get; set; }
        public int Quantity { get; set; }
    }
}
