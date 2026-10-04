using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ECommerce.Domain.Interfaces
{
    public interface IBaseRepository<T, TId> where T : class
    {
        Task<T?> GetByIdAsync(TId id, CancellationToken ct = default);

        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
    }
}
