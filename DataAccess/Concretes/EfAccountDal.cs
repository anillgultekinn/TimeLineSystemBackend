using Core.DataAccess.Repositories;
using DataAccess.Abstracts;
using DataAccess.Contexts;
using Entities.Concretes;

namespace DataAccess.Concretes;

public class EfAccountDal : EfRepositoryBase<Account, Guid, TimeLineContext>, IAccountDal
{
    public EfAccountDal(TimeLineContext context) : base(context)
    {
    }
}