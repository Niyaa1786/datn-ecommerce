using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Application.Features.Users.UploadAvatar
{
    public class UploadAvatarRequest
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
        public IFormFile File { get; set; } = default!;
    }
}
