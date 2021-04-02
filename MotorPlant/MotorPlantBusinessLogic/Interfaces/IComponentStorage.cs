using System.Collections.Generic;
using MotorPlantBusinessLogic.BindingModels;
using MotorPlantBusinessLogic.ViewModels;

namespace MotorPlantBusinessLogic.Interfaces
{
	public interface IComponentStorage
	{
		List<ComponentViewModel> GetFullList();

		List<ComponentViewModel> GetFilteredList(ComponentBindingModel model);

		ComponentViewModel GetElement(ComponentBindingModel model);

		void Insert(ComponentBindingModel model);

		void Update(ComponentBindingModel model);

		void Delete(ComponentBindingModel model);
	}
}
