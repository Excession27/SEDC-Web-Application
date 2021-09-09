using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SEDCWebApplication.BLL.Logic.Models
{
    public class CustomerDTO
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Ime je obavezno")]
        public string Name { get; set; }
        public int ContactId { get; set; }
        public string Email { get; set; }
        public string ImagePath { get; set; }


    }
}
