using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Auth.RefreshToken
{
    public class RefreshTokenResponse
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime AccessTokenExpiration { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
    }
}
