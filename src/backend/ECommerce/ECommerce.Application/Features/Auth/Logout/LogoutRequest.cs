using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Auth.Logout
{
    public class LogoutRequest
    {
        public Guid UserId { get; set; }
    }
}
