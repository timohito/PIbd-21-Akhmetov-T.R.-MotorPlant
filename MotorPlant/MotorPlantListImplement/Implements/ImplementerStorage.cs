using MotorPlantBusinessLogic.BindingModels;
using MotorPlantBusinessLogic.Interfaces;
using MotorPlantBusinessLogic.ViewModels;
using MotorPlantListImplement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MotorPlantListImplement.Implements
{
	public class ImplementerStorage : IImplementerStorage
	{
        private readonly DataListSingleton source;

        public ImplementerStorage()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<ImplementerViewModel> GetFullList()
        {
            List<ImplementerViewModel> result = new List<ImplementerViewModel>();
            foreach (var Implementer in source.Implementers)
            {
                result.Add(CreateModel(Implementer));
            }
            return result;
        }

        public List<ImplementerViewModel> GetFilteredList(ImplementerBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            List<ImplementerViewModel> result = new List<ImplementerViewModel>();
            foreach (var Implementer in source.Implementers)
            {
                if (Implementer.ImplementerFIO.Contains(model.ImplementerFIO))
                {
                    result.Add(CreateModel(Implementer));
                }
            }
            if (result.Count > 0)
            {
                return result;
            }
            return null;
        }

        public ImplementerViewModel GetElement(ImplementerBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var Implementer in source.Implementers)
            {
                if (Implementer.Id == model.Id)
                {
                    return CreateModel(Implementer);
                }
            }
            return null;
        }

        public void Insert(ImplementerBindingModel model)
        {
            Implementer tempImplementer = new Implementer { Id = 1 };
            foreach (var Implementer in source.Implementers)
            {
                if (Implementer.Id >= tempImplementer.Id)
                {
                    tempImplementer.Id = Implementer.Id + 1;
                }
            }
            source.Implementers.Add(CreateModel(model, tempImplementer));
        }

        public void Update(ImplementerBindingModel model)
        {
            Implementer tempImplementer = null;
            foreach (var Implementer in source.Implementers)
            {
                if (Implementer.Id == model.Id)
                {
                    tempImplementer = Implementer;
                }
            }
            if (tempImplementer == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, tempImplementer);
        }

        public void Delete(ImplementerBindingModel model)
        {
            for (int i = 0; i < source.Implementers.Count; ++i)
            {
                if (source.Implementers[i].Id == model.Id.Value)
                {
                    source.Implementers.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }

        private Implementer CreateModel(ImplementerBindingModel model, Implementer Implementer)
        {
            Implementer.ImplementerFIO = model.ImplementerFIO;
            Implementer.PauseTime = model.PauseTime;
            Implementer.WorkingTime = model.WorkingTime;
            return Implementer;
        }

        private ImplementerViewModel CreateModel(Implementer Implementer)
        {
            return new ImplementerViewModel
            {
                Id = Implementer.Id,
                ImplementerFIO = Implementer.ImplementerFIO,
                PauseTime = Implementer.PauseTime,
                WorkingTime = Implementer.WorkingTime
            };
        }
    }
}
