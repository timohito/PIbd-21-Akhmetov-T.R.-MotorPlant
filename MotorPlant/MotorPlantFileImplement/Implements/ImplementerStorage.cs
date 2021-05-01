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
	public class ImplementerStorage : IImplementerStorage
	{
        private readonly FileDataListSingleton source;
        public ImplementerStorage()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public List<ImplementerViewModel> GetFullList()
        {
            return source.Implementers
            .Select(CreateModel)
            .ToList();
        }
        public List<ImplementerViewModel> GetFilteredList(ImplementerBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return source.Implementers
            .Where(rec => rec.ImplementerFIO.Contains(model.ImplementerFIO))
            .Select(CreateModel)
            .ToList();
        }
        public ImplementerViewModel GetElement(ImplementerBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var Implementer = source.Implementers
            .FirstOrDefault(rec => rec.Id == model.Id);
            return Implementer != null ? CreateModel(Implementer) : null;
        }
        public void Insert(ImplementerBindingModel model)
        {
            int maxId = source.Implementers.Count > 0 ? source.Implementers.Max(rec => rec.Id) : 0;
            var element = new Implementer { Id = maxId + 1 };
            source.Implementers.Add(CreateModel(model, element));
        }
        public void Update(ImplementerBindingModel model)
        {
            var element = source.Implementers.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element);
        }
        public void Delete(ImplementerBindingModel model)
        {
            Implementer element = source.Implementers.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                source.Implementers.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private Implementer CreateModel(ImplementerBindingModel model, Implementer Implementer)
        {
            Implementer.ImplementerFIO = model.ImplementerFIO;
            Implementer.WorkingTime = model.WorkingTime;
            Implementer.PauseTime = model.PauseTime;
            return Implementer;
        }

        private ImplementerViewModel CreateModel(Implementer Implementer)
        {
            return new ImplementerViewModel
            {
                Id = Implementer.Id,
                ImplementerFIO = Implementer.ImplementerFIO,
                WorkingTime = Implementer.WorkingTime,
                PauseTime = Implementer.PauseTime
            };
        }
    }
}
