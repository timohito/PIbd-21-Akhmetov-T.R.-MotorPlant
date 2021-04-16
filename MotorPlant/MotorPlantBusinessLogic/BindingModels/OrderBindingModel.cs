using System;
using MotorPlantBusinessLogic.Enums;

namespace MotorPlantBusinessLogic.BindingModels
{
	public class OrderBindingModel
	{
		public int? Id { get; set; }

		public int EngineId { get; set; }

		public int Count { get; set; }

		public decimal Sum { get; set; }

		public OrderStatus Status { get; set; }

		public DateTime DateCreate { get; set; }

		public DateTime? DateImplement { get; set; }

		public DateTime? DateFrom { get; set; }

		public DateTime? DateTo { get; set; }
	}
}
