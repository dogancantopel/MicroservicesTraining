using System;
using System.Collections.Generic;
using System.Text;
using FreeCourse.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
namespace FreeCourse.Shared.ControllerBase
{
    public class CustomBaseApiController :Controller
    {
        public IActionResult CreateActionResult<T>(Response<T> response)
        {
            return new ObjectResult(response) { StatusCode = response.StatusCode };
        }
    }
}
