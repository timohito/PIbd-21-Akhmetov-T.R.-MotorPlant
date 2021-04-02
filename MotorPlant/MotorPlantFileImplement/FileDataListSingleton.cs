using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using MotorPlantListImplement;	
using MotorPlantListImplement.Models;

namespace MotorPlantFileImplement
{
	public class FileDataListSingleton
	{
		private static FileDataListSingleton instance;
		private readonly string ComponentFileName = "Component.xml";
		private readonly string OrderFileName = "Order.xml";
		private readonly string EngineFileName = "Engine.xml";
		public List<Component> Components { get; set; }
		public List<Order> Orders { get; set; }
		public List<Engine> Engines { get; set; }
		private FileDataListSingleton()
		{
			Components = LoadComponents();
			Orders = LoadOrders();
			Engines = LoadEngines();
		}
		public static FileDataListSingleton GetInstance()
		{
			if (instance == null)
			{
				instance = new FileDataListSingleton();
			}
			return instance;
		}
		~FileDataListSingleton()
		{
			SaveComponents();
			SaveOrders();
			SaveEngines();
		}
		private List<Component> LoadComponents()
		{
			var list = new List<Component>();
			if (File.Exists(ComponentFileName))
			{
				XDocument xDocument = XDocument.Load(ComponentFileName);
				var xElements = xDocument.Root.Elements("Component").ToList();
				foreach (var elem in xElements)
				{
					list.Add(new Component
					{
						Id = Convert.ToInt32(elem.Attribute("Id").Value),
						ComponentName = elem.Element("ComponentName").Value
					});
				}
			}
			return list;
		}

		private List<Order> LoadOrders()
		{
			// прописать логику
		}

		private List<Engine> LoadEngines()
		{
			var list = new List<Engine>();
			if (File.Exists(EngineFileName))
			{
				XDocument xDocument = XDocument.Load(EngineFileName);
				var xElements = xDocument.Root.Elements("Engine").ToList();
				foreach (var elem in xElements)
				{
					var prodComp = new Dictionary<int, int>();
					foreach (var component in elem.Element("EngineComponents").Elements("EngineComponent").ToList())
					{
						prodComp.Add(Convert.ToInt32(component.Element("Key").Value), Convert.ToInt32(component.Element("Value").Value));
					}
					list.Add(new Engine
					{
						Id = Convert.ToInt32(elem.Attribute("Id").Value),
						EngineName = elem.Element("EngineName").Value,
						Price = Convert.ToDecimal(elem.Element("Price").Value),
						EngineComponents = prodComp
					});
				}
			}
			return list;
		}

		private void SaveComponents()
		{
			if (Components != null)
			{
				var xElement = new XElement("Components");
				foreach (var component in Components)
				{
					xElement.Add(new XElement("Component",
					new XAttribute("Id", component.Id),
					new XElement("ComponentName", component.ComponentName)));
				}
				XDocument xDocument = new XDocument(xElement);
				xDocument.Save(ComponentFileName);
			}
		}

		private void SaveOrders()
		{
			// прописать логику
		}

		private void SaveEngines()
		{
			if (Engines != null)
			{
				var xElement = new XElement("Engines");
				foreach (var Engine in Engines)
				{
					var compElement = new XElement("EngineComponents");
					foreach (var component in Engine.EngineComponents)
					{
						compElement.Add(new XElement("EngineComponent",
						new XElement("Key", component.Key),
						new XElement("Value", component.Value)));
					}
					xElement.Add(new XElement("Engine",
					new XAttribute("Id", Engine.Id),
					new XElement("EngineName", Engine.EngineName),
					new XElement("Price", Engine.Price),
					compElement));
				}
				XDocument xDocument = new XDocument(xElement);
				xDocument.Save(EngineFileName);
			}
		}
	}
}



		