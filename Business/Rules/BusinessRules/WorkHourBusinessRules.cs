using Business.Messages;
using Core.Business.Rules;
using DataAccess.Abstracts;

namespace Busines.Rules.BusinessRules;

public class WorkHourBusinessRules : BaseBusinessRules
{
    IWorkHourDal _workHourDal;

    public WorkHourBusinessRules(IWorkHourDal workHourDal)
    {
        _workHourDal = workHourDal;
    }

    public async Task IsExistsWorkHour(Guid workHourId)
    {
        var result = await _workHourDal.GetAsync(
            predicate: a => a.Id == workHourId,
            enableTracking: false);

        if (result == null)
        {
            throw new BusinessException(BusinessMessages.DataNotFound);
        }
    }

    public async Task WorkHourCannotBeDuplicatedWhenInserted(Guid accountId, DateTime studyDate)
    {
        var result = await _workHourDal.GetAsync(
            predicate: a => a.AccountId == accountId
                && a.StudyDate.Date == studyDate.Date,
            enableTracking: false);

        if (result != null)
        {
            throw new BusinessException(BusinessMessages.DataAvailable);
        }
    }
}