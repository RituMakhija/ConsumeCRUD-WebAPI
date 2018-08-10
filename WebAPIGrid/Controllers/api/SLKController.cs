using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIGrid.DataAccess;
using WebAPIGrid.Models;

namespace WebAPIGrid.Controllers.api
{
    public class SLKController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetAllEmployees()
        {
            IList<DataViewModel> students = null;

            using (var ctx = new SLKDatabaseEntities())
            {
                students = ctx.registrations
                            .Select(s => new DataViewModel()
                            {
                                SlNo = s.SlNo,
                                UserName = s.UserName,
                                EmailId = s.EmailId,
                                Password = s.Password,
                                ConfirmPassword = s.ConfirmPassword
                            }).ToList<DataViewModel>();
            }

            if (students.Count == 0)
            {
                return NotFound();
            }

            return Ok(students);
        }

        //Create New
        [HttpPost]
        public IHttpActionResult PostNewEmployee(DataViewModel s)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new SLKDatabaseEntities())
            {
                ctx.registrations.Add(new registration()
                {
                    UserName = s.UserName,
                    EmailId = s.EmailId,
                    Password = s.Password,
                    ConfirmPassword = s.ConfirmPassword
                });

                ctx.SaveChanges();
            }

            return Ok();
        }

        //update
        //[HttpPut]
        public IHttpActionResult PutAnEmployee(DataViewModel s)
        {           
            if (!ModelState.IsValid)
                return BadRequest("Not a valid data");

            using (var ctx = new SLKDatabaseEntities())
            {
                var existingStudent = ctx.registrations.Where(m => m.SlNo == s.SlNo).FirstOrDefault<registration>();

                if (existingStudent != null)
                {
                    existingStudent.UserName = s.UserName;
                    existingStudent.EmailId = s.EmailId;
                    existingStudent.Password = s.Password;
                    existingStudent.ConfirmPassword = s.ConfirmPassword;
                    ctx.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteEmployee(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid student id");

            using (var ctx = new SLKDatabaseEntities())
            {
                var student = ctx.registrations
                    .Where(s => s.SlNo == id)
                    .FirstOrDefault();

                ctx.Entry(student).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }

            return Ok();
        }

    }
}
