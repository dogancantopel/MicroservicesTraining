using FluentValidation;
using FreeCourse.Web.Models.Catalog;

namespace FreeCourse.Web.Validators
{
    public class CourseCreateInputValidator : AbstractValidator<CourseCreateInput>
    {
        public CourseCreateInputValidator()
        {
            RuleFor(m => m.Name).NotEmpty().WithMessage("İsim alanı boş olamaz");
            RuleFor(m => m.Description).NotEmpty().WithMessage("İsim alanı boş olamaz");
            RuleFor(m => m.Feature.Duration).InclusiveBetween(1, int.MaxValue).WithMessage("Süre alanı boş olamaz");
            RuleFor(m => m.Price).NotEmpty().WithMessage("Ücret boş olamaz").ScalePrecision(2, 6).WithMessage("Hatalaı format");
            RuleFor(m => m.CategoryId).NotEmpty().WithMessage("Kategori boş olamaz");
        }
    }
}