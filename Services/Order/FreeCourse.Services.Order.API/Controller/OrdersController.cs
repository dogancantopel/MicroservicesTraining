using FreeCourse.Shared.ControllerBase;
using FreeCourse.Shared.Services;
using FreeCourse.Services.Order.Application.Commands;
using FreeCourse.Services.Order.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FreeCourse.Services.Order.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : CustomBaseApiController
    {
        private readonly IMediator _mediator;
        private ISharedIdentityService _sharedIdentityService;

        public OrdersController(IMediator mediator, ISharedIdentityService sharedIdentityService)
        {
            _mediator = mediator;
            _sharedIdentityService = sharedIdentityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var response =await _mediator.Send(new GetOrdersByUserIdQuery() { UserId=_sharedIdentityService.GetUserId});

            return CreateActionResult(response);
        }
        [HttpPost]
        public async Task<IActionResult> SaveOrder(CreateOrderCommand createOrderCommand)
        {
            var response=await _mediator.Send(createOrderCommand);
            return CreateActionResult(response);
        }
    }
}
