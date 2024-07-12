using Busines.Abstracts;
using Busines.Dtos.Requests.WorkHourRequests;
using Business.Dtos.Requests.FilterRequests;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.SeriLog.Logger;
using Core.DataAccess.Dynamic;
using Core.DataAccess.Paging;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorkHoursController : ControllerBase
{
    IWorkHourService _workHourService;

    public WorkHoursController(IWorkHourService workHourService)
    {
        _workHourService = workHourService;
    }


    [Logging(typeof(MsSqlLogger))]
    [Logging(typeof(FileLogger))]
    [Cache(60)]
    [HttpGet("GetList")]
    public async Task<IActionResult> GetListAsync([FromQuery] PageRequest pageRequest)
    {
        var result = await _workHourService.GetListAsync(pageRequest);
        return Ok(result);
    }



    [Logging(typeof(MsSqlLogger))]
    [Logging(typeof(FileLogger))]
    [Cache(60)]
    [HttpPost("GetListByDynamic")]
    public async Task<IActionResult> GetListByDynamic(DynamicQuery dynamicQuery, [FromQuery] PageRequest pageRequest)
    {
        var result = await _workHourService.GetListByDynamic(dynamicQuery, pageRequest);
        return Ok(result);
    }

    [Logging(typeof(MsSqlLogger))]
    [Logging(typeof(FileLogger))]
    [Cache(60)]
    [HttpPost("GetListByFiltered")]
    public async Task<IActionResult> GetListByFiltered([FromBody] WorkHourFilterRequest workHourFilterRequest , [FromQuery] PageRequest pageRequest)
    {
        var result = await _workHourService.GetListByFiltered(workHourFilterRequest, pageRequest);
        return Ok(result);
    }

    [Logging(typeof(MsSqlLogger))]
    [Logging(typeof(FileLogger))]
    [Cache]
    [HttpGet("GetById")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var result = await _workHourService.GetByIdAsync(id);
        return Ok(result);
    }

    [Logging(typeof(MsSqlLogger))]
    [Logging(typeof(FileLogger))]
    [Cache]
    [HttpGet("GetByAccountId")]
    public async Task<IActionResult> GetByAccountIdAsync([FromQuery] Guid accountId, [FromQuery] PageRequest pageRequest)
    {
        var result = await _workHourService.GetByAccountIdAsync(accountId, pageRequest);
        return Ok(result);
    }

    [Logging(typeof(MsSqlLogger))]
    [Logging(typeof(FileLogger))]
    [Cache]
    [HttpGet("GetByMonthAsync")]
    public async Task<IActionResult> GetByMonthAsync(int month, [FromQuery] PageRequest pageRequest)
    {
        var result = await _workHourService.GetByMonthAsync(month, pageRequest);
        return Ok(result);
    }

    [Logging(typeof(MsSqlLogger))]
    [Logging(typeof(FileLogger))]
    [Cache]
    [HttpGet("GetByMonthAndDayAsync")]
    public async Task<IActionResult> GetByMonthAndDayAsync(int month, int day, [FromQuery] PageRequest pageRequest)
    {
        var result = await _workHourService.GetByMonthAndDayAsync(month, day, pageRequest);
        return Ok(result);
    }



    [Logging(typeof(MsSqlLogger))]
    [Logging(typeof(FileLogger))]
    [Cache]
    [HttpGet("GetByAccountIdAndMonthAsync")]
    public async Task<IActionResult> GetByAccountIdAndMonthAsync(Guid accountId, int month, [FromQuery] PageRequest pageRequest)
    {
        var result = await _workHourService.GetByAccountIdAndMonthAsync(accountId, month, pageRequest);
        return Ok(result);
    }


    [Logging(typeof(MsSqlLogger))]
    [Logging(typeof(FileLogger))]
    [CacheRemove("WorkHours.Get")]
    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] CreateWorkHourRequest createWorkHourRequest)
    {
        var result = await _workHourService.AddAsync(createWorkHourRequest);
        return Ok(result);
    }


    [Logging(typeof(MsSqlLogger))]
    [Logging(typeof(FileLogger))]
    [CacheRemove("WorkHours.Get")]
    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateWorkHourRequest updateWorkHourRequest)
    {
        var result = await _workHourService.UpdateAsync(updateWorkHourRequest);
        return Ok(result);
    }


    [Logging(typeof(MsSqlLogger))]
    [Logging(typeof(FileLogger))]
    [CacheRemove("WorkHours.Get")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        var result = await _workHourService.DeleteAsync(id);
        return Ok(result);
    }

}