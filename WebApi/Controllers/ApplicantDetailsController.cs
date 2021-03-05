using DAL.Models;
using DAL.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        [HttpGet]
        public IEnumerable<ApplicantDetails> Get()
        {
            return _repository.GetApplicantDetails();
        }
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
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody] ApplicantDetails applicantDetails)
        {
            await _repository.AddApplicantDetailsAsync(applicantDetails);
            return CreatedAtAction(nameof(GetById), new { id = applicantDetails.ID }, applicantDetails);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _repository.DelApplicantDetailsAsync(id);
            return NoContent();
        }
        




    }
}
