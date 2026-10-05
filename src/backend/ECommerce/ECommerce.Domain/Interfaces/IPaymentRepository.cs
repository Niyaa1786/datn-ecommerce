using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Interfaces
{
    public interface IPaymentRepository : IBaseRepository<Payment, Guid>
    {
        Task<Payment?> GetByOrderIdAsync(Guid orderId, CancellationToken ct = default);
    }
}
