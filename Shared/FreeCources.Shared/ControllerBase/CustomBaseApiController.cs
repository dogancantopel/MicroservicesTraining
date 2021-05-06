using System;
using System.Collections.Generic;
using System.Text;
using FreeCources.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
namespace FreeCources.Shared.ControllerBase
{
    public class CustomBaseApiController :Controller
    {
        public IActionResult CreateActionResult<T>(Response<T> response)
        {
            return new ObjectResult(response) { StatusCode = response.StatusCode };
        }
    }
}
