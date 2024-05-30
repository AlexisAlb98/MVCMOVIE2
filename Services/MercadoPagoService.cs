using MercadoPago.Client.Payment;
using MercadoPago.Config;
using MercadoPago.Resource.Payment;

public class MercadoPagoService
{
    private readonly string _accessToken;

#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
    public MercadoPagoService(IConfiguration configuration)
#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
    {
#pragma warning disable CS8601 // Posible asignación de referencia nula
        _accessToken = configuration["MercadoPago:AccessToken"];
#pragma warning restore CS8601 // Posible asignación de referencia nula
        MercadoPagoConfig.AccessToken = _accessToken;
    }

    public async Task<Payment> CreatePayment(decimal amount, string token, string description, string email)
    {
        var paymentRequest = new PaymentCreateRequest
        {
            TransactionAmount = amount,
            Token = token,
            Description = description,
            PaymentMethodId = "visa",
            Payer = new PaymentPayerRequest
            {
                Email = email
            }
        };

        var client = new PaymentClient();
        Payment payment = await client.CreateAsync(paymentRequest);
        return payment;
    }
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD

=======
>>>>>>> 8e5e430d4d2c69d53280a9e755c4c5391dc04f16
=======
>>>>>>> 8e5e430d4d2c69d53280a9e755c4c5391dc04f16
=======
>>>>>>> 8e5e430d4d2c69d53280a9e755c4c5391dc04f16
}