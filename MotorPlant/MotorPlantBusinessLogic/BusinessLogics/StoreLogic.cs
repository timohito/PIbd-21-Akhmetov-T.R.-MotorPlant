using MotorPlantBusinessLogic.BindingModels;
using MotorPlantBusinessLogic.Interfaces;
using MotorPlantBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;

namespace MotorPlantBusinessLogic.BusinessLogics
{
	public class StoreLogic
	{
		private readonly IStoreStorage _storeStorage;
		private readonly IComponentStorage _componentStorage;
		public StoreLogic(IStoreStorage storeStorage, IComponentStorage componentStorage)
		{
			_storeStorage = storeStorage;
			_componentStorage = componentStorage;
		}
		public List<StoreViewModel> Read(StoreBindingModel model)
		{
			if (model == null)
			{
				return _storeStorage.GetFullList();
			}
			if (model.Id.HasValue)
			{
				return new List<StoreViewModel> { _storeStorage.GetElement(model) };
			}
			return _storeStorage.GetFilteredList(model);
		}
		public void CreateOrUpdate(StoreBindingModel model)
		{
			var element = _storeStorage.GetElement(new StoreBindingModel { StoreName = model.StoreName });
			if (element != null && element.Id != model.Id)
			{
				throw new Exception("Уже есть склад с таким названием");
			}

			if (model.Id.HasValue)
			{
				_storeStorage.Update(model);
			}
			else
			{
				_storeStorage.Insert(model);
			}
		}
		public void Delete(StoreBindingModel model)
		{
			var element = _storeStorage.GetElement(new StoreBindingModel { Id = model.Id });
			if (element == null)
			{
				throw new Exception("Элемент не найден");
			}
			_storeStorage.Delete(model);
		}
		public void Fill(StoreBindingModel model, int componentId, int count)
		{
			var store = _storeStorage.GetElement(new StoreBindingModel { Id = model.Id });

			if (store.StoreComponents.ContainsKey(componentId))
			{
				store.StoreComponents[componentId] =
					(store.StoreComponents[componentId].Item1, store.StoreComponents[componentId].Item2 + count);
			}
			else
			{
				var component = _componentStorage.GetElement(new ComponentBindingModel
				{
					Id = componentId
				});
				if (component == null)
				{
					throw new Exception("Компонент не найден");
				}
				store.StoreComponents.Add(componentId, (component.ComponentName, count));
			}
			_storeStorage.Update(new StoreBindingModel
			{
				Id = store.Id,
				StoreName = store.StoreName,
				ResponsibleName = store.ResponsibleName,
				DateCreation = store.DateCreation,
				StoreComponents = store.StoreComponents
			});
		}
	}
}