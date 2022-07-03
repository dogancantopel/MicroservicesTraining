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
    public class CategoryController : CustomBaseApiController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult>GetAll()
        {
            var categories = await _categoryService.GetAllAsync();
            return CreateActionResult(categories);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            return CreateActionResult(category);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto input)
        {
            var response= await _categoryService.CreateAsync(input);
            return CreateActionResult(response);
        }
    }
}
