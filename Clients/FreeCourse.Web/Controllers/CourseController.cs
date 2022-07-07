using FreeCourse.Shared.Services;
using FreeCourse.Web.Models.Catalog;
using FreeCourse.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace FreeCourse.Web.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public CourseController(ICatalogService catalogService, ISharedIdentityService sharedIdentityService)
        {
            _catalogService = catalogService;
            _sharedIdentityService = sharedIdentityService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _catalogService.GetAllCourseByUserId(_sharedIdentityService.GetUserId));
        }

        public async Task<IActionResult> Create()
        {
            var categories= await _catalogService.GetAllCategories();

            ViewBag.categoryList = new SelectList(categories, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateInput courseCreateInput)
        {
            if (!ModelState.IsValid)
                return View();

            var categories = await _catalogService.GetAllCategories();
            ViewBag.categoryList = new SelectList(categories, "Id", "Name");

            courseCreateInput.UserId = _sharedIdentityService.GetUserId;

            await _catalogService.CreateCourse(courseCreateInput);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(string id)
        {
            var course = await _catalogService.GetCourseById(id);
            var categories = await _catalogService.GetAllCategories();
            ViewBag.categoryList = new SelectList(categories, "Id", "Name",course.CategoryId);

            if (course == null)
                return RedirectToAction(nameof(Index));

            CourseUpdateInput model = new CourseUpdateInput()
            {
                CategoryId = course.CategoryId,
                Description=course.Description,
                Feature= course.Feature,
                Id = course.Id,
                Name = course.Name,
                Price = course.Price,
                Picture =course.Picture,
                UserId = course.UserId,
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Update(CourseUpdateInput model)
        {
            if (!ModelState.IsValid)
                return View();

            var course = await _catalogService.GetCourseById(model.Id);
            if (course == null)
                return View(ViewBag.Error = "Kurs bulunamadı");

            var result= await _catalogService.UpdateCourse(model);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(string id)
        {
            await _catalogService.DeleteCourse(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
