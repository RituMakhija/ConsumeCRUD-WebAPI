using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPIGrid.Models
{
    public class DataViewModel
    {
        public int SlNo { get; set; }
        public string UserName { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public DetailViewModel Detail { get; set; }
    }
}