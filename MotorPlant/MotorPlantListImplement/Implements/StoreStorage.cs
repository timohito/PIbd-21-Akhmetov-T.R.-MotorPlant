using MotorPlantBusinessLogic.BindingModels;
using MotorPlantBusinessLogic.Interfaces;
using MotorPlantBusinessLogic.ViewModels;
using MotorPlantListImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
namespace MotorPlantListImplement.Implements
{
    public class StoreStorage : IStoreStorage
    {
        private readonly DataListSingleton source;
        public StoreStorage()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<StoreViewModel> GetFullList()
        {
            List<StoreViewModel> result = new List<StoreViewModel>();
            foreach (var component in source.Stores)
            {
                result.Add(CreateModel(component));
            }
            return result;
        }
        public List<StoreViewModel> GetFilteredList(StoreBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            List<StoreViewModel> result = new List<StoreViewModel>();
            foreach (var Store in source.Stores)
            {
                if (Store.StoreName.Contains(model.StoreName))
                {
                    result.Add(CreateModel(Store));
                }
            }
            return result;
        }
        public StoreViewModel GetElement(StoreBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var store in source.Stores)
            {
                if (store.Id == model.Id || store.StoreName == model.StoreName)
                {
                    return CreateModel(store);
                }
            }
            return null;
        }
        public void Insert(StoreBindingModel model)
        {
            Store tempStore = new Store { Id = 1, StoreComponents = new Dictionary<int, (string, int)>() };
            foreach (var Store in source.Stores)
            {
                if (Store.Id >= tempStore.Id)
                {
                    tempStore.Id = Store.Id + 1;
                }
            }
            source.Stores.Add(CreateModel(model, tempStore));
        }
        public void Update(StoreBindingModel model)
        {
            Store tempStore = null;
            foreach (var Store in source.Stores)
            {
                if (Store.Id == model.Id)
                {
                    tempStore = Store;
                }
            }
            if (tempStore == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, tempStore);
        }
        public void Delete(StoreBindingModel model)
        {
            for (int i = 0; i < source.Stores.Count; ++i)
            {
                if (source.Stores[i].Id == model.Id)
                {
                    source.Stores.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
        private Store CreateModel(StoreBindingModel model, Store store)
        {
            store.StoreName = model.StoreName;
            store.ResponsibleName = model.ResponsibleName;
            store.DateCreation = model.DateCreation;
            // удаляем убранные
            foreach (var key in store.StoreComponents.Keys.ToList())
            {
                if (!model.StoreComponents.ContainsKey(key))
                {
                    store.StoreComponents.Remove(key);
                }
            }
            // обновляем существуюущие и добавляем новые
            foreach (var component in model.StoreComponents)
            {
                if (store.StoreComponents.ContainsKey(component.Key))
                {
                    store.StoreComponents[component.Key] = model.StoreComponents[component.Key];
                }
                else
                {
                    store.StoreComponents.Add(component.Key, model.StoreComponents[component.Key]);
                }
            }
            return store;
        }
        private StoreViewModel CreateModel(Store Store)
        {
            Dictionary<int, (string, int)> StoreComponents = new Dictionary<int, (string, int)>();
            foreach (var sc in Store.StoreComponents)
            {
                string componentName = string.Empty;
                foreach (var component in source.Components)
                {
                    if (sc.Key == component.Id)
                    {
                        componentName = component.ComponentName;
                        break;
                    }
                }
                StoreComponents.Add(sc.Key, (componentName, sc.Value.Item2));
            }
            return new StoreViewModel
            {
                Id = Store.Id,
                StoreName = Store.StoreName,
                ResponsibleName = Store.ResponsibleName,
                DateCreation = Store.DateCreation,
                StoreComponents = StoreComponents
            };
        }
    }
}