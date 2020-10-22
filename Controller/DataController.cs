using Iot_app.Dtos;
using Iot_app.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Iot_app.Controller
{
    [ApiController]//serve http response, atribute http routing
    [Route("[controller]")]//controller can be aess by name: character
    public class DataController: ControllerBase
    {
        private readonly IDataService _dataService;
        public DataController(IDataService dataService)
        {
            _dataService = dataService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetList()
        {
            return Ok(await _dataService.GetDataList());
        }
        [HttpGet("GetAllbme")]
        public async Task<IActionResult> GetBMEList()
        {
            return Ok(await _dataService.GetBMEList());
        }
        [HttpGet("GetPart/{length}")]
        public async Task<IActionResult> GetPart(int length)
        {
            return Ok(await _dataService.GetDataDefinedList(length));
        }

        [HttpGet("GetbmePart/{length}")]
        public async Task<IActionResult> GetbmePart(int length)
        {
            return Ok(await _dataService.GetPartBMEList(length));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            return Ok(await _dataService.GetData(id));
        }
        [HttpPost]
        public async Task<IActionResult> AddDHT(AddDataDto newData)
        {

            return Ok(await _dataService.AddData(newData));
        }
        [HttpPost]
        public async Task<IActionResult> AddBME(AddBMEDto newData)
        {

            return Ok(await _dataService.AddBMEData(newData));
        }
    }
}
