using Microsoft.AspNetCore.Mvc;
using Tanulok.Entity;
using Tanulok.Repository;

namespace Tanulok.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TanarController :ControllerBase
    {
        private readonly ITanarRepository _tanarRepo;
        public TanarController(ITanarRepository tanarRepo, DapperContext dapperContext)
        {
            _tanarRepo = tanarRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetTanarok()
        {
            try
            {
                var companies = await _tanarRepo.GetTanarok();
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
