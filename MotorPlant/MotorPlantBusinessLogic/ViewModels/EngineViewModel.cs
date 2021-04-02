using System.Collections.Generic;
using System.ComponentModel;

namespace MotorPlantBusinessLogic.ViewModels
{
	public class EngineViewModel
	{
		public int Id { get; set; }

		[DisplayName("Название изделия")]
		public string EngineName { get; set; }

		[DisplayName("Цена")]
		public decimal Price { get; set; }

		public Dictionary<int, (string, int)> EngineComponents { get; set; }
	}
}