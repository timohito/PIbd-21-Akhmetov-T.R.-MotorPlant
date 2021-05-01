using MotorPlantBusinessLogic.Enums;
using System;

namespace MotorPlantBusinessLogic.ViewModels
{
    public class ReportOrdersViewModel
    {
        public string ClientFIO { get; set; }

        public DateTime DateCreate { get; set; }

        public string EngineName { get; set; }

        public int Count { get; set; }

        public decimal Sum { get; set; }

        public OrderStatus Status { get; set; }
    }
}
