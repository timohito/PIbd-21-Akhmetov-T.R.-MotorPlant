using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using MotorPlantBusinessLogic.BindingModels;
using MotorPlantBusinessLogic.Enums;
using MotorPlantBusinessLogic.Interfaces;
using MotorPlantBusinessLogic.ViewModels;
using MotorPlantDatabaseImplement.Models;

namespace MotorPlantDatabaseImplement.Implements
{
    public class OrderStorage : IOrderStorage
    {
        public List<OrderViewModel> GetFullList()
        {
            using (var context = new MotorPlantDatabase())
            {
                return context.Orders.Include(rec => rec.Engine).Include(rec => rec.Client).Include(rec => rec.Implementer).Select(rec => new OrderViewModel
                {
                    Id = rec.Id,
                    EngineId = rec.EngineId,
                    ClientId = rec.ClientId,
                    ImplementerId = rec.ImplementerId,
                    EngineName = rec.Engine.EngineName,
                    ClientFIO = rec.Client.ClientFIO,
                    ImplementerFIO = rec.ImplementerId.HasValue ? rec.Implementer.ImplementerFIO : string.Empty,
                    Count = rec.Count,
                    Sum = rec.Sum,
                    Status = rec.Status,
                    DateCreate = rec.DateCreate,
                    DateImplement = rec.DateImplement,
                }).ToList();
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
                return context.Orders.Include(rec => rec.Engine).Include(rec => rec.Client).Include(rec => rec.Implementer).Where(rec => (!model.DateFrom.HasValue && !model.DateTo.HasValue && rec.DateCreate.Date == model.DateCreate.Date) || (model.DateFrom.HasValue && model.DateTo.HasValue && rec.DateCreate.Date >= model.DateFrom.Value.Date && rec.DateCreate.Date <= model.DateTo.Value.Date) || (model.ClientId.HasValue && rec.ClientId == model.ClientId) || (model.FreeOrders.HasValue && model.FreeOrders.Value && rec.Status == OrderStatus.Принят) || (model.ImplementerId.HasValue && rec.ImplementerId == model.ImplementerId && rec.Status == OrderStatus.Выполняется)).Select(rec => new OrderViewModel
                {
                    Id = rec.Id,
                    EngineId = rec.EngineId,
                    ClientId = rec.ClientId,
                    ImplementerId = rec.ImplementerId,
                    EngineName = rec.Engine.EngineName,
                    ClientFIO = rec.Client.ClientFIO,
                    ImplementerFIO = rec.ImplementerId.HasValue ? rec.Implementer.ImplementerFIO : string.Empty,
                    Count = rec.Count,
                    Sum = rec.Sum,
                    Status = rec.Status,
                    DateCreate = rec.DateCreate,
                    DateImplement = rec.DateImplement,
                }).ToList();
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
                var order = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                return order != null ? new OrderViewModel
                {
                    Id = order.Id,
                    EngineId = order.EngineId,
                    ClientId = order.ClientId,
                    ImplementerId = order.ImplementerId,
                    EngineName = context.Engines.Include(pr => pr.Orders).FirstOrDefault(rec => rec.Id == order.EngineId)?.EngineName,
                    ClientFIO = context.Clients.Include(pr => pr.Order).FirstOrDefault(rec => rec.Id == order.ClientId)?.ClientFIO,
                    ImplementerFIO = order.Implementer?.ImplementerFIO,
                    Count = order.Count,
                    Sum = order.Sum,
                    Status = order.Status,
                    DateCreate = order.DateCreate,
                    DateImplement = order.DateImplement,
                } : null;
            }
        }

        public void Insert(OrderBindingModel model)
        {
            using (var context = new MotorPlantDatabase())
            {
                if (model.ClientId.HasValue == false)
                {
                    throw new Exception("Клиент не указан");
                }
                Order order = new Order
                {
                    EngineId = model.EngineId,
                    ClientId = (int)model.ClientId,
                    ImplementerId = model.ImplementerId,
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
                element.ClientId = (int)model.ClientId;
                element.ImplementerId = model.ImplementerId;
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
                Engine Engine = context.Engines.FirstOrDefault(rec => rec.Id == model.EngineId);
                if (Engine != null)
                {
                    if (Engine.Orders == null)
                    {
                        Engine.Orders = new List<Order>();
                    }
                    Engine.Orders.Add(order);
                    context.Engines.Update(Engine);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Изделие не найдено");
                }
                Client client = context.Clients.FirstOrDefault(rec => rec.Id == model.ClientId);
                if (client != null)
                {
                    if (client.Order == null)
                    {
                        client.Order = new List<Order>();
                    }
                    client.Order.Add(order);
                    context.Clients.Update(client);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Клиент не найден");
                }
                Implementer Implementer = context.Implementers.FirstOrDefault(rec => rec.Id == model.ImplementerId);
                if (Implementer != null)
                {
                    if (Implementer.Order == null)
                    {
                        Implementer.Order = new List<Order>();
                    }
                    Implementer.Order.Add(order);
                    context.Implementers.Update(Implementer);
                    context.SaveChanges();
                }
            }
            return order;
        }
    }
}