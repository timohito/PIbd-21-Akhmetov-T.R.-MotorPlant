using System;
using System.Collections.Generic;
using System.Text;
using MotorPlantBusinessLogic.ViewModels;

namespace MotorPlantBusinessLogic.HelperModels
{
    class WordInfo
    {
        public string FileName { get; set; }

        public string Title { get; set; }

        public List<EngineViewModel> Engines { get; set; }
    }
}
