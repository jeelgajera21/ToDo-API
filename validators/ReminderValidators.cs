using FluentValidation;
using System;
using ToDo_API.Models;

public class ReminderValidator : AbstractValidator<ReminderModel>
{
    #region Reminder Validations
    public ReminderValidator()
    {
        RuleFor(r => r.TaskID)
            .GreaterThan(0).WithMessage("TaskID must be greater than 0.");

        RuleFor(r => r.ReminderTime)
            .GreaterThanOrEqualTo(DateTime.UtcNow).WithMessage("Reminder time must be in the future or present.");

        RuleFor(r => r.IsSent)
            .NotNull().WithMessage("IsSent status is required.");
    }
    #endregion
}
