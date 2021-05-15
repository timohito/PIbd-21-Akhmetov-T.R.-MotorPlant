using System.ComponentModel;
using MotorPlantBusinessLogic.Attributes;

namespace MotorPlantBusinessLogic.ViewModels
{
	public class ComponentViewModel
	{
		[Column(title: "Номер", width: 100)]
		public int Id { get; set; }

		[Column(title: "Название компонента", gridViewAutoSize: GridViewAutoSize.Fill)]
		public string ComponentName { get; set; }
	}
}
