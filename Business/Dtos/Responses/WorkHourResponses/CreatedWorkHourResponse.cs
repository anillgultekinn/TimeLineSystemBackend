﻿namespace Busines.Dtos.Responses.WorkHourResponse;

public class CreatedWorkHourResponse
{
    public Guid Id { get; set; }
    public Guid AccountId { get; set; }
    public string StartHour { get; set; }
    public string EndHour { get; set; }
    public DateTime StudyDate { get; set; }
}