using Core.Entities;
using Entities.Concretes;

namespace Entities;

public class WorkHour : Entity<Guid>
{
    public Guid AccountId{ get; set; }
    public TimeSpan StartHour { get; set; } 
    public TimeSpan EndHour { get; set; } 
    public DateTime StudyDate { get; set; } 
    public Account Account { get; set; }
}