using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebApi.Tests
{
    public class DummyDataInitilizer
    {
        public DummyDataInitilizer()
        {

        }
        public void Seed(ApplicantDetailsContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Applicant.AddRange(new ApplicantDetails
            {
                ID = 0,
                Name = "Harris",
                FamilyName = "Malik",
                Address = "abc def ghi assd",
                Age = 24,
                CountryOfOrigin = "Pakistan",
                EmailAddress = "abc@gmail.com",
                Hired = true
            },
                new ApplicantDetails
                {
                    ID = 0,
                    Name = "hira",
                    FamilyName = "Malik",
                    Address = "abc def ghi assd",
                    Age = 24,
                    CountryOfOrigin = "Pakistan",
                    EmailAddress = "abc@gmail.com",
                    Hired = true
                }
                );
            context.SaveChanges();
        }
    }
}
