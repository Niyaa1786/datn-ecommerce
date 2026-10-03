using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Auth.Register
{
    public class RegisterResponse
    {
        public Guid UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
    }
}
