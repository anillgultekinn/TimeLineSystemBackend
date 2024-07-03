using Core.Entities;

namespace Entities;

public class WorkHour : Entity<Guid>
{
    public Guid UserId { get; set; }
    public DateTime StartHour { get; set; } 
    public DateTime EndHour { get; set; } 
    public DateTime StudyDate { get; set; } 
    public User User { get; set; }
}