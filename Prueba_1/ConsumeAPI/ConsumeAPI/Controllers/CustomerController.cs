using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using ConsumeAPI.Models;


namespace ConsumeAPI.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            IEnumerable<CustomerViewModel> customer = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
                var responseTask = client.GetAsync("users");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readUser = result.Content.ReadAsAsync<List<CustomerViewModel>>();
                    readUser.Wait();
                    customer = readUser.Result;
                }
                else
                {
                    customer = Enumerable.Empty<CustomerViewModel>();
                    ModelState.AddModelError(string.Empty, "Ocurrio un error");
                }
            }
            return View(customer);
        }
        int idd;
        public ActionResult Details(int id)
        {
            idd = id;
            CustomerViewModel customer = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
                var responseTask = client.GetAsync("users/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<CustomerViewModel>();
                    readTask.Wait();
                    customer = readTask.Result;
                }
            }
            return View(customer);
        }

        public ActionResult Post(int id)
        {
            IEnumerable<PostViewModel> customer = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
                var responseTask = client.GetAsync("users/" + id.ToString() + "/posts");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readUser = result.Content.ReadAsAsync<List<PostViewModel>>();
                    readUser.Wait();
                    customer = readUser.Result;
                }
                else
                {
                    customer = Enumerable.Empty<PostViewModel>();
                    ModelState.AddModelError(string.Empty, "Ocurrio un error");
                }
            }
            return View(customer);
        }
        public ActionResult Comments(int id)
        {
            IEnumerable<comentsViewModel> customer = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
                var responseTask = client.GetAsync("/post/" + id.ToString() + "/comments");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readComments = result.Content.ReadAsAsync<List<comentsViewModel>>();
                    readComments.Wait();
                    customer = readComments.Result;
                }
                else
                {
                    customer = Enumerable.Empty<comentsViewModel>();
                    ModelState.AddModelError(string.Empty, "Ocurrio un error");
                }
            }
            return View(customer);
        }
        public ActionResult Todos(int id)
        {
            IEnumerable<todosViewModel> customer = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
                var responseTask = client.GetAsync("/users/" + id.ToString() + "/todos");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readComments = result.Content.ReadAsAsync<List<todosViewModel>>();
                    readComments.Wait();
                    customer = readComments.Result;
                }
                else
                {
                    customer = Enumerable.Empty<todosViewModel>();
                    ModelState.AddModelError(string.Empty, "Ocurrio un error");
                }
            }
            return View(customer);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(todosViewModel todos)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/users/"+idd.ToString() + "/todos");
                var postTodos = client.PostAsJsonAsync<todosViewModel>("todos",todos);
                postTodos.Wait();

                var postResult = postTodos.Result;
                if (postResult.IsSuccessStatusCode)
                    return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "Server error");
            return View(todos);
        }

    }
}