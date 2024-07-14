using AutoMapper;
using Busines.Abstracts;
using Busines.Dtos.Requests.WorkHourRequests;
using Busines.Dtos.Responses.WorkHourResponse;
using Busines.Rules.BusinessRules;
using Business.Dtos.Requests.FilterRequests;
using Core.DataAccess.Dynamic;
using Core.DataAccess.Paging;
using DataAccess.Abstracts;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

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
                orderBy: u => u.OrderByDescending(u => u.StudyDate),
                include: w => w.Include(w => w.Account)
                .ThenInclude(w => w.User),
                enableTracking: false); ;
            var mappedWorkHour = _mapper.Map<Paginate<GetListWorkHourResponse>>(workHour);
            return mappedWorkHour;
        }

        public async Task<IPaginate<GetListWorkHourResponse>> GetListAsync(PageRequest pageRequest)
        {
            var workHour = await _workHourDal.GetListAsync(
            include: w => w.Include(w => w.Account)
            .ThenInclude(w => w.User),
            orderBy: u => u.OrderByDescending(u => u.StudyDate),
            index: pageRequest.PageIndex,
            size: pageRequest.PageSize);

            var mappedWorkHours = _mapper.Map<Paginate<GetListWorkHourResponse>>(workHour);
            return mappedWorkHours;

        }

        public async Task<UpdatedWorkHourResponse> UpdateAsync(UpdateWorkHourRequest updateWorkHourRequest)
        {
            await _workHourBusinessRules.IsExistsWorkHour(updateWorkHourRequest.Id);

            WorkHour workHour = _mapper.Map<WorkHour>(updateWorkHourRequest);
            WorkHour updatedWorkHour = await _workHourDal.UpdateAsync(workHour);
            UpdatedWorkHourResponse updatedWorkHourResponse = _mapper.Map<UpdatedWorkHourResponse>(updatedWorkHour);
            return updatedWorkHourResponse;
        }

        public async Task<IPaginate<GetListWorkHourResponse>> GetByMonthAsync(int month, PageRequest pageRequest)
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
                 include: w => w.Include(w => w.Account)
                    .ThenInclude(w => w.User),
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
                include: w => w.Include(w => w.Account)
                   .ThenInclude(w => w.User),
                enableTracking: false);

            var mappedWorkHours = _mapper.Map<Paginate<GetListWorkHourResponse>>(workHour);
            return mappedWorkHours;
        }

        public async Task<IPaginate<GetListWorkHourResponse>> GetListByDynamic(DynamicQuery dynamicQuery, PageRequest pageRequest)
        {
            var workHourDynamic = await _workHourDal.GetListByDynamicAsync(
             dynamic: dynamicQuery,
             include: w => w.Include(w => w.Account)
                .ThenInclude(w => w.User),
             index: pageRequest.PageIndex,
             size: pageRequest.PageSize,
             enableTracking: false);
            var mappedWorkHours = _mapper.Map<Paginate<GetListWorkHourResponse>>(workHourDynamic);
            return mappedWorkHours;
        }


        public async Task<IPaginate<GetListWorkHourResponse>> GetListByFiltered(WorkHourFilterRequest workHourFilterRequest, PageRequest pageRequest)
        {
            // Filtre koşullarını hazırlayın
            bool filterByAccount = workHourFilterRequest.RequestingAccountId.ToString() != "-1";
            bool filterByMonth = workHourFilterRequest.Month != "-1";

            //filterByAccount: İstek yapılan hesap ID'si "-1" değilse true olur ve bu durumda hesap ID'sine göre filtreleme yapılacaktır.
            //filterByMonth: İstek yapılan ay "-1" değilse true olur ve bu durumda aya göre filtreleme yapılacaktır.

            // Sorguyu oluşturun
            IQueryable<WorkHour> query = _workHourDal.Query();


            if (filterByAccount)
            {
                query = query.Where(wh => wh.AccountId.ToString() == workHourFilterRequest.RequestingAccountId);
            }

            if (filterByMonth)
            {
                query = query.Where(wh => wh.StudyDate.Month.ToString() == workHourFilterRequest.Month);
            }

            // İlgili varlıkları dahil edin ve sorguyu çalıştırın
            var workHourList = await query.OrderByDescending(u => u.StudyDate)
                .Include(wh => wh.Account)
                    .ThenInclude(a => a.User).ToPaginateAsync(pageRequest.PageIndex, pageRequest.PageSize);


            // Filtrelenmiş sonuçları mapleyin
            var mappedWorkHours = _mapper.Map<Paginate<GetListWorkHourResponse>>(workHourList);
            return mappedWorkHours;
        }

    }
}