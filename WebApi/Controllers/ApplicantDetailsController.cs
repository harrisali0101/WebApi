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

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicantDetailsController : ControllerBase
    {
        private readonly ApplicantDetailsRepository _repository;

        public ApplicantDetailsController(ApplicantDetailsRepository repository)
        {
            _repository = repository;
        }
        /// <summary>
        /// When a Http call is made to this api to get all records this method is called.
        /// </summary>
        /// <returns>A list of all applicant data.</returns>
        [HttpGet]
        public IEnumerable<ApplicantDetails> Get()
        {
            return _repository.GetApplicantDetails();
        }
        /// <summary>
        /// When a get request with id is sent to this api this method is called.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Data specific to the sent id is returned when successfull along with 200 status code,
        /// else it return 404 status code not found.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApplicantDetails), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            if (!_repository.TryGetApplicant(id, out var applicantDetails))
            {
                return NotFound();
            }
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
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody] ApplicantDetails applicantDetails)
        {
            Boolean valid = false;
            List<Country> countries = await RESTCountriesAPI.GetAllCountriesAsync();
            for (int i = 0; i < countries.Count; i++)
            {
                if (countries[i].Name.ToUpper()==applicantDetails.CountryOfOrigin.ToUpper() )
                {
                    valid = true;
                }
            }

            if (valid)
            {

                await _repository.AddApplicantDetailsAsync(applicantDetails);
                return CreatedAtAction(nameof(GetById), new { id = applicantDetails.ID }, applicantDetails);
            }
            else
            {
                return BadRequest("Please Enter Valid Country");
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
            await _repository.DelApplicantDetailsAsync(id);
            return NoContent();
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
            await _repository.PutApplicantDetails(id, applicantDetails);
            return NoContent();
        }



        
    }
}
