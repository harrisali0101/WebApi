using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FF.Data.Repository
{
    public interface IApplicantDetailsRepository
    {
        IEnumerable<ApplicantDetails> GetApplicantDetails();
        bool TryGetApplicant(int id, out ApplicantDetails applicantDetails);
        Task<int> AddApplicantDetailsAsync(ApplicantDetails applicantDetails);
        Task<int> DelApplicantDetailsAsync(int id);
        Task<int> PutApplicantDetails(int id, ApplicantDetails applicantDetails);



    }
}
