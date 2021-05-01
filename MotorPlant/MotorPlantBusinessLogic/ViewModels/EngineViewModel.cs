using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace MotorPlantBusinessLogic.ViewModels
{
	[DataContract]
	public class EngineViewModel
	{
		[DataMember]
		public int Id { get; set; }

		[DataMember]
		[DisplayName("Название изделия")]
		public string EngineName { get; set; }

		[DataMember]
		[DisplayName("Цена")]
		public decimal Price { get; set; }

		[DataMember]
		public Dictionary<int, (string, int)> EngineComponents { get; set; }
	}
}