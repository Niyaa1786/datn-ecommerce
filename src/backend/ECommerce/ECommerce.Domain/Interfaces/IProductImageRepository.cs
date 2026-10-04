using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Interfaces
{
    public interface IProductImageRepository : IBaseRepository<ProductImage, int>
    {
        Task<IEnumerable<ProductImage>> GetByProductIdAsync(Guid productId, CancellationToken ct = default);
        Task<IEnumerable<ProductImage>> GetByPublicIdAsync(string publicId, CancellationToken ct = default);
    }
}
