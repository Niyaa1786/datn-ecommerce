using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ECommerce.Application.Features.Users.DeleteAddress
{
    public class DeleteAddressRequest
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
        [JsonIgnore]
        public int AddressId { get; set; }
    }
}
