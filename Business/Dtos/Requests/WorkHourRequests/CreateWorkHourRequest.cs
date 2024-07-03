namespace Busines.Dtos.Requests.WorkHourRequests;

public class CreateWorkHourRequest
{
    public Guid UserId { get; set; }
    public DateTime StartHour { get; set; }
    public DateTime EndHour { get; set; }
    public DateTime StudyDate { get; set; }
}