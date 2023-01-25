using LogisticsApp.Domain.Models;
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
    public class ValidationsController : ControllerBase
    {
        private IValidationService _validationService;

        public ValidationsController(IValidationService validationService)
        {
            _validationService = validationService;
        }

        [HttpGet]
        public ActionResult<List<ValidationDto>> Get()
        {
            try
            {
                return Ok(_validationService.GetAllValidations());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred! Contact the admin!");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<ValidationDto> GetById(int id)
        {
            try
            {
                var validationDto = _validationService.GetById(id);
                return Ok(validationDto);
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

        [HttpPost("addValidation")]
        public IActionResult AddValidation([FromBody] AddValidationDto addValidationDto)
        {
            try
            {
                _validationService.AddValidation(addValidationDto);
                return StatusCode(StatusCodes.Status201Created, "Validation added to the system.");
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

        [HttpDelete("{id}")]
        public ActionResult<ValidationDto> Delete(int id)
        {
            try
            {
                _validationService.DeleteValidation(id);
                return Ok($"Validation with id {id} successfully deleted.");
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
