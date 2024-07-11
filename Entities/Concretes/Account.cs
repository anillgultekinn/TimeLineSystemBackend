using Core.Entities;

namespace Entities.Concretes;

public class Account : Entity<Guid>
{
    public Guid UserId { get; set; }
    public User User { get; set; }

    public ICollection<WorkHour> WorkHours { get; set; }
}