using FreeCourse.Shared.Dtos;
using FreeCourse.Web.Helper;
using FreeCourse.Web.Models;
using FreeCourse.Web.Models.Catalog;
using FreeCourse.Web.Services.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FreeCourse.Web.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _client;
        private readonly IPhotoStockService _photoStockService;
        private readonly PhotoHelper _photoHelper;

        public CatalogService(HttpClient client, IPhotoStockService photoStockService,PhotoHelper photoHelper)
        {
            _client = client;
            _photoStockService = photoStockService;
            _photoHelper = photoHelper;
        }

        public async Task<bool> CreateCourse(CourseCreateInput input)
        {
            var resultPhotoService = await _photoStockService.UploadPhoto(input.PhotoFormFile);
            if (resultPhotoService != null)
                input.Picture = resultPhotoService.Url;

            var response = await _client.PostAsJsonAsync<CourseCreateInput>("course", input);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCourse(string courseId)
        {
            var response = await _client.DeleteAsync($"course/{courseId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<CategoryViewModel>> GetAllCategories()
        {
            var response = await _client.GetAsync("category");
            if (!response.IsSuccessStatusCode)
                return null;

            var successResponse = await response.Content.ReadFromJsonAsync<Response<List<CategoryViewModel>>>();

            return successResponse.Data;
        }

        public async Task<List<CourseViewModel>> GetAllCourse()
        {
            var response = await _client.GetAsync("course");
            if (!response.IsSuccessStatusCode)
                return null;

            var successResponse= await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();

            successResponse.Data.ForEach(m =>
            {
                m.FullUrl = _photoHelper.GetPhotoStockUrl(m.Picture);
            });

            return successResponse.Data;
        }

        public async Task<List<CourseViewModel>> GetAllCourseByUserId(string userId)
        {
            
            var response = await _client.GetAsync($"course/GetAllByUserId/{userId}");
            if (!response.IsSuccessStatusCode)
                return null;

            var successResponse = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();

            successResponse.Data.ForEach(m =>
            {
                m.FullUrl = _photoHelper.GetPhotoStockUrl(m.Picture);
            });

            return successResponse.Data;
        }

        public async Task<CourseViewModel> GetCourseById(string courseId)
        {
            var response = await _client.GetAsync($"course/{courseId}");
            if (!response.IsSuccessStatusCode)
                return null;

            var successResponse = await response.Content.ReadFromJsonAsync<Response<CourseViewModel>>();

            successResponse.Data.FullUrl = _photoHelper.GetPhotoStockUrl(successResponse.Data.Picture);

            return successResponse.Data;
        }

        public async Task<bool> UpdateCourse(CourseUpdateInput input)
        {
            var resultPhotoService = await _photoStockService.UploadPhoto(input.PhotoFormFile);
            if (resultPhotoService != null)
            {
                await _photoStockService.DeletePhoto(input.Picture);
                input.Picture = resultPhotoService.Url;
            }
            var response = await _client.PutAsJsonAsync<CourseUpdateInput>("course", input);
            return response.IsSuccessStatusCode;
        }
    }
}
