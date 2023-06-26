using Microsoft.AspNetCore.Mvc;
using Tanulok.Entity;
using Tanulok.Helper;
using Tanulok.Repository;

namespace Tanulok.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class HomeController :ControllerBase
    {
        private readonly ITanuloRepository _tanuloRepo;
        private readonly ITanarRepository _tanarRepo;
        private readonly ILakcimRepository _lakcimRepo;
        public HomeController(ITanuloRepository tanuloRepo, ITanarRepository tanarRepo , DapperContext dapperContext, ILakcimRepository lakcimRepo)
        {
            _tanuloRepo = tanuloRepo;
            _tanarRepo = tanarRepo;
            _lakcimRepo = lakcimRepo;
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
        public async Task<IActionResult> GetLakcim()
        {
            try
            {
                var companies = await _lakcimRepo.GetLakcim();
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
        public async Task<ValidationResult<Tanar>> setTanar(Tanar tanar)
        {
            ValidationResult<Tanar> validationResult= await TanarHelper.validateTanar(tanar, _lakcimRepo, _tanarRepo);
            if (validationResult.isValid)
            {
                //_tanarRepo.setTanar(tanar);
                //return validationResult;
            }
            return validationResult;
        }

        /*[HttpPost]
        public async Task<ValidationResult<Tanulo>> setTanulo(Tanulo tanulo)
        {
            ValidationResult validationResult = TanuloHelper.validateTanulo(tanulo);
            if (validationResult.isValid)
            {
                _tanuloRepo.setTanulo(tanulo);
                //return validationResult;  
            }
            return validationResult;
        }
        [HttpPost]
        public async Task<ValidationResult> SetLakcim(Lakcim lakcim)
        {
            ValidationResult validationResult = LakcimHelper.validateLakcim(lakcim);
            if (validationResult.isValid)
            {
                _lakcimRepo.SetLakcim(lakcim);
                //return validationResult;
            }
            return validationResult;
        }*/
    }
}
