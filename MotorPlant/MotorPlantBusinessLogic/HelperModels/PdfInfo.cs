using System;
using System.Collections.Generic;
using System.Text;
using MotorPlantBusinessLogic.ViewModels;

namespace MotorPlantBusinessLogic.HelperModels
{
    class PdfInfo
    {
        public string FileName { get; set; }

        public string Title { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public List<ReportOrdersViewModel> Orders { get; set; }
    }
}