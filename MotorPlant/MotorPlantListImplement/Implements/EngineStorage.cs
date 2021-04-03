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
			foreach (var engine in source.Engines)
			{
				result.Add(CreateModel(engine));
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
			foreach (var engine in source.Engines)
			{
				if (engine.EngineName.Contains(model.EngineName))
				{
					result.Add(CreateModel(engine));
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
			foreach (var engine in source.Engines)
			{
				if (engine.Id == model.Id || engine.EngineName == model.EngineName)
				{
					return CreateModel(engine);
				}
			}
			return null;
		}

		public void Insert(EngineBindingModel model)
		{
			Engine tempEngine = new Engine { Id = 1, EngineComponents = new Dictionary<int, int>() };
			foreach (var engine in source.Engines)
			{
				if (engine.Id >= tempEngine.Id)
				{
					tempEngine.Id = engine.Id + 1;
				}
			}
			source.Engines.Add(CreateModel(model, tempEngine));
		}

		public void Update(EngineBindingModel model)
		{
			Engine tempEngine = null;
			foreach (var engine in source.Engines)
			{
				if (engine.Id == model.Id)
				{
					tempEngine = engine;
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
		
		private Engine CreateModel(EngineBindingModel model, Engine engine)
		{
			engine.EngineName = model.EngineName;
			engine.Price = model.Price;
			// удаляем убранные
			foreach (var key in engine.EngineComponents.Keys.ToList())
			{
				if (!model.EngineComponents.ContainsKey(key))
				{
					engine.EngineComponents.Remove(key);
				}
			}
			// обновляем существуюущие и добавляем новые
			foreach (var component in model.EngineComponents)
			{
				if (engine.EngineComponents.ContainsKey(component.Key))
				{
					engine.EngineComponents[component.Key] = model.EngineComponents[component.Key].Item2;
				}
				else
				{
					engine.EngineComponents.Add(component.Key, model.EngineComponents[component.Key].Item2);
				}
			}
			return engine;
		}

		private EngineViewModel CreateModel(Engine engine)
		{
			// требуется дополнительно получить список компонентов для изделия с названиями и их количество
			Dictionary<int, (string, int)> engineComponents = new Dictionary<int, (string, int)>();
			foreach (var ec in engine.EngineComponents)
			{
				string componentName = string.Empty;
				foreach (var component in source.Components)
				{
					if (ec.Key == component.Id)
					{
						componentName = component.ComponentName;
						break;
					}
				}
				engineComponents.Add(ec.Key, (componentName, ec.Value));
			}
			return new EngineViewModel
			{
				Id = engine.Id,
				EngineName = engine.EngineName,
				Price = engine.Price,
				EngineComponents = engineComponents
			};
		}
	}
}