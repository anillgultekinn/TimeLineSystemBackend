using Core.Entities;

namespace Entities.Concretes;

public class Account : Entity<Guid>
{
    public Guid UserId { get; set; }
    public string? PhoneNumber { get; set; }
    public string? NationalId { get; set; }
    public string? Description { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? ProfilePhotoPath { get; set; }

    public User User { get; set; }

    public ICollection<WorkHour> WorkHours { get; set; }


}