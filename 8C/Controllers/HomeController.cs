using Microsoft.AspNetCore.Mvc;
using Tanulok.Entity;
using Tanulok.Helper;
using Tanulok.Repository;

namespace Tanulok.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class HomeController : ControllerBase
    {
        private readonly ITanuloRepository _tanuloRepo;
        private readonly ITanarRepository _tanarRepo;
        private readonly ILakcimRepository _lakcimRepo;
        public HomeController(ITanuloRepository tanuloRepo, ITanarRepository tanarRepo, DapperContext dapperContext, ILakcimRepository lakcimRepo)
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
        public async Task<ValidationResult<Tanar>> setTanar(Tanar tanar, ILakcimRepository lakcimRepository, ITanarRepository tanarRepository)
        {
            ValidationResult<Tanar> validationResult = await TanarHelper.validateTanar(tanar, _lakcimRepo, _tanarRepo);
            if (validationResult.isValid)
            {
                if (validationResult.result.lakcim.id == 0)
                {
                    long lakcimId = await lakcimRepository.setLakcim(tanar.lakcim);
                    tanar.lakcim.id = lakcimId;
                    long tanarId = await tanarRepository.setTanar(tanar);
                    tanar.id = tanarId;
                    validationResult.result = tanar;
                }
                else
                {
                    tanar.lakcim = validationResult.result.lakcim;
                    long tanarId = await tanarRepository.setTanar(tanar);
                    tanar.id = tanarId;
                    validationResult.result = tanar;
                }
            }
            return validationResult;
        }

        [HttpPost]
        public async Task<ValidationResult<Tanulo>> setTanulo(Tanulo tanulo, ILakcimRepository lakcimRepository, ITanuloRepository tanuloRepository)
        {
            ValidationResult<Tanulo> validationResult = await TanuloHelper.validateTanulo(tanulo, _lakcimRepo, _tanuloRepo);
            if (validationResult.isValid)
            {
                Lakcim lakcim = lakcimRepository.getLakcimByObject(tanulo.lakcim);
                if (lakcim == null)
                {
                    long lakcimId = await lakcimRepository.setLakcim(tanulo.lakcim);
                    tanulo.lakcim.id = lakcimId;
                    long tanuloId = await tanuloRepository.setTanulo(tanulo);
                    tanulo.id = tanuloId;
                    validationResult.result = tanulo;
                }
                else
                {
                    tanulo.lakcim = lakcim;                  
                    long tanuloId = await tanuloRepository.setTanulo(tanulo);
                    tanulo.id = tanuloId; 
                    validationResult.result = tanulo;
                }
            }
            return validationResult;
        }
        [HttpPost]
        public async Task<ValidationResult<Lakcim>> setLakcim(Lakcim lakcim)
        {
            ValidationResult<Lakcim> validationResult = LakcimHelper.validateLakcim(lakcim,_lakcimRepo);
            if (validationResult.isValid)
            {
                if (validationResult.result.id==0)
                {
                    long id = await _lakcimRepo.setLakcim(lakcim);
                    validationResult.result.id = id;
                }
                //return validationResult;
            }
            return validationResult;
        }
    }
}
