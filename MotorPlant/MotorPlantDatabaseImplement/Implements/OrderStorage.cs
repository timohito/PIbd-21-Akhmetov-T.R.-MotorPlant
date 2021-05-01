using MotorPlantBusinessLogic.BindingModels;
using MotorPlantBusinessLogic.Interfaces;
using MotorPlantBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using MotorPlantDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace MotorPlantDatabaseImplement.Implements
{
    public class OrderStorage : IOrderStorage
    {
        public void Delete(OrderBindingModel model)
        {
            using (var context = new MotorPlantDatabase())
            {
                Order element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Orders.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Element not found");
                }
            }
        }

        public OrderViewModel GetElement(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new MotorPlantDatabase())
            {
                var order = context.Orders.Include(rec => rec.Engine)
                .Include(rec => rec.Client)
                .FirstOrDefault(rec => rec.Id == model.Id);
                return order != null ?
                CreateModel(order) : null;
            }
        }

        public List<OrderViewModel> GetFilteredList(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new MotorPlantDatabase())
            {
                return context.Orders.Include(rec => rec.Engine)
                    .Include(rec => rec.Client)
                    .Where(rec => (!model.DateFrom.HasValue &&
                    !model.DateTo.HasValue && rec.DateCreate.Date == model.DateCreate.Date) ||
                    (model.DateFrom.HasValue && model.DateTo.HasValue && rec.DateCreate.Date >=
                    model.DateFrom.Value.Date && rec.DateCreate.Date <= model.DateTo.Value.Date) ||
                    (model.ClientId.HasValue && rec.ClientId == model.ClientId))
                    .Select(CreateModel).ToList();
            }
        }

        public List<OrderViewModel> GetFullList()
        {
            using (var context = new MotorPlantDatabase())
            {
                return context.Orders.Include(rec => rec.Engine)
                    .Include(rec => rec.Client)
                    .Select(CreateModel).ToList();
            }
        }

        public void Insert(OrderBindingModel model)
        {
            if (!model.ClientId.HasValue)
            {
                throw new Exception("Client not specified");
            }
            using (var context = new MotorPlantDatabase())
            {
                context.Orders.Add(CreateModel(model, new Order()));
                context.SaveChanges();
            }
        }

        public void Update(OrderBindingModel model)
        {
            using (var context = new MotorPlantDatabase())
            {
                var element = context.Orders.Include(rec => rec.Client)
                    .Include(rec => rec.Engine)
                    .FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Element not found");
                }
                if (!model.ClientId.HasValue)
                {
                    model.ClientId = element.ClientId;
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }

        private OrderViewModel CreateModel(Order order)
        {
            return new OrderViewModel
            {
                Id = order.Id,
                EngineId = order.EngineId,
                ClientId = order.ClientId,
                ClientFIO = order.Client.ClientFIO,
                EngineName = order.Engine.EngineName,
                Count = order.Count,
                Sum = order.Sum,
                Status = order.Status,
                DateCreate = order.DateCreate,
                DateImplement = order?.DateImplement
            };
        }

        private Order CreateModel(OrderBindingModel model, Order order)
        {
            order.EngineId = model.EngineId;
            order.ClientId = Convert.ToInt32(model.ClientId);
            order.Count = model.Count;
            order.Status = model.Status;
            order.Sum = model.Sum;
            order.DateCreate = model.DateCreate;
            order.DateImplement = model.DateImplement;
            return order;
        }
    }

}