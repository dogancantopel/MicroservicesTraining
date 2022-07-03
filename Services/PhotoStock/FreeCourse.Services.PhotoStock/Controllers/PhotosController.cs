﻿using FreeCourse.Shared.ControllerBase;
using FreeCourse.Shared.Dtos;
using FreeCourse.Services.PhotoStock.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace FreeCourse.Services.PhotoStock.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PhotosController : CustomBaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> PhotoSave(IFormFile photo, CancellationToken cancellationToken)
        {
            if (photo == null || photo.Length <= 0)
                return CreateActionResult(Response<PhotoDto>.Fail("Photo is empty", 400));

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photo.FileName);

            using var stream = new FileStream(path, FileMode.Create);
            await photo.CopyToAsync(stream, cancellationToken);

            var returnPath = "photos/" + photo.FileName;

            PhotoDto photoDto = new() { Url = returnPath };

            return CreateActionResult(Response<PhotoDto>.Success(photoDto, 200));
        }

        [HttpPost]
        public async Task<IActionResult> PhotoDelete(string photoUrl)
        {

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photoUrl);


            if (string.IsNullOrEmpty(photoUrl) || !System.IO.File.Exists(path))
                return CreateActionResult(Response<NoContent>.Fail("photo not found", 404));
            System.IO.File.Delete(path);

            return CreateActionResult(Response<NoContent>.Success(204));
        }
    }
}
