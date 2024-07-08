using Busines.Dtos.Requests.WorkHourRequests;
using Busines.Dtos.Responses.WorkHourResponse;
using Core.DataAccess.Paging;

namespace Busines.Abstracts;

public interface IWorkHourService
{
    Task<IPaginate<GetListWorkHourResponse>> GetListAsync(PageRequest pageRequest);
    Task<CreatedWorkHourResponse> AddAsync(CreateWorkHourRequest createWorkHourRequest);
    Task<UpdatedWorkHourResponse> UpdateAsync(UpdateWorkHourRequest updateWorkHourRequest);
    Task<DeletedWorkHourResponse> DeleteAsync(Guid id);
    Task<GetWorkHourResponse> GetByIdAsync(Guid? id);
    Task<IPaginate<GetListWorkHourResponse>> GetByAccountIdAsync(Guid accountId);
    Task<IPaginate<GetListWorkHourResponse>> GetByMonthAsync(int month);
    Task<IPaginate<GetListWorkHourResponse>> GetByMonthAndDayAsync(int month, int day);
}