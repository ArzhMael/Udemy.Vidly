using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class BirthDateCustomValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Customer) validationContext.ObjectInstance;
            
            if (customer.MembershipTypeId == MembershipType.Unknown || customer.MembershipTypeId == MembershipType.PayAsYouGo)            
                return ValidationResult.Success;

            if (customer.BirthDay == null)
                return new ValidationResult("Birth Day is required.");

            var age = DateTime.Today.Year - customer.BirthDay.Value.Year;
            return (age >= 18) ? ValidationResult.Success : new ValidationResult("Must be 18 year or older for membership.");
        }
    }
}