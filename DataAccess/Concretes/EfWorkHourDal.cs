using Core.DataAccess.Repositories;
using DataAccess.Abstracts;
using DataAccess.Contexts;
using Entities;

namespace DataAccess.Concretes;

public class EfWorkHourDal : EfRepositoryBase<WorkHour, Guid, TimeLineContext>, IWorkHourDal
{
    public EfWorkHourDal(TimeLineContext context) : base(context)
    {
    }
}