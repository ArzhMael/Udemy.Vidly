using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter your name")]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }

        [Display(Name = "Date of birth")]
        [BirthDateCustomValidation]
        [DataType(DataType.Date)]
        public DateTime? BirthDay { get; set; }

        // Propriété de navigation (Foreign Key) vers une autre table.
        public MembershipType MembershipType { get; set; }

        [Display(Name = "Membership Type")]
        [Required]
        public byte MembershipTypeId { get; set; }

    }
}