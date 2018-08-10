using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebAPIGrid.Models;

namespace WebAPIGrid.Controllers
{
    public class SLKController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<DataViewModel> students = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:61554/api/SLK/");
                //HTTP GET
                var responseTask = client.GetAsync("SLK");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<DataViewModel>>();
                    readTask.Wait();

                    students = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    students = Enumerable.Empty<DataViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(students);
        }

        public ActionResult create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult create(DataViewModel student)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:61554/api/SLK/");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<DataViewModel>("SLK", student);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(student);
        }

        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:61554/api/SLK/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("Delete?" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int Id)
        {
            DataViewModel student = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:61554/api/");
                //HTTP GET
                var responseTask = client.GetAsync("SLK?id=" + Id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<DataViewModel>();
                    readTask.Wait();

                    student = readTask.Result;
                }
            }
            return View(student);
        }

        [HttpPost]
        public ActionResult Edit(DataViewModel student)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:61554/api/SLK/");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<DataViewModel>("SLK", student);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(student);
        }
    }
}