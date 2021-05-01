using MotorPlantBusinessLogic.BindingModels;
using MotorPlantBusinessLogic.Interfaces;
using MotorPlantBusinessLogic.ViewModels;
using MotorPlantListImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
namespace MotorPlantListImplement.Implements
{
	public class EngineStorage : IEngineStorage
	{
		private readonly DataListSingleton source;

		public EngineStorage()
		{
			source = DataListSingleton.GetInstance();
		}

		public List<EngineViewModel> GetFullList()
		{
			List<EngineViewModel> result = new List<EngineViewModel>();
			foreach (var component in source.Engines)
			{
				result.Add(CreateModel(component));
			}
			return result;
		}

		public List<EngineViewModel> GetFilteredList(EngineBindingModel model)
		{
			if (model == null)
			{
				return null;
			}
			List<EngineViewModel> result = new List<EngineViewModel>();
			foreach (var Engine in source.Engines)
			{
				if (Engine.EngineName.Contains(model.EngineName))
				{
					result.Add(CreateModel(Engine));
				}
			}
			return result;
		}
		public EngineViewModel GetElement(EngineBindingModel model)
		{
			if (model == null)
			{
				return null;
			}
			foreach (var Engine in source.Engines)
			{
				if (Engine.Id == model.Id || Engine.EngineName == model.EngineName)
				{
					return CreateModel(Engine);
				}
			}
			return null;
		}
		public void Insert(EngineBindingModel model)
		{
			Engine tempEngine = new Engine { Id = 1, EngineComponents = new Dictionary<int, int>() };
			foreach (var Engine in source.Engines)
			{
				if (Engine.Id >= tempEngine.Id)
				{
					tempEngine.Id = Engine.Id + 1;
				}
			}
			source.Engines.Add(CreateModel(model, tempEngine));
		}
		public void Update(EngineBindingModel model)
		{
			Engine tempEngine = null;
			foreach (var Engine in source.Engines)
			{
				if (Engine.Id == model.Id)
				{
					tempEngine = Engine;
				}
			}
			if (tempEngine == null)
			{
				throw new Exception("Элемент не найден");
			}
			CreateModel(model, tempEngine);
		}
		public void Delete(EngineBindingModel model)
		{
			for (int i = 0; i < source.Engines.Count; ++i)
			{
				if (source.Engines[i].Id == model.Id)
				{
					source.Engines.RemoveAt(i);
					return;
				}
			}
			throw new Exception("Элемент не найден");
		}
		private Engine CreateModel(EngineBindingModel model, Engine Engine)
		{
			Engine.EngineName = model.EngineName;
			Engine.Price = model.Price;
			// удаляем убранные
			foreach (var key in Engine.EngineComponents.Keys.ToList())
			{
				if (!model.EngineComponents.ContainsKey(key))
				{
					Engine.EngineComponents.Remove(key);
				}
			}
			// обновляем существуюущие и добавляем новые
			foreach (var component in model.EngineComponents)
			{
				if (Engine.EngineComponents.ContainsKey(component.Key))
				{
					Engine.EngineComponents[component.Key] = model.EngineComponents[component.Key].Item2;
				}
				else
				{
					Engine.EngineComponents.Add(component.Key, model.EngineComponents[component.Key].Item2);
				}
			}
			return Engine;
		}

		private EngineViewModel CreateModel(Engine Engine)
		{
			// требуется дополнительно получить список компонентов для изделия с названиями и их количество
			Dictionary<int, (string, int)> EngineComponents = new Dictionary<int, (string, int)>();
			foreach (var pc in Engine.EngineComponents)
			{
				string componentName = string.Empty;
				foreach (var component in source.Components)
				{
					if (pc.Key == component.Id)
					{
						componentName = component.ComponentName;
						break;
					}
				}
				EngineComponents.Add(pc.Key, (componentName, pc.Value));
			}
			return new EngineViewModel
			{
				Id = Engine.Id,
				EngineName = Engine.EngineName,
				Price = Engine.Price,
				EngineComponents = EngineComponents
			};
		}
	}
}