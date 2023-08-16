using Microsoft.AspNetCore.Mvc;
using Tanulok.Entity;
using Tanulok.Repository;

namespace Tanulok.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AlkalmazottController :ControllerBase
    {
        private readonly IAlkalmazottRepository _tanarRepo;
        public AlkalmazottController(IAlkalmazottRepository tanarRepo, DapperContext dapperContext)
        {
            _tanarRepo = tanarRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAlkalmazott()
        {
            try
            {
                var companies = await _tanarRepo.GetAlkalmazott();
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
