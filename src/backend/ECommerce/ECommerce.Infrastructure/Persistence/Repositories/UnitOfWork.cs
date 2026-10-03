using ECommerce.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using ECommerce.Domain.Interfaces;
using ECommerce.Infrastructure.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IUserRepository _userRepository;

        public UnitOfWork(AppDbContext context) => _context = context;
        public IUserRepository Users => _userRepository ??= new UserRepository(_context);

        public Task<int> SaveChangesAsync(CancellationToken ct = default) => _context.SaveChangesAsync(ct);
    }
}
