using FluentValidation;
using ToDo_API.Models;

public class CategoryValidator : AbstractValidator<CategoryModel>
{
    #region Category Validations
    public CategoryValidator()
    {
        RuleFor(c => c.UserID)
            .GreaterThan(0).WithMessage("UserID must be greater than 0.");

        RuleFor(c => c.CategoryName)
            .NotEmpty().WithMessage("Category name is required.")
            .MaximumLength(100).WithMessage("Category name cannot exceed 100 characters.");

        RuleFor(c => c.Description)
            .NotEmpty().WithMessage("Please add description.")
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

        RuleFor(c => c.CreatedAt)
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Created date cannot be in the future.");
    }
    #endregion
}
