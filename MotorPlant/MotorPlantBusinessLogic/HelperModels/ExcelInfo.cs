using System;
using System.Collections.Generic;
using System.Text;
using MotorPlantBusinessLogic.ViewModels;

namespace MotorPlantBusinessLogic.HelperModels
{
    class ExcelInfo
    {
        public string FileName { get; set; }

        public string Title { get; set; }

        public List<ReportEngineComponentViewModel> EngineComponents { get; set; }
    }
}
