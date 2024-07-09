using AutoMapper;
using Busines.Abstracts;
using Busines.Dtos.Requests.WorkHourRequests;
using Busines.Dtos.Responses.WorkHourResponse;
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

        public WorkHourManager(IWorkHourDal workHourDal, IMapper mapper)
        {
            _workHourDal = workHourDal;
            _mapper = mapper;
        }

        public async Task<CreatedWorkHourResponse> AddAsync(CreateWorkHourRequest createWorkHourRequest)
        {
            WorkHour workHour = _mapper.Map<WorkHour>(createWorkHourRequest);
            WorkHour createdWorkHour = await _workHourDal.AddAsync(workHour);
            CreatedWorkHourResponse createdWorkHourResponse = _mapper.Map<CreatedWorkHourResponse>(createdWorkHour);
            return createdWorkHourResponse;
        }

        public async Task<DeletedWorkHourResponse> DeleteAsync(Guid id)
        {
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

        public async Task<IPaginate<GetListWorkHourResponse>> GetByAccountIdAsync(Guid accountId)
        {
            var workHour = await _workHourDal.GetListAsync(
                predicate: u => u.AccountId == accountId,
                include: w => w.Include(w => w.Account),
                enableTracking: false);
            var mappedWorkHour = _mapper.Map<Paginate<GetListWorkHourResponse>>(workHour);
            return mappedWorkHour;
        }

        public async Task<IPaginate<GetListWorkHourResponse>> GetListAsync(PageRequest pageRequest)
        {
            var workHour = await _workHourDal.GetListAsync(
            include: w => w.Include(w => w.Account),
            index: pageRequest.PageIndex,
            size: pageRequest.PageSize);

            var mappedWorkHours = _mapper.Map<Paginate<GetListWorkHourResponse>>(workHour);
            return mappedWorkHours;

        }

        public async Task<UpdatedWorkHourResponse> UpdateAsync(UpdateWorkHourRequest updateWorkHourRequest)
        {
            WorkHour workHour = _mapper.Map<WorkHour>(updateWorkHourRequest);
            WorkHour updatedWorkHour = await _workHourDal.AddAsync(workHour);
            UpdatedWorkHourResponse updatedWorkHourResponse = _mapper.Map<UpdatedWorkHourResponse>(updatedWorkHour);
            return updatedWorkHourResponse;
        }

        public async Task<IPaginate<GetListWorkHourResponse>> GetByMonthAsync(int month)
        {
            var workHour = await _workHourDal.GetListAsync(
                 predicate: u => u.StudyDate.Month == month,
                 include: w => w.Include(w => w.Account),
                 enableTracking: false);

            var mappedWorkHours = _mapper.Map<Paginate<GetListWorkHourResponse>>(workHour);
            return mappedWorkHours;

        }

        public async Task<IPaginate<GetListWorkHourResponse>> GetByMonthAndDayAsync(int month, int day)
        {
            var workHour = await _workHourDal.GetListAsync(
               predicate: u => u.StudyDate.Month == month && u.StudyDate.Day == day,
               include: w => w.Include(w => w.Account),
               enableTracking: false);

            var mappedWorkHours = _mapper.Map<Paginate<GetListWorkHourResponse>>(workHour);
            return mappedWorkHours;
        }

        public async Task<IPaginate<GetListWorkHourResponse>> GetByAccountIdAndMonthAsync(Guid accountId, int month)
        {
            var workHour = await _workHourDal.GetListAsync(
             predicate: u => u.StudyDate.Month == month && u.AccountId == accountId,
             include: w => w.Include(w => w.Account),
             enableTracking: false);

            var mappedWorkHours = _mapper.Map<Paginate<GetListWorkHourResponse>>(workHour);
            return mappedWorkHours;
        }
    }
}