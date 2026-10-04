using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product, Guid>
    {
        Task<Product?> GetByIdWithDetailsAsync(Guid id, CancellationToken ct = default);
        Task<IEnumerable<Product>> GetAllByFiltersAsync(
            int? categoryId = null,
            string? keyword = null,
            ProductStatus? status = null,
            bool includeDeleted = false,
            int page = 1,
            int pageSize = 10,
            CancellationToken ct = default);

        Task<int> CountProductsAsync(
            int? categoryId = null,
            string? keyword = null,
            ProductStatus? status = null,
            bool includeDeleted = false,
            CancellationToken ct = default);
    }
}
