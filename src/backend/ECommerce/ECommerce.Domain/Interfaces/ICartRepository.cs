using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Interfaces
{
    public interface ICartRepository : IBaseRepository<Cart, Guid>
    {
        Task<Cart?> GetByIdWithDetails(Guid id, CancellationToken ct = default);
        Task<Cart?> GetByUserIdAsync(Guid userId, CancellationToken ct = default);
        Task<Cart?> GetByUserIdWithDetailsAsync(Guid userId, CancellationToken ct = default);
        Task<int> CountCartItemsByUserIdAsync(Guid userId, CancellationToken ct = default);
    }
}
