using Busines.Dtos.Requests.WorkHourRequests;
using FluentValidation;

namespace Busines.Rules.ValidationRules.FluentValidation.WorkHourValidators;

public class CreateWorkHourRequestValidator : AbstractValidator<CreateWorkHourRequest>
{
    public CreateWorkHourRequestValidator()
    {
        RuleFor(u => u.AccountId).NotEmpty();
        RuleFor(u => u.StartHour).NotEmpty();
        RuleFor(u => u.EndHour).NotEmpty();
        RuleFor(u => u.StudyDate).NotEmpty();
    }
}