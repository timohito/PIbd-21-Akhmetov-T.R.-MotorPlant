using MotorPlantBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MotorPlantBusinessLogic.HelperModels
{
    class StoreWordInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<StoreViewModel> Stores { get; set; }
    }
}
