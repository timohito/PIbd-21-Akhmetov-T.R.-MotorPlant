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
        public List<OrderViewModel> GetFullList()
        {
            using (var context = new MotorPlantDatabase())
            {
                return context.Orders
                .Select(rec => new OrderViewModel
                {
                    Id = rec.Id,
                    EngineId = rec.EngineId,
                    EngineName = context.Engines.Include(en => en.Orders).FirstOrDefault(en => en.Id == rec.EngineId).EngineName,
                    Count = rec.Count,
                    Sum = rec.Sum,
                    Status = rec.Status,
                    DateCreate = rec.DateCreate,
                    DateImplement = rec.DateImplement,
                })
                .ToList();
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
                return context.Orders
                .Where(rec => rec.EngineId == model.EngineId || (rec.DateCreate >= model.DateFrom && rec.DateCreate <= model.DateTo))
                .Include(rec => rec.Engine)
                .Select(rec => new OrderViewModel
                {
                    Id = rec.Id,
                    EngineId = rec.EngineId,
                    EngineName = context.Engines.Include(en => en.Orders).FirstOrDefault(en => en.Id == rec.EngineId).EngineName,
                    Count = rec.Count,
                    Sum = rec.Sum,
                    Status = rec.Status,
                    DateCreate = rec.DateCreate,
                    DateImplement = rec.DateImplement,
                })
                .ToList();
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
                var order = context.Orders
                .FirstOrDefault(rec => rec.Id == model.Id);
                return order != null ?
                new OrderViewModel
                {
                    Id = order.Id,
                    EngineId = order.EngineId,
                    EngineName = context.Engines.Include(en => en.Orders).FirstOrDefault(rec => rec.Id == order.EngineId)?.EngineName,
                    Count = order.Count,
                    Sum = order.Sum,
                    Status = order.Status,
                    DateCreate = order.DateCreate,
                    DateImplement = order.DateImplement,
                } :
                null;
            }
        }
        public void Insert(OrderBindingModel model)
        {
            using (var context = new MotorPlantDatabase())
            {
                Order order = new Order
                {
                    EngineId = model.EngineId,
                    Count = model.Count,
                    Sum = model.Sum,
                    Status = model.Status,
                    DateCreate = model.DateCreate,
                    DateImplement = model.DateImplement,
                };
                context.Orders.Add(order);
                context.SaveChanges();
                CreateModel(model, order);
                context.SaveChanges();
            }
        }
        public void Update(OrderBindingModel model)
        {
            using (var context = new MotorPlantDatabase())
            {
                var element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                element.EngineId = model.EngineId;
                element.Count = model.Count;
                element.Sum = model.Sum;
                element.Status = model.Status;
                element.DateCreate = model.DateCreate;
                element.DateImplement = model.DateImplement;
                CreateModel(model, element);
                context.SaveChanges();
            }
        }

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
                    throw new Exception("Элемент не найден");
                }
            }
        }

        private Order CreateModel(OrderBindingModel model, Order order)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new MotorPlantDatabase())
            {
                Engine element = context.Engines.FirstOrDefault(rec => rec.Id == model.EngineId);
                if (element != null)
                {
                    if(element.Orders == null)
                    {
                        element.Orders = new List<Order>();
                    }
                    element.Orders.Add(order);
                    context.Engines.Update(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
            return order;
        }
    }
}