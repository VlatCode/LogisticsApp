using LogisticsApp.Domain.Models;
using LogisticsApp.DTOs;
using LogisticsApp.Services.Implementations;
using LogisticsApp.Services.Interfaces;
using LogisticsApp.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace LogisticsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculationsController : ControllerBase
    {
        private ICalculationService _calculationService;
        private IPackageService _packageService;
        

        public CalculationsController(ICalculationService calculationService, IPackageService packageService)
        {
            _calculationService = calculationService;
            _packageService= packageService;
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

        [HttpDelete("deleteCalculation/{id}")]
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

        // CALCULATIONS BY COURIER ID
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

        // CALCULATIONS BY TYPE (weight/dimensions)
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

        // COST BY INPUTS
        [HttpGet("costByInputs/{weight}/{height}/{width}/{depth}")]
        public ActionResult<CalculationDto> GetCostByInputs(int weight, int height, int width, int depth)
        {
            try
            {
                var calculation = _calculationService.GetCostByInputs(weight, height, width, depth);
                
                // This code creates a new package based on the user inputs
                var packages = _packageService.GetAllPackages();
                AddPackageDto addPackageDto = new AddPackageDto();
                addPackageDto.Weight = weight;
                addPackageDto.Dimensions = height * width * depth;
                addPackageDto.CalculationId = calculation.Id;
                _packageService.AddPackage(addPackageDto);

                // Returns final price
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

        //// Initial logic - overriden by method above
        //// CALCULATIONS BY INPUTS
        //[HttpGet("calculationsByInputs/{calculationType}/{value}")]
        //public ActionResult<CalculationDto> GetCalculationsByInputs(int calculationType, int value)
        //{
        //    try
        //    {
        //        var calculation = _calculationService.GetCalculationsByInputs(calculationType, value);
        //        return Ok(calculation);
        //    }
        //    catch (NotFoundException e)
        //    {
        //        return NotFound(e.Message);
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred! Contact the admin!");
        //    }
        //}
    }
}
