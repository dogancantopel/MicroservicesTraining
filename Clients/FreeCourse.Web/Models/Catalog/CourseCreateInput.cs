using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FreeCourse.Web.Models.Catalog
{
    public class CourseCreateInput
    {
        [DisplayName("Kurs İsmi")]
        public string Name { get; set; }
        [DisplayName("Kurs Fiyat")]
        public decimal Price { get; set; }
        public string UserId { get; set; }
        public FeatureViewModel Feature { get; set; }
        [DisplayName("Kurs Kategori")]
        public string CategoryId { get; set; }

        [DisplayName("Kurs Açıklama")]
        public string Description { get; set; }
        public string Picture { get; set; }

        [DisplayName("Kurs Resmi")]
        public IFormFile PhotoFormFile { get; set; }
    }
}
