namespace Busines.Dtos.Responses.WorkHourResponse;

public class UpdatedWorkHourResponse
{
    public Guid Id { get; set; }
    public Guid AccountId { get; set; }
    public string StartHour { get; set; }
    public string EndHour { get; set; }
    public DateTime StudyDate { get; set; }
}