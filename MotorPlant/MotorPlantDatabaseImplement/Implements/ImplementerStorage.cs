using MotorPlantBusinessLogic.BindingModels;
using MotorPlantBusinessLogic.Interfaces;
using MotorPlantBusinessLogic.ViewModels;
using MotorPlantDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MotorPlantDatabaseImplement.Implements
{
	public class ImplementerStorage : IImplementerStorage
	{
        public List<ImplementerViewModel> GetFullList()
        {
            using (var context = new MotorPlantDatabase())
            {
                return context.Implementers.Select(rec => new ImplementerViewModel
                {
                    Id = rec.Id,
                    ImplementerFIO = rec.ImplementerFIO,
                    WorkingTime = rec.WorkingTime,
                    PauseTime = rec.PauseTime,
                })
                .ToList();
            }
        }

        public List<ImplementerViewModel> GetFilteredList(ImplementerBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new MotorPlantDatabase())
            {
                return context.Implementers
                .Where(rec => rec.ImplementerFIO.Contains(model.ImplementerFIO))
                .Select(rec => new ImplementerViewModel
                {
                    Id = rec.Id,
                    ImplementerFIO = rec.ImplementerFIO,
                    WorkingTime = rec.WorkingTime,
                    PauseTime = rec.PauseTime,
                })
                .ToList();
            }
        }

        public ImplementerViewModel GetElement(ImplementerBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new MotorPlantDatabase())
            {
                var Implementer = context.Implementers
                .FirstOrDefault(rec => rec.ImplementerFIO == model.ImplementerFIO ||
                rec.Id == model.Id);
                return Implementer != null ?
                new ImplementerViewModel
                {
                    Id = Implementer.Id,
                    ImplementerFIO = Implementer.ImplementerFIO,
                    WorkingTime = Implementer.WorkingTime,
                    PauseTime = Implementer.PauseTime,
                } :
                null;
            }
        }

        public void Insert(ImplementerBindingModel model)
        {
            using (var context = new MotorPlantDatabase())
            {
                context.Implementers.Add(CreateModel(model, new Implementer(), context));
                context.SaveChanges();
            }
        }

        public void Update(ImplementerBindingModel model)
        {
            using (var context = new MotorPlantDatabase())
            {
                var element = context.Implementers.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Работник не найден");
                }
                CreateModel(model, element, context);
                context.SaveChanges();
            }
        }

        public void Delete(ImplementerBindingModel model)
        {
            using (var context = new MotorPlantDatabase())
            {
                Implementer element = context.Implementers.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Implementers.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Работник не найден");
                }
            }
        }

        private Implementer CreateModel(ImplementerBindingModel model, Implementer Implementer, MotorPlantDatabase database)
        {
            Implementer.ImplementerFIO = model.ImplementerFIO;
            Implementer.WorkingTime = model.WorkingTime;
            Implementer.PauseTime = model.PauseTime;
            return Implementer;
        }
    }
}
