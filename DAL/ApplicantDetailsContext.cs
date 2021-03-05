using System;
using System.Collections.Generic;
using System.Text;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class ApplicantDetailsContext:DbContext
    {
        public ApplicantDetailsContext(DbContextOptions<ApplicantDetailsContext> options) : base(options)
        {

        }
        public DbSet<ApplicantDetails> ApplicantDetails { get; set; }
    }
}
