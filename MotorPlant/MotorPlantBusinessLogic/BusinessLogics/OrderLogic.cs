using MotorPlantBusinessLogic.BindingModels;
using MotorPlantBusinessLogic.Enums;
using MotorPlantBusinessLogic.Interfaces;
using MotorPlantBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;

namespace MotorPlantBusinessLogic.BusinessLogics
{
	public class OrderLogic
	{
		private readonly IOrderStorage _orderStorage;
		private readonly IEngineStorage _engineStorage;
		private readonly IStoreStorage _storeStorage;

		public OrderLogic(IOrderStorage orderStorage, IEngineStorage engineStorage, IStoreStorage warehouseStorage)
		{
			_orderStorage = orderStorage;
			_engineStorage = engineStorage;
			_storeStorage = warehouseStorage;
		}
		public List<OrderViewModel> Read(OrderBindingModel model)
		{
			if (model == null)
			{
				return _orderStorage.GetFullList();
			}
			if (model.Id.HasValue)
			{
				return new List<OrderViewModel> { _orderStorage.GetElement(model) };
			}
			return _orderStorage.GetFilteredList(model);
		}
		public void CreateOrder(CreateOrderBindingModel model)
		{
			_orderStorage.Insert(new OrderBindingModel
			{
				EngineId = model.EngineId,
				Count = model.Count,
				Sum = model.Sum,
				DateCreate = DateTime.Now,
				Status = OrderStatus.Принят
			});
		}
		public void TakeOrderInWork(ChangeStatusBindingModel model)
		{
			var order = _orderStorage.GetElement(new OrderBindingModel { Id = model.OrderId });
			if (order == null)
			{
				throw new Exception("Не найден заказ");
			}
			if (order.Status != OrderStatus.Принят)
			{
				throw new Exception("Заказ не в статусе \"Принят\"");
			}
			if (!_storeStorage.CheckEnginesByComponents(order.EngineId, order.Count))
			{
				throw new Exception("Недостаточно компонентов на складах");
			}
			_storeStorage.Extract(order.EngineId, order.Count);
			_orderStorage.Update(new OrderBindingModel
			{
				Id = order.Id,
				EngineId = order.EngineId,
				Count = order.Count,
				Sum = order.Sum,
				DateCreate = order.DateCreate,
				DateImplement = DateTime.Now,
				Status = OrderStatus.Выполняется
			});
		}
		public void FinishOrder(ChangeStatusBindingModel model)
		{
			var order = _orderStorage.GetElement(new OrderBindingModel { Id = model.OrderId });
			if (order == null)
			{
				throw new Exception("Не найден заказ");
			}
			if (order.Status != OrderStatus.Выполняется)
			{
				throw new Exception("Заказ не в статусе \"Выполняется\"");
			}
			_orderStorage.Update(new OrderBindingModel
			{
				Id = order.Id,
				EngineId = order.EngineId,
				Count = order.Count,
				Sum = order.Sum,
				DateCreate = order.DateCreate,
				DateImplement = order.DateImplement,
				Status = OrderStatus.Готов
			});
		}

		public void PayOrder(ChangeStatusBindingModel model)
		{
			var order = _orderStorage.GetElement(new OrderBindingModel { Id = model.OrderId });
			if (order == null)
			{
				throw new Exception("Не найден заказ");
			}
			if (order.Status != OrderStatus.Готов)
			{
				throw new Exception("Заказ не в статусе \"Готов\"");
			}
			_orderStorage.Update(new OrderBindingModel
			{
				Id = order.Id,
				EngineId = order.EngineId,
				Count = order.Count,
				Sum = order.Sum,
				DateCreate = order.DateCreate,
				DateImplement = order.DateImplement,
				Status = OrderStatus.Оплачен
			});
		}
	}
}
