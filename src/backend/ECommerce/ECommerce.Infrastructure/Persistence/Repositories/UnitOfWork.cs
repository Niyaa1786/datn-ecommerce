using ECommerce.Application.Common.Interfaces;
using ECommerce.Domain.Interfaces;
using ECommerce.Infrastructure.Persistence.Data;

namespace ECommerce.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IUserRepository? _userRepository;
        private ICategoryRepository? _categoryRepository;
        private IProductRepository _productRepository;
        private IProductVariantRepository _productVariantRepository;
        private IProductImageRepository _productImageRepository;

        public UnitOfWork(AppDbContext context) => _context = context;
        public IUserRepository Users => _userRepository ??= new UserRepository(_context);
        public ICategoryRepository Categories => _categoryRepository ??= new CategoryRepository(_context);
        public IProductRepository Products => _productRepository ??= new ProductRepository(_context);

        public IProductVariantRepository ProductVariants => _productVariantRepository ??= new ProductVariantRepository(_context);

        public IProductImageRepository ProductImages => _productImageRepository ??= new ProductImageRepository(_context);

        public Task<int> SaveChangesAsync(CancellationToken ct = default) => _context.SaveChangesAsync(ct);
    }
}
