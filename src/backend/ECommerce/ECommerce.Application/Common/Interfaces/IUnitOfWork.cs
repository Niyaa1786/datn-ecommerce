using System;
using System.Collections.Generic;
using System.Text;
using ECommerce.Domain.Interfaces;
using ECommerce.Domain.Interfaces;

namespace ECommerce.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        ICategoryRepository Categories { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
