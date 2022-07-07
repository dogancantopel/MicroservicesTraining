using System.ComponentModel;

namespace FreeCourse.Web.Models.Catalog
{
    public class FeatureViewModel
    {
        [DisplayName("Kurs Süresi")]
        public int Duration { get; set; }
    }
}
