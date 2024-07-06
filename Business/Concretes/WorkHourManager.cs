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

        public async Task<GetListWorkHourResponse> GetByUserIdAsync(Guid? id)
        {
            var workHour = await _workHourDal.GetListAsync(
                include: w => w.Include(w => w.Account),
        predicate: u => u.Id == id,
        enableTracking: false);

            GetListWorkHourResponse getListWorkHourResponse = _mapper.Map<GetListWorkHourResponse>(workHour);
            return getListWorkHourResponse;
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


        public async Task<IPaginate<GetListWorkHourResponse>> GetListDateAsync(PageRequest pageRequest, int month, int year)
        {
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1).AddSeconds(-1);

            var workHoursQuery = _workHourDal.Query()
                .Include(w => w.Account) // Manuel olarak Include ekliyoruz
                .Where(w => w.StudyDate >= startDate && w.StudyDate <= endDate);

            var totalCount = await workHoursQuery.CountAsync(); // Toplam kayıt sayısını hesapla

            var workHours = await workHoursQuery
                .Skip(pageRequest.PageIndex * pageRequest.PageSize)
                .Take(pageRequest.PageSize)
                .ToListAsync();

            var mappedWorkHours = _mapper.Map<List<GetListWorkHourResponse>>(workHours);

            // IPaginate<GetListWorkHourResponse> türünde bir nesne oluştur
            var paginatedResult = new Paginate<GetListWorkHourResponse>
            {
                Items = mappedWorkHours,
                Index = pageRequest.PageIndex,
                Size = pageRequest.PageSize,
                Count = totalCount,
                Pages = (int)Math.Ceiling(totalCount / (double)pageRequest.PageSize),
                From = pageRequest.PageIndex * pageRequest.PageSize + 1,
            };

            return paginatedResult;
        }


        public async Task<UpdatedWorkHourResponse> UpdateAsync(UpdateWorkHourRequest updateWorkHourRequest)
        {
            WorkHour workHour = _mapper.Map<WorkHour>(updateWorkHourRequest);
            WorkHour updatedWorkHour = await _workHourDal.AddAsync(workHour);
            UpdatedWorkHourResponse updatedWorkHourResponse = _mapper.Map<UpdatedWorkHourResponse>(updatedWorkHour);
            return updatedWorkHourResponse;
        }
    }
}