using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.Repository
{
    public class ApplicantDetailsRepository
    {
        private readonly ApplicantDetailsContext _context;
        public ApplicantDetailsRepository(ApplicantDetailsContext context)
        {
            this._context = context;
            
        }
        /// <summary>
        /// This method is called to get all the applicants data.
        /// </summary>
        /// <returns>List of Applicant data.</returns>
        public IEnumerable<ApplicantDetails> GetApplicantDetails()
        {
            return _context.Applicant.ToList();
        }

        /// <summary>
        /// This method is called to get the data of a specific record.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="applicantDetails"></param>
        /// <returns>Return applicant data according to the id received.</returns>
        public bool TryGetApplicant(int id, out ApplicantDetails applicantDetails)
        {
            applicantDetails = _context.Applicant.Find(id);
            return (applicantDetails != null);
        }
        /// <summary>
        /// This method is called whenever there is a post request to add data.
        /// </summary>
        /// <param name="applicantDetails"></param>
        public async Task<int> AddApplicantDetailsAsync(ApplicantDetails applicantDetails)
        {
            int rowsAffected = 0;
            _context.Applicant.Add(applicantDetails);
            rowsAffected = await _context.SaveChangesAsync();
            return rowsAffected;
        }
        /// <summary>
        /// This method is called whenever there is a delete request.
        /// </summary>
        /// <param name="id"></param>
        public async Task<int> DelApplicantDetailsAsync(int id)
        {
            ApplicantDetails ad;
            int rowsAffected = 0;
            ad = _context.Applicant.Find(id);
            _context.Applicant.Remove(ad);
            rowsAffected = await _context.SaveChangesAsync();
            return rowsAffected;

        }
        /// <summary>
        /// This method will check whether the sent id exists in the database or not. If it does it will
        /// update the record in database according to the given id sent along with the updated data to the api.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="applicantDetails"></param>
        public async Task<int> PutApplicantDetails(int id, ApplicantDetails applicantDetails)
        {
            var existAD = _context.Applicant.Find(id);
            int rowsAffected = 0;
            if (existAD.ID != 0)
            {
                existAD.Name = applicantDetails.Name;
                existAD.FamilyName = applicantDetails.FamilyName;
                existAD.Address = applicantDetails.Address;
                existAD.CountryOfOrigin = applicantDetails.CountryOfOrigin;
                existAD.EmailAddress = applicantDetails.EmailAddress;
                existAD.Age = applicantDetails.Age;
                existAD.Hired = applicantDetails.Hired;
                rowsAffected = await _context.SaveChangesAsync();
            }
            return rowsAffected;
        }



    }
}
