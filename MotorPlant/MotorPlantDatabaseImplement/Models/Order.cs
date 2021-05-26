using System;
using System.ComponentModel.DataAnnotations;
using MotorPlantBusinessLogic.Enums;

namespace MotorPlantDatabaseImplement.Models
{
	public class Order
	{
		public int Id { get; set; }

		public int EngineId { get; set; }

		[Required]
		public int Count { get; set; }

		[Required]
		public decimal Sum { get; set; }

		[Required]
		public OrderStatus Status { get; set; }

		[Required]
		public DateTime DateCreate { get; set; }

		public virtual Engine Engine { get; set; }

		public DateTime? DateImplement { get; set; }
	}
}
