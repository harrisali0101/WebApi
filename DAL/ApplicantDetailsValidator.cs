using DAL.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FF.Application.Models
{
    public class ApplicantDetailsValidator : AbstractValidator<ApplicantDetails>
    {
        public ApplicantDetailsValidator()
        {
            RuleFor(x => x.ID).NotNull();
            RuleFor(x => x.Name).MinimumLength(5);
            RuleFor(x => x.FamilyName).MinimumLength(5);
            RuleFor(x => x.CountryOfOrigin).NotNull();
            RuleFor(x => x.Address).MinimumLength(10);
            RuleFor(x => x.EmailAddress).EmailAddress();
            RuleFor(x => x.Age).GreaterThanOrEqualTo(20);
            RuleFor(x => x.Age).LessThanOrEqualTo(60);
            RuleFor(x => x.Hired).NotNull();
        }
    }
}
