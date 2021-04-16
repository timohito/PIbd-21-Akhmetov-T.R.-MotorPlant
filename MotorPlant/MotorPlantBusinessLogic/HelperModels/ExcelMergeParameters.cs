using System;
using System.Collections.Generic;
using System.Text;
using DocumentFormat.OpenXml.Spreadsheet;

namespace MotorPlantBusinessLogic.HelperModels
{
    class ExcelMergeParameters
    {
        public Worksheet Worksheet { get; set; }

        public string CellFromName { get; set; }

        public string CellToName { get; set; }

        public string Merge => $"{CellFromName}:{CellToName}";
    }
}
