using Busines.Dtos.Requests.WorkHourRequests;
using FluentValidation;

namespace Busines.Rules.ValidationRules.FluentValidation.WorkHourValidators;

public class UpdateWorkHourRequestValidator : AbstractValidator<UpdateWorkHourRequest>
{
    public UpdateWorkHourRequestValidator()
    {
        RuleFor(u => u.UserId).NotEmpty();
        RuleFor(u => u.StartHour).NotEmpty();
        RuleFor(u => u.EndHour).NotEmpty();
        RuleFor(u => u.StudyDate).NotEmpty();
    }
}