using FreeCourse.Web.Models.FakePayments;
using FreeCourse.Web.Services.Interfaces;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FreeCourse.Web.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly HttpClient _httpClient;

        public PaymentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> ReceivePayment(PaymentInfo paymentInfo)
        {
            var response = await _httpClient.PostAsJsonAsync<PaymentInfo>("fakepayments", paymentInfo);

            return response.IsSuccessStatusCode;
        }
    }
}
