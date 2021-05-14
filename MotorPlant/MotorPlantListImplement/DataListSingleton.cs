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
        public List<Client> Clients { get; set; }
        public List<Implementer> Implementers { get; set; }
        public List<MessageInfo> Messages { get; set; }

        private DataListSingleton()
        {
            Components = new List<Component>();
            Orders = new List<Order>();
            Engines = new List<Engine>();
            Clients = new List<Client>();
            Implementers = new List<Implementer>();
            Messages = new List<MessageInfo>();
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