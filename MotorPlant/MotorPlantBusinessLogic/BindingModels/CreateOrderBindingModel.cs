using System.Runtime.Serialization;

namespace MotorPlantBusinessLogic.BindingModels
{
	[DataContract]
	public class CreateOrderBindingModel
	{
		[DataMember]
		public int ClientId	{ get; set; }

		[DataMember]
		public int EngineId { get; set; }

		[DataMember]
		public int Count { get; set; }

		[DataMember]
		public decimal Sum { get; set; }
	}
}
