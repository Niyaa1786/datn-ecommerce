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

        public UnitOfWork(AppDbContext context) => _context = context;
        public IUserRepository Users => _userRepository ??= new UserRepository(_context);
        public ICategoryRepository Categories => _categoryRepository ??= new CategoryRepository(_context);

        public Task<int> SaveChangesAsync(CancellationToken ct = default) => _context.SaveChangesAsync(ct);
    }
}
