using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Interfaces
{
    public interface IProductVariantRepository : IBaseRepository<ProductVariant, Guid>
    {
        Task<ProductVariant?> GetBySKUAsync(string sku, CancellationToken ct = default);
        Task<IEnumerable<ProductVariant>> GetByProductIdAsync(Guid productId, CancellationToken ct = default);
        Task<bool> IsExistByIdAsync(Guid id, CancellationToken ct = default);
        Task<bool> IsExistBySkuAsync(string sku, CancellationToken ct = default);
        Task<ProductVariant?> GetByIdWithDetailsAsync(Guid id, CancellationToken ct = default);
    }
}
