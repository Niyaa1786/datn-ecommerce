using FluentValidation;
using ECommerce.Application.Common.Exceptions;
using ECommerce.Application.Common.Interfaces;
using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;

namespace ECommerce.Application.Features.Orders.CreateOrder
{
    public class CreateOrderUseCase(IUnitOfWork unitOfWork, IValidator<CreateOrderRequest> validator) : IUseCase<CreateOrderRequest, CreateOrderResponse>
    {
        public async Task<CreateOrderResponse> ExecuteAsync(CreateOrderRequest request, CancellationToken ct = default)
        {
            validator.ValidateAndThrow(request);

            var user = await unitOfWork.Users.GetByIdAsync(request.UserId, ct);
            if (user == null)
                throw new NotFoundException("User not found.");

            var cart = await unitOfWork.Carts.GetByUserIdWithDetailsAsync(request.UserId, ct);
            if (cart == null || cart.Items.Count == 0)
                throw new AppValidationException(nameof(Cart), "Cart is empty.");

            var totalAmount = cart.TotalPrice;

            var order = new Order(request.UserId, request.ReceiverName, request.ReceiverPhone, request.ShippingAddress, totalAmount);

            foreach (var cartItem in cart.Items)
            {
                var variant = cartItem.ProductVariant;
                if (variant == null)
                    throw new NotFoundException("Product variant not found.");

                if (variant.Stock < cartItem.Quantity)
                    throw new AppValidationException(nameof(cartItem.Quantity), $"Not enough stock for variant {variant.SKU}. Available: {variant.Stock}");

                variant.DeductStock(cartItem.Quantity);
                order.AddItem(variant.Id, variant.Product?.Name!, variant.SKU, cartItem.Quantity, variant.Price);
            }

            var payment = new Payment(order.Id, request.PaymentMethod, totalAmount);
            order.AttachPayment(payment);

            unitOfWork.Orders.Add(order);
            unitOfWork.Carts.Remove(cart);

            await unitOfWork.SaveChangesAsync(ct);

            return new CreateOrderResponse
            {
                OrderId = order.Id,
                TotalAmount = totalAmount,
                PaymentMethod = payment.Method,
                Message = "Order created successfully."
            };
        }
    }
}
