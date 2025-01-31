using FluentValidation;
using System.Text.RegularExpressions;
using ToDo_API.Models;

public class UserValidator : AbstractValidator<UserModel>
{
    public UserValidator()
    {
        RuleFor(u => u.UserName)
            .NotEmpty().WithMessage("Username is required.")
            .MaximumLength(50).WithMessage("Username cannot exceed 50 characters.");

        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.")
            .MaximumLength(100).WithMessage("Email cannot exceed 100 characters.");

        RuleFor(u => u.PasswordHash)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .Matches(new Regex(@"[A-Z]")).WithMessage("Password must contain at least one uppercase letter.")
            .Matches(new Regex(@"[a-z]")).WithMessage("Password must contain at least one lowercase letter.")
            .Matches(new Regex(@"\d")).WithMessage("Password must contain at least one number.")
            .Matches(new Regex(@"[\W_]")).WithMessage("Password must contain at least one special character.");

        RuleFor(u => u.CreatedAt)
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Created date cannot be in the future.");

        RuleFor(u => u.IsActive)
            .NotNull().WithMessage("IsActive status is required.");
    }
}
