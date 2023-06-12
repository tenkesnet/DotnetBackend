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
        private readonly ITanarRepository _tanarRepo;
        public HomeController(ITanuloRepository tanuloRepo, ITanarRepository tanarRepo , DapperContext dapperContext)
        {
            _tanuloRepo = tanuloRepo;
            _tanarRepo = tanarRepo;
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
        [HttpPost]
        public async Task<string> setTanar(Tanar tanar)
        {
            _tanarRepo.setTanar(tanar);
            return "Ok";
        }
    }
}
