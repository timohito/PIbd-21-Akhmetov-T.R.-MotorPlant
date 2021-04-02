using MotorPlantBusinessLogic.BindingModels;
using MotorPlantBusinessLogic.ViewModels;
using System.Collections.Generic;

namespace MotorPlantBusinessLogic.Interfaces
{
	public interface IEngineStorage
	{
		List<EngineViewModel> GetFullList();

		List<EngineViewModel> GetFilteredList(EngineBindingModel model);

		EngineViewModel GetElement(EngineBindingModel model);

		void Insert(EngineBindingModel model);

		void Update(EngineBindingModel model);

		void Delete(EngineBindingModel model);
	}
}
