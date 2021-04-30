using System;
using System.Collections.Generic;

namespace MotorPlantBusinessLogic.ViewModels
{
    public class ReportEngineComponentViewModel
    {
        public string EngineName { get; set; }

        public int TotalCount { get; set; }

        public List<Tuple<string, int>> Components { get; set; }
    }
}