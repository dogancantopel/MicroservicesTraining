using FreeCourse.Shared.ControllerBase;
using FreeCourse.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.FakePayment.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakePaymentsController : CustomBaseApiController
    {
        [HttpPost]
        public IActionResult ReceivePayment()
        {
            return CreateActionResult(Response<NoContent>.Success(200));
        }
    }
}
