using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPIGrid.Models
{
    public class DetailViewModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string DateOfBirth { get; set; }
        public string EmailID { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Department { get; set; }
        public string TenthBoard { get; set; }
        public int TenthMarks { get; set; }
        public string TwelfthBoard { get; set; }
        public int TwelfthMarks { get; set; }

        public DataViewModel Data { get; set; }
    }
}