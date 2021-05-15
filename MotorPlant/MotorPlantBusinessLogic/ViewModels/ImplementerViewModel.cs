using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using MotorPlantBusinessLogic.Attributes;

namespace MotorPlantBusinessLogic.ViewModels
{
	public class ImplementerViewModel
	{
		public int Id { get; set; }
		[Column(title: "ФИО исполнителя", gridViewAutoSize: GridViewAutoSize.Fill)]
		public string ImplementerFIO { get; set; }
		[Column(title: "Время на заказ", width: 50)]
		public int WorkingTime { get; set; }
		[Column(title: "Время на перерыв", width: 50)]
		public int PauseTime { get; set; }
	}
}
