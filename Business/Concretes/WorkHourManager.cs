using AutoMapper;
using Busines.Abstracts;
using Busines.Dtos.Requests.WorkHourRequests;
using Busines.Dtos.Responses.WorkHourResponse;
using Busines.Rules.BusinessRules;
using Core.DataAccess.Dynamic;
using Core.DataAccess.Paging;
using DataAccess.Abstracts;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Busines.Concretes
{
    public class WorkHourManager : IWorkHourService
    {
        IWorkHourDal _workHourDal;
        IMapper _mapper;
        WorkHourBusinessRules _workHourBusinessRules;


        public WorkHourManager(IWorkHourDal workHourDal, IMapper mapper, WorkHourBusinessRules workHourBusinessRules)
        {
            _workHourDal = workHourDal;
            _mapper = mapper;
            _workHourBusinessRules = workHourBusinessRules;
        }

        public async Task<CreatedWorkHourResponse> AddAsync(CreateWorkHourRequest createWorkHourRequest)
        {

            await _workHourBusinessRules.WorkHourCannotBeDuplicatedWhenInserted(
       createWorkHourRequest.AccountId,
       createWorkHourRequest.StudyDate);
            WorkHour workHour = _mapper.Map<WorkHour>(createWorkHourRequest);
            WorkHour createdWorkHour = await _workHourDal.AddAsync(workHour);
            CreatedWorkHourResponse createdWorkHourResponse = _mapper.Map<CreatedWorkHourResponse>(createdWorkHour);
            return createdWorkHourResponse;
        }

        public async Task<DeletedWorkHourResponse> DeleteAsync(Guid id)
        {
            await _workHourBusinessRules.IsExistsWorkHour(id);
            WorkHour workHour = await _workHourDal.GetAsync(predicate: u => u.Id == id);
            WorkHour deletedWorkHour = await _workHourDal.DeleteAsync(workHour);
            DeletedWorkHourResponse deletedWorkHourResponse = _mapper.Map<DeletedWorkHourResponse>(deletedWorkHour);
            return deletedWorkHourResponse;

        }

        public async Task<GetWorkHourResponse> GetByIdAsync(Guid? id)
        {

            WorkHour workHour = await _workHourDal.GetAsync(
                predicate: w => w.Id == id,
                enableTracking: false);

            GetWorkHourResponse getWorkHourResponse = _mapper.Map<GetWorkHourResponse>(workHour);
            return getWorkHourResponse;
        }

        public async Task<IPaginate<GetListWorkHourResponse>> GetByAccountIdAsync(Guid accountId, PageRequest pageRequest)
        {
            var workHour = await _workHourDal.GetListAsync(
                 index: pageRequest.PageIndex,
                  size: pageRequest.PageSize,
                predicate: u => u.AccountId == accountId,
                include: w => w.Include(w => w.Account)
                .ThenInclude(w=>w.User),
                enableTracking: false);
            var mappedWorkHour = _mapper.Map<Paginate<GetListWorkHourResponse>>(workHour);
            return mappedWorkHour;
        }

        public async Task<IPaginate<GetListWorkHourResponse>> GetListAsync(PageRequest pageRequest)
        {
            var workHour = await _workHourDal.GetListAsync(
            include: w => w.Include(w => w.Account)
            .ThenInclude(w => w.User),
            index: pageRequest.PageIndex,
            size: pageRequest.PageSize);

            var mappedWorkHours = _mapper.Map<Paginate<GetListWorkHourResponse>>(workHour);
            return mappedWorkHours;

        }

        public async Task<UpdatedWorkHourResponse> UpdateAsync(UpdateWorkHourRequest updateWorkHourRequest)
        {
            await _workHourBusinessRules.IsExistsWorkHour(updateWorkHourRequest.Id);

            WorkHour workHour = _mapper.Map<WorkHour>(updateWorkHourRequest);
            WorkHour updatedWorkHour = await _workHourDal.AddAsync(workHour);
            UpdatedWorkHourResponse updatedWorkHourResponse = _mapper.Map<UpdatedWorkHourResponse>(updatedWorkHour);
            return updatedWorkHourResponse;
        }

        public async Task<IPaginate<GetListWorkHourResponse>> GetByMonthAsync(int month,PageRequest pageRequest)
        {
            var workHour = await _workHourDal.GetListAsync(
                    index: pageRequest.PageIndex,
                 size: pageRequest.PageSize,
                 predicate: u => u.StudyDate.Month == month,
                 include: w => w.Include(w => w.Account),
                 enableTracking: false);

            var mappedWorkHours = _mapper.Map<Paginate<GetListWorkHourResponse>>(workHour);
            return mappedWorkHours;

        }

        public async Task<IPaginate<GetListWorkHourResponse>> GetByMonthAndDayAsync(int month, int day, PageRequest pageRequest)
        {
            var workHour = await _workHourDal.GetListAsync(
                 index: pageRequest.PageIndex,
                 size: pageRequest.PageSize,
                 predicate: u => u.StudyDate.Month == month && u.StudyDate.Day == day,
                 include: w => w.Include(w => w.Account),
                 enableTracking: false);

            var mappedWorkHours = _mapper.Map<Paginate<GetListWorkHourResponse>>(workHour);
            return mappedWorkHours;
        }

        public async Task<IPaginate<GetListWorkHourResponse>> GetByAccountIdAndMonthAsync(Guid accountId, int month, PageRequest pageRequest)
        {
            var workHour = await _workHourDal.GetListAsync(
                 index: pageRequest.PageIndex,
                 size: pageRequest.PageSize,
                predicate: u => u.StudyDate.Month == month && u.AccountId == accountId,
                include: w => w.Include(w => w.Account),
                enableTracking: false);

            var mappedWorkHours = _mapper.Map<Paginate<GetListWorkHourResponse>>(workHour);
            return mappedWorkHours;
        }

        public async Task<IPaginate<GetListWorkHourResponse>> GetListByFiltered(DynamicQuery dynamicQuery, PageRequest pageRequest)
        {
            var workHourDynamic = await _workHourDal.GetListByDynamicAsync(
             dynamic: dynamicQuery,
             include: w => w.Include(w => w.Account),
             index: pageRequest.PageIndex,
             size: pageRequest.PageSize,
             enableTracking: false);
            var mappedWorkHours = _mapper.Map<Paginate<GetListWorkHourResponse>>(workHourDynamic);
            return mappedWorkHours;
        }


    }
}