using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ECommerce.Application.Features.Products.GetProduct
{
    public class GetProductRequest
    {
        [JsonIgnore]
        public Guid Id { get; set; }
    }
}
