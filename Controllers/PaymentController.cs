using Microsoft.AspNetCore.Mvc;
using MercadoPago.Client.Preference;
using MercadoPago.Config;
using MercadoPago.Resource.Preference;
using System.Collections.Generic;
using System.Threading.Tasks;
using MvcMovie.Services;
using Microsoft.Identity;

public class PaymentController : Controller
{
    private readonly PedidoService _pedidoService;

    public PaymentController(PedidoService pedidoService)
    {
        _pedidoService = pedidoService;
    }

    public async Task<IActionResult> Checkout(int pedidoId)
    {
        // Recupera el pedido desde la base de datos
        var pedido = await _pedidoService.GetPedidoById(pedidoId);

        if (pedido == null)
        {
            return NotFound();
        }

        // Agrega credenciales
        MercadoPagoConfig.AccessToken = "TEST-348193124302644-052918-fcf664f4e892498ac4e17f5b1dc036d5-666634652";

        // Crea el objeto de request de la preference
        var request = new PreferenceRequest
        {
            Items = new List<PreferenceItemRequest>
            {
                new PreferenceItemRequest
                {
                    Title = "Compra Total",
                    Quantity = 1,
                    CurrencyId = "ARS",
                    UnitPrice = pedido.Total ?? 0m, // Usa el total del pedido
                },
            },
        };            

        // Crea la preferencia usando el client
        var client = new PreferenceClient();
        Preference preference = await client.CreateAsync(request);

        // Pasa el ID de la preferencia a la vista
        ViewBag.PreferenceId = preference.Id;
        ViewBag.PublicKey = "TEST-4d3e2bb1-2c4f-40e0-a7ef-1630e3fe20e1"; // Configura tu clave pública aquí

        return View(pedido);
    }
}