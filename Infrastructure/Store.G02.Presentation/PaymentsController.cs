using Microsoft.AspNetCore.Mvc;
using Store.G02.Domain.Entities.Orders;
using Store.G02.Services.Abstractions;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController(IServicesManager _servicesManager) : ControllerBase
    {
        [HttpPost("{basketId}")]
        public async Task<IActionResult> CreatePaymentIntent(string basketId)
        {
           var result = await _servicesManager.PaymentService.CreatePaymentIntentAsync(basketId);
            return Ok();
        }
       
        [Route("webhook")]
        [HttpPost]
        [HttpPost("webhook")]
        public async Task<IActionResult> Webhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            const string endpointSecret = "whsec_f337e4badb0618f64c28e67e6181e8eb67d9ac0eb255f04e746aec0ecbfcbeaa";
            var signatureHeader = Request.Headers["Stripe-Signature"];

            Event stripeEvent;

            try
            {
                stripeEvent = EventUtility.ConstructEvent(json, signatureHeader, endpointSecret);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Webhook error: {ex.Message}");
                return BadRequest();
            }

            var paymentIntent = stripeEvent.Data.Object as PaymentIntent;

            if (paymentIntent is null)
                return BadRequest();

            if (stripeEvent.Type == EventTypes.PaymentIntentSucceeded)
            {
                await _servicesManager.orderServices.UpdateOrderStatusAsync(paymentIntent.Id, OrderStatus.PaymentSucceses);
            }
            else if (stripeEvent.Type == EventTypes.PaymentIntentPaymentFailed)
            {
                await _servicesManager.orderServices.UpdateOrderStatusAsync(paymentIntent.Id, OrderStatus.PaymentFailed);
            }
            else
            {
                Console.WriteLine($"Unhandled event type: {stripeEvent.Type}");
            }

            return Ok();
        }

    }
}
