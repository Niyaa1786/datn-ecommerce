using System;
using System.Collections.Generic;
using System.Text;
using ECommerce.Domain.Interfaces;

namespace ECommerce.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
