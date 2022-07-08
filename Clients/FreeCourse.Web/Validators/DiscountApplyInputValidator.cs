using FluentValidation;
using FreeCourse.Web.Models.Discount;
namespace FreeCourse.Web.Validators
{
    public class DiscountApplyInputValidator:AbstractValidator<DiscountApplyInput>
    {
        public DiscountApplyInputValidator()
        {
            RuleFor(m => m.Code).NotEmpty().WithMessage("İndirim kuponu boş olamaz");
        }
    }
}
