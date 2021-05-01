using MotorPlantBusinessLogic.BindingModels;
using MotorPlantBusinessLogic.Enums;
using MotorPlantBusinessLogic.ViewModels;
using MotorPlantClientApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MotorPlantClientApp.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            if (Program.Client == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIClient.GetRequest<List<OrderViewModel>>($"api/main/getorders?clientId={Program.Client.Id}"));
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            if (Program.Client == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(Program.Client);
        }

        [HttpPost]
        public void Privacy(string login, string password, string fio)
        {
            if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(fio))
            {
                //прописать запрос
                APIClient.PostRequest("api/client/updatedata", new ClientBindingModel
                {
                    Id = Program.Client.Id,
                    ClientFIO = fio,
                    Email = login,
                    Password = password
                });
                Program.Client.ClientFIO = fio;
                Program.Client.Email = login;
                Program.Client.Password = password;
                Response.Redirect("Index");
                return;
            }
            throw new Exception("Enter login, password and full name");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }

        [HttpGet]
        public IActionResult Enter()
        {
            return View();
        }

        [HttpPost]
        public void Enter(string login, string password)
        {
            if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password))
            {
                Program.Client = APIClient.GetRequest<ClientViewModel>($"api/client/login?login={login}&password={password}");
                if (Program.Client == null)
                {
                    throw new Exception("Invalid username / password");
                }
                Response.Redirect("Index");
                return;
            }
            throw new Exception("Enter login, password");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public void Register(string login, string password, string fio)
        {
            if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(fio))
            {
                APIClient.PostRequest("api/client/register", new ClientBindingModel
                {
                    ClientFIO = fio,
                    Email = login,
                    Password = password
                });
                Response.Redirect("Enter");
                return;
            }
            throw new Exception("Enter login, password and full name");
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Engines = APIClient.GetRequest<List<EngineViewModel>>("api/main/getenginelist");
            return View();
        }

        [HttpPost]
        public void Create(int engine, int count, decimal sum)
        {
            if (count == 0 || sum == 0)
            {
                return;
            }
            var str = Program.Client.Id;
            //прописать запрос
            APIClient.PostRequest("api/main/createorder", new CreateOrderBindingModel
            {
                EngineId = engine,
                ClientId = Program.Client.Id,
                Sum = sum,
                Count = count
            });
            Response.Redirect("Index");
        }

        [HttpPost]
        public decimal Calc(decimal count, int engine)
        {
            EngineViewModel prod = APIClient.GetRequest<EngineViewModel>($"api/main/getengine?engineId={engine}");
            return count * prod.Price;
        }
    }
}
