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
    public class CalculationsController : ControllerBase
    {
        private ICalculationService _calculationService;

        public CalculationsController(ICalculationService calculationService)
        {
            _calculationService = calculationService;
        }

        [HttpGet]
        public ActionResult<List<CalculationDto>> Get()
        {
            try
            {
                return Ok(_calculationService.GetAllCalculations());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred! Contact the admin!");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<CalculationDto> GetById(int id)
        {
            try
            {
                var calculationDto = _calculationService.GetById(id);
                return Ok(calculationDto);
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

        [HttpPost("addCalculation")]
        public IActionResult AddCalculation([FromBody] AddCalculationDto addCalculationDto)
        {
            try
            {
                _calculationService.AddCalculation(addCalculationDto);
                return StatusCode(StatusCodes.Status201Created, "Calculation added to the system.");
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
        public ActionResult<CalculationDto> Delete(int id)
        {
            try
            {
                _calculationService.DeleteCalculation(id);
                return Ok($"Calculation with id {id} successfully deleted.");
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

        // CALCULATIONS BY COURIER ID - ALSO IN CouriersController
        [HttpGet("calculationsByCourier/{courierId}")]
        public ActionResult<CalculationDto> GetByCourierId(int courierId)
        {
            try
            {
                var calculation = _calculationService.GetCalculationsByCourierId(courierId);
                return Ok(calculation);
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

        // CALCULATIONS BY TYPE
        [HttpGet("calculationsByType/{calculationType}")]
        public ActionResult<CalculationDto> GetCalculationsByType(int calculationType)
        {
            try
            {
                var calculation = _calculationService.GetCalculationsByType(calculationType);
                return Ok(calculation);
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

        // CALCULATIONS BY INPUTS
        [HttpGet("calculationsByInputs/{calculationType}/{value}")]
        public ActionResult<CalculationDto> GetCalculationsByInputs(int calculationType, int value)
        {
            try
            {
                var calculation = _calculationService.GetCalculationsByInputs(calculationType, value);
                return Ok(calculation);
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
