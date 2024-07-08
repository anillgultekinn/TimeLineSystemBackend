namespace Busines.Dtos.Responses.WorkHourResponse;

public class GetListWorkHourResponse
{
    public Guid Id { get; set; }
    public Guid AccountId { get; set; }
    public TimeSpan StartHour { get; set; }
    public TimeSpan EndHour { get; set; }
    public DateTime StudyDate { get; set; }
}