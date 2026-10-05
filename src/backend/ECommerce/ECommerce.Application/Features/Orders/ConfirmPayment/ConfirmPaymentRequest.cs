using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Orders.ConfirmPayment
{
    public class ConfirmPaymentRequest
    {
        public Guid OrderId { get; set; }
    }
}
