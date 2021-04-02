using MotorPlantBusinessLogic.BindingModels;
using MotorPlantBusinessLogic.ViewModels;
using System.Collections.Generic;

namespace MotorPlantBusinessLogic.Interfaces
{
	public interface IOrderStorage
	{
		List<OrderViewModel> GetFullList();

		List<OrderViewModel> GetFilteredList(OrderBindingModel model);

		OrderViewModel GetElement(OrderBindingModel model);

		void Insert(OrderBindingModel model);

		void Update(OrderBindingModel model);

		void Delete(OrderBindingModel model);
	}
}
