using MotorPlantListImplement.Models;
using System.Collections.Generic;

namespace MotorPlantListImplement
{
	public class DataListSingleton
	{
		private static DataListSingleton instance;

		public List<Component> Components { get; set; }

		public List<Order> Orders { get; set; }

		public List<Engine> Engines { get; set; }

		public List<Store> Stores { get; set; }

		private DataListSingleton()
		{
			Components = new List<Component>();
			Orders = new List<Order>();
			Engines = new List<Engine>();
			Stores = new List<Store>();
		}

		public static DataListSingleton GetInstance()
		{
			if (instance == null)
			{
				instance = new DataListSingleton();
			}
			return instance;
		}
	}
}