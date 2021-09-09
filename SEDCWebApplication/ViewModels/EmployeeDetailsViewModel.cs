using SEDCWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEDCWebApplication.ViewModels
{
    public class EmployeeDetailsViewModel
    {

        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public string PageTitle { get; set; }
        public string ImagePath { get; set; }
        public bool Test { get; set; }
    }
}
