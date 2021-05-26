using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MotorPlantBusinessLogic.BindingModels;
using MotorPlantBusinessLogic.Interfaces;
using MotorPlantBusinessLogic.ViewModels;
using MotorPlantDatabaseImplement.Models;

namespace MotorPlantDatabaseImplement.Implements
{
    public class StoreStorage : IStoreStorage
    {
        public List<StoreViewModel> GetFullList()
        {
            using (var context = new MotorPlantDatabase())
            {
                return context.Stores
                .Include(rec => rec.StoreComponents)
                .ThenInclude(rec => rec.Component)
                .ToList()
                .Select(rec => new StoreViewModel
                {
                    Id = rec.Id,
                    StoreName = rec.StoreName,
                    ResponsibleName = rec.ResponsibleName,
                    DateCreation = rec.DateCreation,
                    StoreComponents = rec.StoreComponents
                .ToDictionary(recSC => recSC.ComponentId, recSC => ((recSC.Component?.ComponentName, recSC.Count)))
                })
                .ToList();
            }
        }
        public List<StoreViewModel> GetFilteredList(StoreBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new MotorPlantDatabase())
            {
                return context.Stores
                .Include(rec => rec.StoreComponents)
                .ThenInclude(rec => rec.Component)
                .Where(rec => rec.StoreName.Contains(model.StoreName))
                .ToList()
                .Select(rec => new StoreViewModel
                {
                    Id = rec.Id,
                    StoreName = rec.StoreName,
                    ResponsibleName = rec.ResponsibleName,
                    DateCreation = rec.DateCreation,
                    StoreComponents = rec.StoreComponents
                .ToDictionary(recSC => recSC.ComponentId, recSC => ((recSC.Component?.ComponentName, recSC.Count)))
                })
                .ToList();
            }
        }
        public StoreViewModel GetElement(StoreBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new MotorPlantDatabase())
            {
                var store = context.Stores
                .Include(rec => rec.StoreComponents)
                .ThenInclude(rec => rec.Component)
                .FirstOrDefault(rec => rec.StoreName == model.StoreName || rec.Id == model.Id);
                return store != null ?
                new StoreViewModel
                {
                    Id = store.Id,
                    StoreName = store.StoreName,
                    ResponsibleName = store.ResponsibleName,
                    DateCreation = store.DateCreation,
                    StoreComponents = store.StoreComponents
                .ToDictionary(recSC => recSC.ComponentId, recSC => ((recSC.Component?.ComponentName, recSC.Count)))
                } :
                null;
            }
        }

        public void Insert(StoreBindingModel model)
        {
            using (var context = new MotorPlantDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Store s = new Store
                        {
                            StoreName = model.StoreName,
                            ResponsibleName = model.ResponsibleName,
                            DateCreation = model.DateCreation,
                        };
                        context.Stores.Add(s);
                        context.SaveChanges();
                        CreateModel(model, s, context);
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }
        public void Update(StoreBindingModel model)
        {
            using (var context = new MotorPlantDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var element = context.Stores.FirstOrDefault(rec => rec.Id == model.Id);
                        if (element == null)
                        {
                            throw new Exception("Элемент не найден");
                        }
                        element.StoreName = model.StoreName;
                        element.ResponsibleName = model.ResponsibleName;
                        element.DateCreation = model.DateCreation;
                        CreateModel(model, element, context);
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public void Delete(StoreBindingModel model)
        {
            using (var context = new MotorPlantDatabase())
            {
                Store element = context.Stores.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Stores.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        private Store CreateModel(StoreBindingModel model, Store store, MotorPlantDatabase context)
        {
            // код изменён из-за ошибки вставки в бд, поэтому нужно передавать↑ уже с заполнеными полями и добавленным таблицу Stores  
            if (model.Id.HasValue)
            {
                var storeComponents = context.StoreComponents.Where(rec => rec.StoreId == model.Id.Value).ToList();
                // удалили те, которых нет в модели
                context.StoreComponents.RemoveRange(storeComponents.Where(rec => !model.StoreComponents.ContainsKey(rec.ComponentId)).ToList());
                context.SaveChanges();
                // обновили количество у существующих записей
                foreach (var updateComponent in storeComponents)
                {
                    updateComponent.Count = model.StoreComponents[updateComponent.ComponentId].Item2;
                    model.StoreComponents.Remove(updateComponent.ComponentId);
                }
                context.SaveChanges();
            }
            // добавили новые
            foreach (var sc in model.StoreComponents)
            {
                context.StoreComponents.Add(new StoreComponent
                {
                    StoreId = store.Id,
                    ComponentId = sc.Key,
                    Count = sc.Value.Item2
                });
                context.SaveChanges();
            }
            return store;
        }

        public bool CheckEnginesByComponents(int EngineId, int Count)
        {
            return true;
        }
        public void Extract(int EngineId, int Count)
        {
            using (var context = new MotorPlantDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var pc in context.EngineComponents.Where(rec => rec.EngineId == EngineId))
                        {
                            int tempCount = pc.Count * Count;
                            bool notEnough = true;
                            foreach (var store in context.Stores)
                            {
                                StoreComponent sc = context.StoreComponents.FirstOrDefault(rec => rec.ComponentId == pc.ComponentId && rec.StoreId == store.Id);
                                if (sc == null)
                                {
                                    continue;
                                }
                                int componentCount = sc.Count;
                                if (tempCount >= componentCount)
                                {
                                    store.StoreComponents.Remove(sc);
                                    if (tempCount == componentCount)
                                    {
                                        context.SaveChanges();
                                        notEnough = false;
                                        break;
                                    }
                                    tempCount -= componentCount;
                                }
                                else
                                {
                                    sc.Count -= tempCount;
                                    context.SaveChanges();
                                    notEnough = false;
                                    break;
                                }
                            }
                            if (notEnough)
                            {
                                throw new Exception("Недостаточно компонентов на складах");
                            }
                        }
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }
    }
}
