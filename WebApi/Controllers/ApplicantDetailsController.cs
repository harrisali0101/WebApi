using DAL.Models;
using DAL.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RESTCountries.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RESTCountries.Models;
using Microsoft.Extensions.Logging;
using FF.Data.Repository;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicantDetailsController : ControllerBase
    {
        IApplicantDetailsRepository _repository;
        private readonly ILogger<ApplicantDetailsController> logger;

        
        public ApplicantDetailsController(IApplicantDetailsRepository repository, ILogger<ApplicantDetailsController> logger)
        {
            this._repository = repository;
            this.logger = logger;
        }
        
        /// <summary>
        /// When a Http call is made to this api to get all records this method is called.
        /// </summary>
        /// <returns>A list of all applicant data.</returns>
        [HttpGet]
        public IEnumerable<ApplicantDetails> GetAllApplicants()
        {
            logger.LogInformation("Getting all Applicants");
            
            return _repository.GetApplicantDetails();
 
        }


        /// <summary>
        /// When a get request with id is sent to this api this method is called.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Data specific to the sent id is returned when successfull along with 200 status code,
        /// else it return 404 status code not found.</returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            if (!_repository.TryGetApplicant(id, out var applicantDetails))
            {
                logger.LogInformation($"There is no Applicant with ID: {id}");
                return NotFound();
            }
            logger.LogInformation($"Returning applicant with ID:{id}");
            return Ok(applicantDetails);
        }


        /// <summary>
        /// When a post call is made to this api to add data in database this method is called.
        /// </summary>
        /// <param name="applicantDetails"></param>
        /// <returns>The new Applicant data is returned along with 201 status code when successfull,
        /// else it returns 400 bad request status code along with properties which do not 
        /// fullfill the required requirements </returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] ApplicantDetails applicantDetails)
        {
            Boolean valid = false;
            var country = await RESTCountriesAPI.GetCountryByFullNameAsync(applicantDetails.CountryOfOrigin);
            logger.LogInformation(country.Name);
            if (country.Name.ToUpper() == applicantDetails.CountryOfOrigin.ToUpper())
            {
                valid = true;
            }

            if (valid)
            {
                logger.LogInformation("New applicant is added.");
                await _repository.AddApplicantDetailsAsync(applicantDetails);
                logger.LogInformation(nameof(GetById), new { id = applicantDetails.ID }, applicantDetails);
                return CreatedAtAction(nameof(GetById), new { id = applicantDetails.ID }, applicantDetails);
            }
            else
            {
                
                logger.LogInformation($"{applicantDetails.CountryOfOrigin} is not a valid country");
                return BadRequest();
            }
        }


        /// <summary>
        /// When a delete call is made to this api this method is called which receives the id of the data to be
        /// deleted.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>It returns a 204 status code when successful.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            logger.LogInformation($"Applicant with ID: {id} deleted.");
            await _repository.DelApplicantDetailsAsync(id);
            return Ok();
        }


        /// <summary>
        /// When a call to update a specific record is made this method is called.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="applicantDetails"></param>
        /// <returns>It returns a 204 status code when successfull, else it returns a 400 bad request 
        /// status code along with the properties which do not fullfill the required requirements</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditAsync(int id, ApplicantDetails applicantDetails)
        {
            logger.LogInformation($"Applicant with ID: {id} Updated.");
            await _repository.PutApplicantDetails(id, applicantDetails);
            return Ok();
        }
    }
}
