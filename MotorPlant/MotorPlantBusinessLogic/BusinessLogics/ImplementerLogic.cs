using MotorPlantBusinessLogic.BindingModels;
using MotorPlantBusinessLogic.Interfaces;
using MotorPlantBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MotorPlantBusinessLogic.BusinessLogics
{
    public class ImplementerLogic
    {
        private readonly IImplementerStorage _clientStorage;

        public ImplementerLogic(IImplementerStorage clientStorage)
        {
            _clientStorage = clientStorage;
        }

        public List<ImplementerViewModel> Read(ImplementerBindingModel model)
        {
            if (model == null)
            {
                return _clientStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<ImplementerViewModel> { _clientStorage.GetElement(model) };
            }
            return _clientStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(ImplementerBindingModel model)
        {
            var element = _clientStorage.GetElement(new ImplementerBindingModel
            {
                ImplementerFIO = model.ImplementerFIO
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть работник с таким ФИО");
            }
            if (model.Id.HasValue)
            {
                _clientStorage.Update(model);
            }
            else
            {
                _clientStorage.Insert(model);
            }
        }

        public void Delete(ImplementerBindingModel model)
        {
            var element = _clientStorage.GetElement(new ImplementerBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Работник не найден");
            }
            _clientStorage.Delete(model);
        }
    }
}
