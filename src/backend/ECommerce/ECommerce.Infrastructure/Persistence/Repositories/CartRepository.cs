using Microsoft.EntityFrameworkCore;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using ECommerce.Infrastructure.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Persistence.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _context;
        public CartRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<Cart>> GetAllAsync(CancellationToken ct)
            => await _context.Carts.AsNoTracking().ToListAsync(ct);

        public async Task<Cart?> GetByIdAsync(Guid id, CancellationToken ct)
           => await _context.Carts.FirstOrDefaultAsync(c => c.Id == id, ct);

        public async Task<Cart?> GetByIdWithDetails(Guid id, CancellationToken ct)
           => await _context.Carts
            .AsSplitQuery()
            .Include(c => c.Items)
                .ThenInclude(i => i.ProductVariant)
                    .ThenInclude(v => v.Product)
                        .ThenInclude(p => p!.Images)
            .FirstOrDefaultAsync(ct);
        public async Task<Cart?> GetByUserIdAsync(Guid userId, CancellationToken ct = default)
            => await _context.Carts.FirstOrDefaultAsync(c => c.UserId == userId, ct);

        public async Task<Cart?> GetByUserIdWithDetailsAsync(Guid userId, CancellationToken ct = default)
            => await _context.Carts
            .AsSplitQuery()
            .Include(c => c.Items)
                .ThenInclude(i => i.ProductVariant)
                    .ThenInclude(v => v.Product)
                        .ThenInclude(p => p!.Images)
            .FirstOrDefaultAsync(ct);

        public async Task<int> CountCartItemsByUserIdAsync(Guid userId, CancellationToken ct = default)
            => await _context.CartItems.CountAsync(ci => ci.Cart.UserId == userId, ct);

        public void Add(Cart entity) => _context.Carts.Add(entity);
        public void Update(Cart entity) => _context.Carts.Update(entity);
        public void Remove(Cart entity) => _context.Carts.Remove(entity);
    }
}
