using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Auth.RefreshToken
{
    public class RefreshTokenRequest
    {
        public string RefreshToken { get; set; } = string.Empty;
    }
}
