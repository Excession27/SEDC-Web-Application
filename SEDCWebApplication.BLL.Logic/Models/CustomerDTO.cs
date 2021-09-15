using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SEDCWebApplication.BLL.Logic.Models
{
    public class CustomerDTO
    {
        public int? CustomerId { get; set; }

        [Required(ErrorMessage = "Ime je obavezno")]
        public string CustomerName { get; set; }
        public int ContactId { get; set; }
        public string ImagePath { get; set; }


    }
}
