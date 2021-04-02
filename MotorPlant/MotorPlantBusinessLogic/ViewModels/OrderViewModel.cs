using System;
using System.ComponentModel;
using MotorPlantBusinessLogic.Enums;

namespace MotorPlantBusinessLogic.ViewModels
{
	public class OrderViewModel
	{
		public int Id { get; set; }

		public int EngineId { get; set; }

		[DisplayName("Двигатель")]
		public string EngineName { get; set; }

		[DisplayName("Количество")]
		public int Count { get; set; }

		[DisplayName("Сумма")]
		public decimal Sum { get; set; }

		[DisplayName("Статус")]
		public OrderStatus Status { get; set; }

		[DisplayName("Дата создания")]
		public DateTime DateCreate { get; set; }

		[DisplayName("Дата выполнения")]
		public DateTime? DateImplement { get; set; }
	}
}
