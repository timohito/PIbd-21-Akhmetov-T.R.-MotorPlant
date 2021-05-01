using MotorPlantBusinessLogic.BindingModels;
using MotorPlantBusinessLogic.BusinessLogics;
using MotorPlantBusinessLogic.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace MotorPlantRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly OrderLogic _order;

        private readonly EngineLogic _engine;

        private readonly OrderLogic _main;

        public MainController(OrderLogic order, EngineLogic engine, OrderLogic main)
        {
            _order = order;
            _engine = engine;
            _main = main;
        }

        [HttpGet]
        public List<EngineViewModel> GetEngineList() => _engine.Read(null)?.ToList();

        [HttpGet]
        public EngineViewModel GetEngine(int engineId) => _engine.Read(new EngineBindingModel { Id = engineId })?[0];

        [HttpGet]
        public List<OrderViewModel> GetOrders(int clientId) => _order.Read(new OrderBindingModel { ClientId = clientId });

        [HttpPost]
        public void CreateOrder(CreateOrderBindingModel model) => _main.CreateOrder(model);
    }
}
