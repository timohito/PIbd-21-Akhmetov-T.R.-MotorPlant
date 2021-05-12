using MotorPlantBusinessLogic.BindingModels;
using MotorPlantBusinessLogic.ViewModels;
using System.Collections.Generic;

namespace MotorPlantBusinessLogic.Interfaces
{
	public interface IStoreStorage
	{

		List<StoreViewModel> GetFullList();
		List<StoreViewModel> GetFilteredList(StoreBindingModel model);
		StoreViewModel GetElement(StoreBindingModel model);
		void Insert(StoreBindingModel model);
		void Update(StoreBindingModel model);
		void Delete(StoreBindingModel model);
		bool CheckEnginesByComponents(int EngineId, int Count);
		void Extract(int EngineId, int Count);
	}
}