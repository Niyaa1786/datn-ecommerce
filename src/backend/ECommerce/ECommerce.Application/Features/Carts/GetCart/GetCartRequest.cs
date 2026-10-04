using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ECommerce.Application.Features.Carts.GetCart
{
    public class GetCartRequest
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
    }
}
