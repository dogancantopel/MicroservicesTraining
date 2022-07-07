using FreeCourse.Web.Models.Catalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeCourse.Web.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<List<CourseViewModel>> GetAllCourse();
        Task<List<CourseViewModel>> GetAllCourseByUserId(string userId);

        Task<List<CategoryViewModel>> GetAllCategories();
        Task<CourseViewModel> GetCourseById(string courseId);

        Task<bool> CreateCourse(CourseCreateInput input);
        Task<bool> UpdateCourse(CourseUpdateInput input);
        Task<bool> DeleteCourse(string courseId);
    }
}
