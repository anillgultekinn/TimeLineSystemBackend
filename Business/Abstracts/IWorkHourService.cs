using Busines.Dtos.Requests.WorkHourRequests;
using Busines.Dtos.Responses.WorkHourResponse;
using Business.Dtos.Requests.FilterRequests;
using Core.DataAccess.Dynamic;
using Core.DataAccess.Paging;

namespace Busines.Abstracts;

public interface IWorkHourService
{
    Task<IPaginate<GetListWorkHourResponse>> GetListAsync(PageRequest pageRequest);
    Task<CreatedWorkHourResponse> AddAsync(CreateWorkHourRequest createWorkHourRequest);
    Task<UpdatedWorkHourResponse> UpdateAsync(UpdateWorkHourRequest updateWorkHourRequest);
    Task<DeletedWorkHourResponse> DeleteAsync(Guid id);
    Task<GetWorkHourResponse> GetByIdAsync(Guid? id);
    Task<IPaginate<GetListWorkHourResponse>> GetByAccountIdAsync(Guid accountId, PageRequest pageRequest);
    Task<IPaginate<GetListWorkHourResponse>> GetByMonthAsync(int month, PageRequest pageRequest);
    Task<IPaginate<GetListWorkHourResponse>> GetByMonthAndDayAsync(int month, int day, PageRequest pageRequest);
    Task<IPaginate<GetListWorkHourResponse>> GetByAccountIdAndMonthAsync(Guid accountId, int month, PageRequest pageRequest);
    Task<IPaginate<GetListWorkHourResponse>> GetListByDynamic(DynamicQuery dynamicQuery, PageRequest pageRequest);
    Task<IPaginate<GetListWorkHourResponse>> GetListByFiltered(WorkHourFilterRequest workHourFilterRequest, PageRequest pageRequest);
}