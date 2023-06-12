using Microsoft.AspNetCore.Mvc;
using Tanulok.Model;
using Tanulok.Repository;

namespace Tanulok.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class HomeController :ControllerBase
    {
        private readonly ITanuloRepository _tanuloRepo;
        public HomeController(ITanuloRepository tanuloRepo, DapperContext dapperContext)
        {
            _tanuloRepo = tanuloRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetTanulok()
        {
            try
            {
                var companies = await _tanuloRepo.GetTanulok();
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
