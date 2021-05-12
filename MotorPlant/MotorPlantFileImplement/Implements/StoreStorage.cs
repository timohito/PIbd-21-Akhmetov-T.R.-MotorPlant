using MotorPlantBusinessLogic.BindingModels;
using MotorPlantBusinessLogic.Interfaces;
using MotorPlantBusinessLogic.ViewModels;
using MotorPlantFileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MotorPlantFileImplement.Implements
{
    public class StoreStorage : IStoreStorage
    {
        private readonly FileDataListSingleton source;
        public StoreStorage()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public List<StoreViewModel> GetFullList()
        {
            return source.Stores
            .Select(CreateModel)
            .ToList();
        }
        public List<StoreViewModel> GetFilteredList(StoreBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return source.Stores
            .Where(rec => rec.StoreName.Contains(model.StoreName))
            .Select(CreateModel)
            .ToList();
        }
        public StoreViewModel GetElement(StoreBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var store = source.Stores
            .FirstOrDefault(rec => rec.StoreName == model.StoreName || rec.Id == model.Id);
            return store != null ? CreateModel(store) : null;
        }

        public void Insert(StoreBindingModel model)
        {
            int maxId = source.Stores.Count > 0 ? source.Components.Max(rec => rec.Id) : 0;
            var element = new Store { Id = maxId + 1, StoreComponents = new Dictionary<int, (string, int)>() };
            source.Stores.Add(CreateModel(model, element));
        }
        public void Update(StoreBindingModel model)
        {
            var element = source.Stores.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element);
        }
        public void Delete(StoreBindingModel model)
        {
            Store element = source.Stores.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                source.Stores.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
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
        private StoreViewModel CreateModel(Store store)
        {
            return new StoreViewModel
            {
                Id = store.Id,
                StoreName = store.StoreName,
                ResponsibleName = store.ResponsibleName,
                DateCreation = store.DateCreation,
                StoreComponents = store.StoreComponents.ToDictionary(recPC => recPC.Key, recPC => recPC.Value)
            };
        }
        private bool CheckComponent(int ComponentId, int Count)
        {
            int tempCount = 0;
            foreach (var store in source.Stores)
            {
                if (store.StoreComponents.ContainsKey(ComponentId))
                {
                    tempCount += store.StoreComponents[ComponentId].Item2;
                    if (tempCount >= Count)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool CheckEnginesByComponents(int EngineId, int Count)
        {
            Engine Engine = source.Engines.FirstOrDefault(rec => rec.Id == EngineId);
            if (Engine == null)
            {
                return false;
            }
            foreach (var component in Engine.EngineComponents)
            {
                if (!CheckComponent(component.Key, component.Value * Count))
                {
                    return false;
                }
            }
            return true;

        }
        public void Extract(int EngineId, int Count)
        {
            Engine Engine = source.Engines.FirstOrDefault(rec => rec.Id == EngineId);
            if (Engine == null)
            {
                return;
            }
            foreach (var component in Engine.EngineComponents)
            {
                int tempCount = component.Value * Count;
                foreach (var store in source.Stores)
                {
                    if (store.StoreComponents.ContainsKey(component.Key))
                    {
                        int componentCount = store.StoreComponents[component.Key].Item2;
                        if (tempCount >= componentCount)
                        {
                            store.StoreComponents.Remove(component.Key);
                            if (tempCount == componentCount)
                            {
                                break;
                            }
                            tempCount -= componentCount;
                        }
                        else
                        {
                            store.StoreComponents[component.Key] = (store.StoreComponents[component.Key].Item1, store.StoreComponents[component.Key].Item2 - tempCount);
                            break;
                        }
                    }
                }
            }
        }
    }
} 
    
