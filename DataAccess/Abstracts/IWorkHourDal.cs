using Core.DataAccess.Repositories;
using Entities;

namespace DataAccess.Abstracts;

public interface IWorkHourDal : IRepository<WorkHour, Guid>, IAsyncRepository<WorkHour, Guid>
{
}