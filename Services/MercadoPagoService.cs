using MercadoPago.Client.Payment;
using MercadoPago.Config;
using MercadoPago.Resource.Payment;

public class MercadoPagoService
{
    private readonly string _accessToken;

    public MercadoPagoService(IConfiguration configuration)
    {
        _accessToken = configuration["TEST-348193124302644-052918-fcf664f4e892498ac4e17f5b1dc036d5-666634652"];
        MercadoPagoConfig.AccessToken = _accessToken;
    }

    public async Task<Payment> CreatePayment(decimal amount, string token, string description, string email, string returnUrl)
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
            },
        };
        var client = new PaymentClient();
        Payment payment = await client.CreateAsync(paymentRequest);
        return payment;
    }
}