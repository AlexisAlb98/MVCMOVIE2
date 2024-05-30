using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
using MercadoPago.Client.Preference;
using MercadoPago.Config;
using MercadoPago.Resource.Preference;
using System.Collections.Generic;
using System.Threading.Tasks;

public class PaymentController : Controller
{
    public async Task<IActionResult> Checkout()
    {
        // Agrega credenciales
        MercadoPagoConfig.AccessToken = "TEST-348193124302644-052918-fcf664f4e892498ac4e17f5b1dc036d5-666634652";

        // Crea el objeto de request de la preference
        var request = new PreferenceRequest
        {
            Items = new List<PreferenceItemRequest>
            {
                new PreferenceItemRequest
                {
                    Title = "Mi producto",
                    Quantity = 1,
                    CurrencyId = "ARS",
                    UnitPrice = 75.56m,
                },
            },
        };

        // Crea la preferencia usando el client
        var client = new PreferenceClient();
        Preference preference = await client.CreateAsync(request);

        // Pasa el ID de la preferencia a la vista
        ViewBag.PreferenceId = preference.Id;
        ViewBag.PublicKey = "TEST-4d3e2bb1-2c4f-40e0-a7ef-1630e3fe20e1"; // Configura tu clave pública aquí

        return View();
    }
}
=======
=======
>>>>>>> 8e5e430d4d2c69d53280a9e755c4c5391dc04f16
=======
>>>>>>> 8e5e430d4d2c69d53280a9e755c4c5391dc04f16
using MvcMovie.Services;
using System.Threading.Tasks;

namespace YourNamespace.Controllers
{
    public class PaymentController : Controller
    {
        private readonly MercadoPagoService _mercadoPagoService;

        public PaymentController(MercadoPagoService mercadoPagoService)
        {
            _mercadoPagoService = mercadoPagoService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment(decimal amount, string token, string description, string email)
        {
            if (string.IsNullOrEmpty(token))
            {
                // Manejar el caso en el que el token es nulo o vacío
                return View("Error");
            }

            var payment = await _mercadoPagoService.CreatePayment(amount, token, description, email);
            if (payment.Status == "approved")
            {
                return View("Success");
            }
            else
            {
                return View("Failure");
            }
        }
        [HttpGet]
        public IActionResult Checkout()
        {
            return View();
        }
    }
}
<<<<<<< HEAD
<<<<<<< HEAD
>>>>>>> 8e5e430d4d2c69d53280a9e755c4c5391dc04f16
=======
>>>>>>> 8e5e430d4d2c69d53280a9e755c4c5391dc04f16
=======
>>>>>>> 8e5e430d4d2c69d53280a9e755c4c5391dc04f16
