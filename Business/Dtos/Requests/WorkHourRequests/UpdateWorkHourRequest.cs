namespace Busines.Dtos.Requests.WorkHourRequests;

public class UpdateWorkHourRequest
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime StartHour { get; set; }
    public DateTime EndHour { get; set; }
    public DateTime StudyDate { get; set; }
}