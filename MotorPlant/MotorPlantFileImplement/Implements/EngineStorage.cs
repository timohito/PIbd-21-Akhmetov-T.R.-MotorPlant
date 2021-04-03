using MotorPlantBusinessLogic.BindingModels;
using MotorPlantBusinessLogic.Interfaces;
using MotorPlantBusinessLogic.ViewModels;
using MotorPlantFileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MotorPlantFileImplement.Implements
{
	public class EngineStorage : IEngineStorage
	{
		private readonly FileDataListSingleton source;
		public EngineStorage()
		{
			source = FileDataListSingleton.GetInstance();
		}
		public List<EngineViewModel> GetFullList()
		{
			return source.Engines
			.Select(CreateModel)
			.ToList();
		}
		public List<EngineViewModel> GetFilteredList(EngineBindingModel model)
		{
			if (model == null)
			{
				return null;
			}
			return source.Engines
			.Where(rec => rec.EngineName.Contains(model.EngineName))
			.Select(CreateModel)
			.ToList();
		}
		public EngineViewModel GetElement(EngineBindingModel model)
		{
			if (model == null)
			{
				return null;
			}
			var engine = source.Engines
			.FirstOrDefault(rec => rec.EngineName == model.EngineName || rec.Id == model.Id);
			return engine != null ? CreateModel(engine) : null;
		}
		public void Insert(EngineBindingModel model)
		{
			int maxId = source.Engines.Count > 0 ? source.Components.Max(rec => rec.Id) : 0;
			var element = new Engine { Id = maxId + 1, EngineComponents = new Dictionary<int, int>() };
			source.Engines.Add(CreateModel(model, element));
		}
		public void Update(EngineBindingModel model)
		{
			var element = source.Engines.FirstOrDefault(rec => rec.Id == model.Id);
			if (element == null)
			{
				throw new Exception("Элемент не найден");
			}
			CreateModel(model, element);
		}
		public void Delete(EngineBindingModel model)
		{
			Engine element = source.Engines.FirstOrDefault(rec => rec.Id == model.Id);
			if (element != null)
			{
				source.Engines.Remove(element);
			}
			else
			{
				throw new Exception("Элемент не найден");
			}
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
		private EngineViewModel CreateModel(Engine Engine)
		{
			return new EngineViewModel
			{
				Id = Engine.Id,
				EngineName = Engine.EngineName,
				Price = Engine.Price,
				EngineComponents = Engine.EngineComponents
				.ToDictionary(recEC => recEC.Key, recPC =>
				(source.Components.FirstOrDefault(recC => recC.Id == recPC.Key)?.ComponentName, recPC.Value))
			};
		}
	}
}