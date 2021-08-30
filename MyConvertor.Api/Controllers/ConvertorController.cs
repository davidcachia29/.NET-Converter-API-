using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MyConvertor.Api;
using MyConvertor.Api.Resources;
using MyConvertor.Api.Validations;
//using MyConvertor.Api.Resources;
//using MyConvertor.Api.Validations;
using MyConvertor.Core.Models;
using MyConvertor.Core.Services;

[Route("api/[controller]")]
[ApiController]
public class ConvertorController : ControllerBase{

    private readonly ICurrencyService _currencyService;    
    private readonly IMapper _mapper;

  
    public ConvertorController(ICurrencyService currencyService, IMapper mapper)
    {
        this._mapper = mapper;
        this._currencyService = currencyService;
    }

    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<Currency>>> GetAllData()
    {
        var covnertor = await _currencyService.DisplayDataAsync();
        return Ok(covnertor);
    }

    [HttpGet("GetByTarget/{target}")]
    public async Task<ActionResult<IEnumerable<Currency>>> GetAllTargetData(string target)
    {
        var covnertor = await _currencyService.DisplayTargetDataAsync(target);
        return Ok(covnertor);
    }

    [HttpGet("GetByDays/{days}")]
    public async Task<ActionResult<IEnumerable<Currency>>> GetAllDateDate(int days)
    {
        var covnertor = await _currencyService.DisplayTargetDateAsync(days);
        return Ok(covnertor);
    }
    

    [HttpPost("")]
    public async Task<ActionResult<IEnumerable<ConvertorDataGet>>> GetConvertedData([FromBody] ConvertorResource convertorResource)
    {      
        

        var convertor = await _currencyService.DisplayConvertDataAsync(convertorResource);      
          return Ok(convertor);
    }
}