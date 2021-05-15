using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using MotorPlantBusinessLogic.Attributes;

namespace MotorPlantBusinessLogic.ViewModels
{
	[DataContract]
	public class EngineViewModel
	{
		[DataMember]
		public int Id { get; set; }

		[DataMember]
		[Column(title: "Название изделия", gridViewAutoSize: GridViewAutoSize.Fill)]
		public string EngineName { get; set; }

		[DataMember]
		[Column(title: "Цена", width: 50)]
		public decimal Price { get; set; }

		[DataMember]
		public Dictionary<int, (string, int)> EngineComponents { get; set; }
	}
}