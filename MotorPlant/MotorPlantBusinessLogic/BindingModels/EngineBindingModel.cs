using System.Collections.Generic;

namespace MotorPlantBusinessLogic.BindingModels
{
	public class EngineBindingModel
	{
		public int? Id { get; set; }

		public string EngineName { get; set; }

		public decimal Price { get; set; }

		public Dictionary<int, (string, int)> EngineComponents { get; set; }
	}
}
