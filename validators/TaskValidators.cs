using FluentValidation;

using System;
using ToDo_API.Models;

public class TaskValidator : AbstractValidator<TaskModel>
{
    public TaskValidator()
    {
        RuleFor(t => t.UserID)
            .GreaterThan(0).WithMessage("UserID must be greater than 0.");

        RuleFor(t => t.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(150).WithMessage("Title cannot exceed 150 characters.");

        RuleFor(t => t.Description)
            .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters.");

        RuleFor(t => t.DueDate)
            .GreaterThanOrEqualTo(DateTime.UtcNow).WithMessage("Due date must be in the future or today.");

        RuleFor(t => t.Priority)
            .InclusiveBetween(1, 5).WithMessage("Priority must be between 1 (Low) and 5 (High).");

        RuleFor(t => t.Status)
            .NotEmpty().WithMessage("Status is required.")
            .Must(status => new[] { "Pending", "In Progress", "Completed", "Canceled" }.Contains(status))
            .WithMessage("Status must be 'Pending', 'In Progress', 'Completed', or 'Canceled'.");

        RuleFor(t => t.CategoryID)
            .GreaterThan(0).WithMessage("CategoryID must be greater than 0.");

        RuleFor(t => t.CreatedAt)
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Created date cannot be in the future.");

        RuleFor(t => t.UpdatedAt)
            .GreaterThanOrEqualTo(t => t.CreatedAt).WithMessage("Updated date cannot be before the created date.");
    }
}
