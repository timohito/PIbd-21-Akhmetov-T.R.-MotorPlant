using System.ComponentModel;

namespace MotorPlantBusinessLogic.ViewModels
{
	public class ComponentViewModel
	{
		public int Id { get; set; }

		[DisplayName("Название компонента")]
		public string ComponentName { get; set; }
	}
}
