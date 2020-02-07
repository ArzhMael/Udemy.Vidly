using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ModelsDTO
{
    public class CustomerDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }

        //[BirthDateCustomValidation]
        [DataType(DataType.Date)]
        public DateTime? BirthDay { get; set; }

        public MembershipTypeDTO MembershipType { get; set; }

        public byte MembershipTypeId { get; set; }

    }
}