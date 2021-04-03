using System.ComponentModel.DataAnnotations;

namespace MotorPlantDatabaseImplement.Models
{
	public class EngineComponent
	{
		public int Id { get; set; }

		public int EngineId { get; set; }

		public int ComponentId { get; set; }

		[Required]
		public int Count { get; set; }

		public virtual Component Component { get; set; }

		public virtual Engine Engine { get; set; }
	}
}
