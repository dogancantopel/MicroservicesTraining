using FreeCourse.Web.Services.Interfaces;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FreeCourse.Web.Exceptions;
namespace FreeCourse.Web.Handlers
{
    public class ClientCredentialTokenHandler: DelegatingHandler
    {
        private readonly IClientCredentialTokenService _clientCredentialTokenService;

        public ClientCredentialTokenHandler(IClientCredentialTokenService clientCredentialTokenService)
        {
            _clientCredentialTokenService = clientCredentialTokenService;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,CancellationToken cancellationToken)
        {
            var token =await _clientCredentialTokenService.GetToken();
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            try
            {
                var response = await base.SendAsync(request, cancellationToken);
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw new UnAuthorizeException();
                }
                return response;
               
            }
            catch (System.Exception ex)
            {

                throw ex;
            }


        }
    }
}
