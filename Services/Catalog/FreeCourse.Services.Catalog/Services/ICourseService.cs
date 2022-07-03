using FreeCourse.Shared.Dtos;
using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Services.Catalog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.Services.Catalog.Services
{
    public interface ICourseService
    {
        Task<Response<List<CourseDto>>> GetAllAsync();
        Task<Response<CourseDto>> CreateAsync(Course course);
        Task<Response<CourseDto>> GetByIdAsync(string id);
        Task<Response<List<CourseDto>>> GetByUserIdAsync(string userId);
        Task<Response<CourseDto>> CreateAsync(CourseCreateDto input);
        Task<Response<NoContent>> UpdateAsync(CourseUpdateDto input);
        Task<Response<NoContent>> DeleteAsync(string id);
    }
}
