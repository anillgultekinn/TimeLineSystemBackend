using AutoMapper;
using Busines.Dtos.Requests.WorkHourRequests;
using Busines.Dtos.Responses.WorkHourResponse;
using Business.Dtos.Responses.AccountResponses;
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


        CreateMap<WorkHour, GetListWorkHourResponse>()
       .ForMember(destinationMember: response => response.FirstName,
       memberOptions: a => a.MapFrom(a => a.Account.User.FirstName))
         .ForMember(destinationMember: response => response.LastName,
       memberOptions: a => a.MapFrom(a => a.Account.User.LastName))
       .ForMember(destinationMember: response => response.Email,
       memberOptions: a => a.MapFrom(a => a.Account.User.Email));

        CreateMap<WorkHour, GetWorkHourResponse>()
         .ForMember(destinationMember: response => response.FirstName,
       memberOptions: a => a.MapFrom(a => a.Account.User.FirstName))
         .ForMember(destinationMember: response => response.LastName,
       memberOptions: a => a.MapFrom(a => a.Account.User.LastName))
       .ForMember(destinationMember: response => response.Email,
       memberOptions: a => a.MapFrom(a => a.Account.User.Email));
    }
}