using Microsoft.AspNetCore.Mvc;
using Tanulok.Entity;
using Tanulok.Mapper;
using Tanulok.Models;
using Tanulok.Repository;

namespace Tanulok.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AutoController :ControllerBase
    {
        private readonly IAutoEFRepository _tanarRepo;
        public AutoController(IAutoEFRepository tanarRepo, IskolaContext dapperContext)
        {
            _tanarRepo = tanarRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAutoByRendeles()
        {
            try
            {
                var companies = await _tanarRepo.GetAutoByRendeles();
                return Ok(companies);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAutoByAlkalmazott()
        {
            try
            {
                var autok = await _tanarRepo.GetAutoByAlkalmazott();
                AutoSzervizMapper autoSzervizMapper = new AutoSzervizMapper(_tanarRepo);
                var b = autok.ToList();
                return Ok(autoSzervizMapper.AutoToAutoSzervizDTOs(b));

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAutoByTipusok()
        {
            try
            {
                var companies = await _tanarRepo.GetAutoByTipusok();
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
