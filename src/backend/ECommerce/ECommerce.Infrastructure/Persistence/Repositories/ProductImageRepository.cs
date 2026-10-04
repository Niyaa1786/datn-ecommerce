using Microsoft.EntityFrameworkCore;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using ECommerce.Infrastructure.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Persistence.Repositories
{
    public class ProductImageRepository : IProductImageRepository
    {
        private readonly AppDbContext _context;
        public ProductImageRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<ProductImage>> GetAllAsync(CancellationToken ct)
            => await _context.ProductImages.AsNoTracking().ToListAsync(ct);

        public async Task<ProductImage?> GetByIdAsync(int id, CancellationToken ct)
            => await _context.ProductImages.FindAsync(id, ct);

        public async Task<IEnumerable<ProductImage>> GetByProductIdAsync(Guid productId, CancellationToken ct)
            => await _context.ProductImages
                .Where(i => i.ProductId == productId)
                .ToListAsync(ct);

        public async Task<IEnumerable<ProductImage>> GetByPublicIdAsync(string publicId, CancellationToken ct)
            => await _context.ProductImages
                .Where(i => i.PublicId == publicId)
                .ToListAsync(ct);

        public void Add(ProductImage entity) => _context.ProductImages.Add(entity);
        public void Update(ProductImage entity) => _context.ProductImages.Update(entity);
        public void Remove(ProductImage entity) => _context.ProductImages.Remove(entity);

    }
}
