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
            if (_context.ApplicantDetails.Count() == 0)
            {
                _context.ApplicantDetails.AddRange(
                    new ApplicantDetails
                    {
                        Name="Harris Ali",
                        FamilyName="Malik",
                        Address="House 171, Street 9 , Dha Phase 2.",
                        CountryOfOrigin="Pakistan",
                        EmailAddress="ali@gmail.com",
                        Age=24,
                        Hired=false
                        
                    },
                    new ApplicantDetails
                    {
                        Name = "Hira Ali",
                        FamilyName = "Malik",
                        Address = "House 171, Street 9 , Dha Phase 2.",
                        CountryOfOrigin = "Pakistan",
                        EmailAddress = "Hira@gmail.com",
                        Age = 24,
                        Hired = false

                    }


                    );
                _context.SaveChanges();
            }
        }

        public IEnumerable<ApplicantDetails> GetApplicantDetails()
        {
            return _context.ApplicantDetails.ToList();
        }

        public bool TryGetApplicant(int id, out ApplicantDetails applicantDetails)
        {
            applicantDetails = _context.ApplicantDetails.Find(id);
            return (applicantDetails != null);
        }

        public async Task<int> AddApplicantDetailsAsync(ApplicantDetails applicantDetails)
        {
            int rowsAffected = 0;
            _context.ApplicantDetails.Add(applicantDetails);
            rowsAffected = await _context.SaveChangesAsync();
            return rowsAffected;
        }

        public async Task<int> DelApplicantDetailsAsync(int id)
        {
            ApplicantDetails ad;
            int rowsAffected = 0;
            ad = _context.ApplicantDetails.Find(id);
            _context.ApplicantDetails.Remove(ad);
            rowsAffected = await _context.SaveChangesAsync();
            return rowsAffected;

        }

    }
}
