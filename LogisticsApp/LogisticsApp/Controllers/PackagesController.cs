using LogisticsApp.DTOs;
using LogisticsApp.Services.Implementations;
using LogisticsApp.Services.Interfaces;
using LogisticsApp.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackagesController : ControllerBase
    {
        private IPackageService _packageService;

        public PackagesController(IPackageService packageService)
        {
            _packageService = packageService;
        }

        [HttpGet]
        public ActionResult<List<PackageDto>> Get()
        {
            try
            {
                return Ok(_packageService.GetAllPackages());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred! Contact the admin!");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<PackageDto> GetById(int id)
        {
            try
            {
                var packageDto = _packageService.GetById(id);
                return Ok(packageDto);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred! Contact the admin!");
            }
        }

        [HttpPost("addPackage")]
        public IActionResult AddPackage([FromBody] AddPackageDto addPackageDto)
        {
            try
            {
                _packageService.AddPackage(addPackageDto);
                return StatusCode(StatusCodes.Status201Created, "Package was added to the system.");
            }
            catch (InvalidEntryException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred! Contact the admin!");
            }
        }

        [HttpDelete("deletePackage/{id}")]
        public ActionResult<PackageDto> Delete(int id)
        {
            try
            {
                _packageService.DeletePackage(id);
                return Ok($"Package with id {id} successfully deleted.");
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred! Contact the admin!");
            }
        }
    }
}
