﻿using MotorPlantBusinessLogic.BindingModels;
using MotorPlantBusinessLogic.Enums;
using MotorPlantBusinessLogic.Interfaces;
using MotorPlantBusinessLogic.ViewModels;
using MotorPlantFileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MotorPlantFileImplement.Implements
{
	public class OrderStorage : IOrderStorage
	{
		private readonly FileDataListSingleton source;
		public OrderStorage()
		{
			source = FileDataListSingleton.GetInstance();
		}
		public List<OrderViewModel> GetFullList()
		{
			return source.Orders
			.Select(CreateModel)
			.ToList();
		}
		public List<OrderViewModel> GetFilteredList(OrderBindingModel model)
		{
			if (model == null)
			{
				return null;
			}
			return source.Orders
			.Where(rec => (!model.DateFrom.HasValue && !model.DateTo.HasValue && rec.DateCreate.Date == model.DateCreate.Date) ||
			(model.DateFrom.HasValue && model.DateTo.HasValue && rec.DateCreate.Date >= model.DateFrom.Value.Date && rec.DateCreate.Date <= model.DateTo.Value.Date) ||
			(model.ClientId.HasValue && rec.ClientId == model.ClientId) ||
(model.FreeOrders.HasValue && model.FreeOrders.Value && rec.Status == OrderStatus.Принят) ||
(model.ImplementerId.HasValue && rec.ImplementerId == model.ImplementerId && rec.Status == OrderStatus.Выполняется))
            .Select(CreateModel)
			.ToList();
		}
		public OrderViewModel GetElement(OrderBindingModel model)
		{
			if (model == null)
			{
				return null;
			}
			var order = source.Orders
			.FirstOrDefault(rec => rec.Id == model.Id);
			return order != null ? CreateModel(order) : null;
		}
		public void Insert(OrderBindingModel model)
		{
			if (!model.ClientId.HasValue)
			{
				throw new Exception("Client not specified");
			}
			int maxId = source.Orders.Count > 0 ? source.Orders.Max(rec => rec.Id) : 0;
			var element = new Order { Id = maxId + 1 };
			source.Orders.Add(CreateModel(model, element));
		}
		public void Update(OrderBindingModel model)
		{
			var element = source.Orders.FirstOrDefault(rec => rec.Id == model.Id);
			if (element == null)
			{
				throw new Exception("Элемент не найден");
			}
			if (!model.ClientId.HasValue)
			{
				model.ClientId = element.ClientId;
			}
			CreateModel(model, element);
		}
		public void Delete(OrderBindingModel model)
		{
			Order element = source.Orders.FirstOrDefault(rec => rec.Id == model.Id);
			if (element != null)
			{
				source.Orders.Remove(element);
			}
			else
			{
				throw new Exception("Элемент не найден");
			}
		}
		private Order CreateModel(OrderBindingModel model, Order order)
		{
			order.ClientId = (int)model.ClientId;
			order.EngineId = model.EngineId;
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
			return new OrderViewModel
			{
				Id = order.Id,
				EngineId = order.EngineId,
				ClientId = order.ClientId,
				ClientFIO = source.Clients.FirstOrDefault(client => client.Id == order.ClientId)?.ClientFIO,
				EngineName = source.Engines.FirstOrDefault(p => p.Id == order.EngineId)?.EngineName,
				ImplementerId = order.ImplementerId,
				ImplementerFIO = source.Implementers.FirstOrDefault(e => e.Id == order.ImplementerId)?.ImplementerFIO,
				Count = order.Count,
				Status = order.Status,
				Sum = order.Sum,
				DateCreate = order.DateCreate,
				DateImplement = order.DateImplement,
			};
		}
	}
}
