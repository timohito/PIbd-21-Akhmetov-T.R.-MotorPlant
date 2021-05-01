using System;
using System.Collections.Generic;
using MotorPlantBusinessLogic.BindingModels;
using MotorPlantBusinessLogic.Interfaces;
using MotorPlantBusinessLogic.ViewModels;
using System.Linq;
using MotorPlantListImplement.Models;
using MotorPlantBusinessLogic.Enums;

namespace MotorPlantListImplement.Implements
{
    public class OrderStorage : IOrderStorage
    {
        private readonly DataListSingleton source;

        public OrderStorage()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<OrderViewModel> GetFullList()
        {
            List<OrderViewModel> result = new List<OrderViewModel>();
            foreach (var component in source.Orders)
            {
                result.Add(CreateModel(component));
            }
            return result;
        }
        public List<OrderViewModel> GetFilteredList(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            List<OrderViewModel> result = new List<OrderViewModel>();
            foreach (var Order in source.Orders)
            {
                if ((!model.DateFrom.HasValue && !model.DateTo.HasValue && Order.DateCreate.Date == model.DateCreate.Date) ||
(model.DateFrom.HasValue && model.DateTo.HasValue && Order.DateCreate.Date >= model.DateFrom.Value.Date && Order.DateCreate.Date <= model.DateTo.Value.Date) ||
(model.ClientId.HasValue && Order.ClientId == model.ClientId) ||
(model.FreeOrders.HasValue && model.FreeOrders.Value && Order.Status == OrderStatus.Принят) ||
(model.ImplementerId.HasValue && Order.ImplementerId == model.ImplementerId && Order.Status == OrderStatus.Выполняется))
                {
                    result.Add(CreateModel(Order));
                }
            }
            return result;
        }
        public OrderViewModel GetElement(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var Order in source.Orders)
            {
                if (Order.Id == model.Id)
                {
                    return CreateModel(Order);
                }
            }
            return null;
        }
        public void Insert(OrderBindingModel model)
        {
            Order tempOrder = new Order { Id = 1 };
            foreach (var Order in source.Orders)
            {
                if (Order.Id >= tempOrder.Id)
                {
                    tempOrder.Id = Order.Id + 1;
                }
            }
            source.Orders.Add(CreateModel(model, tempOrder));
        }
        public void Update(OrderBindingModel model)
        {
            Order tempOrder = null;
            foreach (var Order in source.Orders)
            {
                if (Order.Id == model.Id)
                {
                    tempOrder = Order;
                }
            }
            if (tempOrder == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, tempOrder);
        }
        public void Delete(OrderBindingModel model)
        {
            for (int i = 0; i < source.Orders.Count; ++i)
            {
                if (source.Orders[i].Id == model.Id)
                {
                    source.Orders.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
        private Order CreateModel(OrderBindingModel model, Order order)
        {
            order.EngineId = model.EngineId;
            order.ClientId = (int)model.ClientId;
            order.ImplementerId = model.ImplementerId;
            order.Count = model.Count;
            order.Status = model.Status;
            order.Sum = model.Sum;
            order.DateCreate = model.DateCreate;
            order.DateImplement = model.DateImplement;
            return order;
        }
        private OrderViewModel CreateModel(Order order)
        {
            string EngineName = "";
            for (int i = 0; i < source.Engines.Count; ++i)
            {
                if (source.Engines[i].Id == order.EngineId)
                {
                    EngineName = source.Engines[i].EngineName;
                }
            }
            string clientFIO = "";
            for (int i = 0; i < source.Clients.Count; ++i)
            {
                if (source.Clients[i].Id == order.ClientId)
                {
                    clientFIO = source.Clients[i].ClientFIO;
                }
            }
            string ImplementerFIO = "";
            for (int i = 0; i < source.Implementers.Count; ++i)
            {
                if (source.Implementers[i].Id == order.ImplementerId)
                {
                    clientFIO = source.Implementers[i].ImplementerFIO;
                }
            }
            return new OrderViewModel
            {
                Id = order.Id,
                EngineName = EngineName,
                EngineId = order.EngineId,
                ClientId = order.ClientId,
                ClientFIO = clientFIO,
                ImplementerId = order.ImplementerId,
                ImplementerFIO = ImplementerFIO,
                Count = order.Count,
                Status = order.Status,
                Sum = order.Sum,
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement,
            };
        }
    }
}
