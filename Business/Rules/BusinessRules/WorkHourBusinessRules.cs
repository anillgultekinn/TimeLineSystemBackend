using Business.Messages;
using Core.Business.Rules;
using DataAccess.Abstracts;
using DataAccess.Concretes;

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
}