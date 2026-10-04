using System;
using System.Collections.Generic;
using System.Text;
using ECommerce.Domain.Interfaces;

namespace ECommerce.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        ICategoryRepository Categories { get; }
        IProductRepository Products { get; }
        IProductVariantRepository ProductVariants { get; }
        IProductImageRepository ProductImages { get; }
        ICartRepository Carts { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
