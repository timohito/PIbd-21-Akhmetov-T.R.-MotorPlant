using System;
using System.Collections.Generic;
using System.Text;
using MotorPlantBusinessLogic.Enums;

namespace MotorPlantListImplement
{
	public class Order
	{
		public int Id { get; set; }

		public int ClientId { get; set; }

		public int? ImplementerId { get; set; }

		public int EngineId { get; set; }

		public int Count { get; set; }

		public decimal Sum { get; set; }

		public OrderStatus Status { get; set; }

		public DateTime DateCreate { get; set; }

		public DateTime? DateImplement { get; set; }
	}
}
