using LogisticsApp.DataAccess;
using LogisticsApp.DataAccess.Implementations;
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
    public class CouriersController : ControllerBase
    {
        private ICourierService _courierService;
        private IValidationService _validationService;
        private ICalculationService _calculationService;

        public CouriersController(ICourierService courierService, IValidationService validationService, ICalculationService calculationService)
        {
            _courierService = courierService;
            _validationService = validationService;
            _calculationService = calculationService;
        }

        [HttpGet]
        public ActionResult<List<CourierDto>> Get()
        {
            try
            {
                return Ok(_courierService.GetAllCouriers());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred! Contact the admin!");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<CourierDto> GetById(int id)
        {
            try
            {
                var courierDto = _courierService.GetById(id);
                return Ok(courierDto);
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

        [HttpPost("addCourier")]
        public IActionResult AddCourier([FromBody] AddCourierDto addCourierDto)
        {
            try
            {
                _courierService.AddCourier(addCourierDto);
                return StatusCode(StatusCodes.Status201Created, "Courier added to the system.");
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

        [HttpDelete("deleteCourier/{id}")]
        public ActionResult<CourierDto> Delete(int id)
        {
            try
            {
                _courierService.DeleteCourier(id);
                return Ok($"Courier with id {id} successfully deleted.");
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

        // VALIDATIONS BY COURIER ID
        [HttpGet("validationsByCourier/{courierId}")]
        public ActionResult<ValidationDto> GetValidationsByCourierId(int courierId)
        {
            try
            {
                var validation = _validationService.GetValidationsByCourierId(courierId);
                //var calculation = _calculationService.GetCalculationsByCourierId(courierId);
                return Ok(validation);
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

        // CALCULATIONS BY COURIER ID - ALSO IN CalculationsController
        [HttpGet("calculationsByCourier/{courierId}")]
        public ActionResult<CalculationDto> GetCalculationsByCourierId(int courierId)
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
    }
}
