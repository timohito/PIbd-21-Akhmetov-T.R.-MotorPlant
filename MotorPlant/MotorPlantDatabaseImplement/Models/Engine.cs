using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotorPlantDatabaseImplement.Models
{
	public class Engine
	{
		public int Id { get; set; }

		[Required]
		public string EngineName { get; set; }

		[Required]
		public decimal Price { get; set; }

		[ForeignKey("EngineId")]
		public virtual List<EngineComponent> EngineComponents { get; set; }

		[ForeignKey("EngineId")]
		public virtual List<Order> Orders { get; set; }
	}
}