using Microsoft.AspNetCore.Mvc;
using Tanulok.Entity;
using Tanulok.Models;
using Tanulok.Repository;

namespace Tanulok.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AlkalmazottController :ControllerBase
    {
        private readonly IAlkalmazottRepository _tanarRepo;
        public AlkalmazottController(IAlkalmazottRepository tanarRepo, IskolaContext dapperContext)
        {
            _tanarRepo = tanarRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAlkalmazottbyAuto()
        {
            try
            {
                var companies = await _tanarRepo.GetAlkalmazottbyAuto();
                return Ok(companies);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAlkalmazottbyReszleg()
        {
            try
            {
                var companies = await _tanarRepo.GetAlkalmazottbyReszleg();
                return Ok(companies);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
    }
}
