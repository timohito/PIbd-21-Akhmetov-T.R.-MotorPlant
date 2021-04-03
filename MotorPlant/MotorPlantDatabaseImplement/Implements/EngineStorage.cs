using MotorPlantBusinessLogic.BindingModels;
using MotorPlantBusinessLogic.Interfaces;
using MotorPlantBusinessLogic.ViewModels;
using MotorPlantDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
namespace MotorPlantDatabaseImplement.Implements
{
    public class EngineStorage : IEngineStorage
    {
        public List<EngineViewModel> GetFullList()
        {
            using (var context = new MotorPlantDatabase())
            {
                return context.Engines
                .Include(rec => rec.EngineComponents)
                .ThenInclude(rec => rec.Component)
                .ToList()
                .Select(rec => new EngineViewModel
                {
                    Id = rec.Id,
                    EngineName = rec.EngineName,
                    Price = rec.Price,
                    EngineComponents = rec.EngineComponents
                .ToDictionary(recEC => recEC.ComponentId, recEC => (recEC.Component?.ComponentName, recEC.Count))
                })
                .ToList();
            }
        }
        public List<EngineViewModel> GetFilteredList(EngineBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new MotorPlantDatabase())
            {
                return context.Engines
                .Include(rec => rec.EngineComponents)
                .ThenInclude(rec => rec.Component)
                .Where(rec => rec.EngineName.Contains(model.EngineName))
                .ToList()
                .Select(rec => new EngineViewModel
                {
                    Id = rec.Id,
                    EngineName = rec.EngineName,
                    Price = rec.Price,
                    EngineComponents = rec.EngineComponents
                .ToDictionary(recEC => recEC.ComponentId, recEC =>
                (recEC.Component?.ComponentName, recEC.Count))
                })
                .ToList();
            }
        }
        public EngineViewModel GetElement(EngineBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new MotorPlantDatabase())
            {
                var Engine = context.Engines
                .Include(rec => rec.EngineComponents)
                .ThenInclude(rec => rec.Component)
                .FirstOrDefault(rec => rec.EngineName == model.EngineName || rec.Id == model.Id);
                return Engine != null ?
                new EngineViewModel
                {
                    Id = Engine.Id,
                    EngineName = Engine.EngineName,
                    Price = Engine.Price,
                    EngineComponents = Engine.EngineComponents
                .ToDictionary(recEC => recEC.ComponentId, recEC => (recEC.Component?.ComponentName, recEC.Count))
                } :
                null;
            }
        }
        public void Insert(EngineBindingModel model)
        {
            using (var context = new MotorPlantDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Engine p = new Engine
                        {
                            EngineName = model.EngineName,
                            Price = model.Price
                        };
                        context.Engines.Add(p);
                        context.SaveChanges();
                        CreateModel(model, p, context);
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch(Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }
        public void Update(EngineBindingModel model)
        {
            using (var context = new MotorPlantDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var element = context.Engines.FirstOrDefault(rec => rec.Id == model.Id);
                        if (element == null)
                        {
                            throw new Exception("Элемент не найден");
                        }
                        element.EngineName = model.EngineName;
                        element.Price = model.Price;
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
        public void Delete(EngineBindingModel model)
        {
            using (var context = new MotorPlantDatabase())
            {
                Engine element = context.Engines.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Engines.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        private Engine CreateModel(EngineBindingModel model, Engine engine, MotorPlantDatabase context)
        {
            // код изменён из-за ошибки вставки в бд, поэтому нужно передавать↑ уже с заполнеными полями и добавленным таблицу Engines  
            if (model.Id.HasValue)
            {
                var EngineComponents = context.EngineComponents.Where(rec => rec.EngineId == model.Id.Value).ToList();
                // удалили те, которых нет в модели
                context.EngineComponents.RemoveRange(EngineComponents.Where(rec => !model.EngineComponents.ContainsKey(rec.ComponentId)).ToList());
                context.SaveChanges();
                // обновили количество у существующих записей
                foreach (var updateComponent in EngineComponents)
                {
                    updateComponent.Count = model.EngineComponents[updateComponent.ComponentId].Item2;
                    model.EngineComponents.Remove(updateComponent.ComponentId);
                }
                context.SaveChanges();
            }
            // добавили новые
            foreach (var ec in model.EngineComponents)
            {
                context.EngineComponents.Add(new EngineComponent
                {
                    EngineId = engine.Id,
                    ComponentId = ec.Key,
                    Count = ec.Value.Item2
                });
                context.SaveChanges();
            }
            return engine;
        }
    }
}