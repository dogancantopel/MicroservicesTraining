using FreeCourse.Shared.ControllerBase;
using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Services.Catalog.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.Services.Catalog.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : CustomBaseApiController
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _courseService.GetByIdAsync(id);
            return CreateActionResult(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetByAll()
        {
            var response = await _courseService.GetAllAsync();
            return CreateActionResult(response);
        }
        [HttpGet]
        [Route("/api/[controller]/GetAllByUserId/{userId}")]
        public async Task<IActionResult> GetAllByUserId(string userId)
        {
            var response = await _courseService.GetByUserIdAsync(userId);
            return CreateActionResult(response);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateDto input)
        {
            var response = await _courseService.CreateAsync(input);
            return CreateActionResult(response);
        }
        [HttpPut]
        public async Task<IActionResult> Update(CourseUpdateDto input)
        {
            var response = await _courseService.UpdateAsync(input);
            return CreateActionResult(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _courseService.DeleteAsync(id);
            return CreateActionResult(response);
        }
    }
}
