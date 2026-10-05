using ECommerce.Application.Common.Exceptions;
using ECommerce.Application.Common.Interfaces;
using ECommerce.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Orders.ConfirmPayment
{
    public class ConfirmPaymentUseCase(IUnitOfWork unitOfWork) : IUseCase<ConfirmPaymentRequest, ConfirmPaymentResponse>
    {
        public async Task<ConfirmPaymentResponse> ExecuteAsync(ConfirmPaymentRequest request, CancellationToken ct = default)
        {
            var payment = await unitOfWork.Payments.GetByOrderIdAsync(request.OrderId, ct);
            if (payment == null)
                throw new NotFoundException("Payment not found.");

            if (payment.Method != PaymentMethod.COD)
                throw new AppValidationException(nameof(payment.Method), "This endpoint only supports COD payments.");

            payment.Complete();

            await unitOfWork.SaveChangesAsync(ct);
            return new ConfirmPaymentResponse
            {
                Status = payment.Status.ToString(),
                PaidAt = payment.PaidAt,
                Message = "Payment confirmed successfully"
            };
        }
    }
}
