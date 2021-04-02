using MotorPlantBusinessLogic.BindingModels;
using MotorPlantBusinessLogic.Interfaces;
using MotorPlantBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;

namespace MotorPlantBusinessLogic.BusinessLogics
{
    public class EngineLogic
    {
		private readonly IEngineStorage _engineStorage;

		public EngineLogic(IEngineStorage EngineStorage)
		{
			_engineStorage = EngineStorage;
		}

		public List<EngineViewModel> Read(EngineBindingModel model)
		{
			if (model == null)
			{
				return _engineStorage.GetFullList();
			}
			if (model.Id.HasValue)
			{
				return new List<EngineViewModel> { _engineStorage.GetElement(model) };
			}
			return _engineStorage.GetFilteredList(model);
		}

		public void CreateOrUpdate(EngineBindingModel model)
		{
			var element = _engineStorage.GetElement(new EngineBindingModel { EngineName = model.EngineName });
			if (element != null && element.Id != model.Id)
			{
				throw new Exception("Уже есть изделие с таким названием");
			}
			if (model.Id.HasValue)
			{
				_engineStorage.Update(model);
			}
			else
			{
				_engineStorage.Insert(model);
			}
		}

		public void Delete(EngineBindingModel model)
		{
			var element = _engineStorage.GetElement(new EngineBindingModel { Id = model.Id });
			if (element == null)
			{
				throw new Exception("Двигатель не найден");
			}
			_engineStorage.Delete(model);
		}
	}
}
