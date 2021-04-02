using System.Collections.Generic;

namespace MotorPlantListImplement.Models
{
	public class Engine
	{
		public int Id { get; set; }

		public string EngineName { get; set; }

		public decimal Price { get; set; }

		public Dictionary<int, int> EngineComponents { get; set; }
	}
}