namespace Busines.Dtos.Requests.WorkHourRequests;

public class CreateWorkHourRequest
{
    public Guid AccountId { get; set; }
    public TimeSpan StartHour { get; set; }
    public TimeSpan EndHour { get; set; }
    public DateTime StudyDate { get; set; }
}