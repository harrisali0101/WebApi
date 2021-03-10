using System;
using System.Collections.Generic;
using System.Text;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class ApplicantDetailsContext:DbContext
    {
        public ApplicantDetailsContext()
        {

        }
       public ApplicantDetailsContext(DbContextOptions<ApplicantDetailsContext> options) : base(options)
        {
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=localhost;port=3306;user=root;password=root;database=applicants");
            }
        }

        

       public DbSet<ApplicantDetails> Applicant { get; set; }
    }
}
