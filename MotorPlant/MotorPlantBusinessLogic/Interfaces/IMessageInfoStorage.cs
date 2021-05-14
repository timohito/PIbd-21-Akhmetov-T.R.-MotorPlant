using MotorPlantBusinessLogic.BindingModels;
using MotorPlantBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MotorPlantBusinessLogic.Interfaces
{
    public interface IMessageInfoStorage
    {
        List<MessageInfoViewModel> GetFullList();
        List<MessageInfoViewModel> GetFilteredList(MessageInfoBindingModel model);
        void Insert(MessageInfoBindingModel model);
    }
}
