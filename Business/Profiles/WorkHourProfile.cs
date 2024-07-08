using AutoMapper;
using Busines.Dtos.Requests.WorkHourRequests;
using Busines.Dtos.Responses.WorkHourResponse;
using Core.DataAccess.Paging;
using Entities;

namespace Busines.Profiles;

public class WorkHourProfile : Profile
{
    public WorkHourProfile()
    {
        CreateMap<WorkHour, CreateWorkHourRequest>().ReverseMap();
        CreateMap<WorkHour, CreatedWorkHourResponse>().ReverseMap();

        CreateMap<WorkHour, UpdateWorkHourRequest>().ReverseMap();
        CreateMap<WorkHour, UpdatedWorkHourResponse>().ReverseMap();

        CreateMap<WorkHour, DeletedWorkHourResponse>().ReverseMap();


        CreateMap<IPaginate<WorkHour>, Paginate<GetListWorkHourResponse>>().ReverseMap();
        CreateMap<WorkHour, GetListWorkHourResponse>().ReverseMap();

        CreateMap<WorkHour, GetWorkHourResponse>().ReverseMap();
    }
}