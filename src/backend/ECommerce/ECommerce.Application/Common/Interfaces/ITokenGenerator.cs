using ECommerce.Application.Common.DTOs;
using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace ECommerce.Application.Common.Interfaces
{
    public interface ITokenGenerator
    {
        TokenResult GenerateToken(User user);
    }
}
