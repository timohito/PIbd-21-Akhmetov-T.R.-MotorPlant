﻿using MotorPlantBusinessLogic.Enums;
using MotorPlantFileImplement.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace MotorPlantFileImplement
{
	public class FileDataListSingleton
	{
		private static FileDataListSingleton instance;
		private readonly string ComponentFileName = "Component.xml";
		private readonly string OrderFileName = "Order.xml";
		private readonly string EngineFileName = "Engine.xml";
		private readonly string StoreFileName = "Store.xml";
		public List<Component> Components { get; set; }
		public List<Order> Orders { get; set; }
		public List<Engine> Engines { get; set; }
		public List<Store> Stores { get; set; }
		private FileDataListSingleton()
		{
			Components = LoadComponents();
			Orders = LoadOrders();
			Engines = LoadEngines();
			Stores = LoadStores();
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
			SaveStores();
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
			var list = new List<Order>();
			if (File.Exists(OrderFileName))
			{
				XDocument xDocument = XDocument.Load(OrderFileName);
				var xElements = xDocument.Root.Elements("Order").ToList();
				foreach (var elem in xElements)
				{
					list.Add(new Order
					{
						Id = Convert.ToInt32(elem.Attribute("Id").Value),
						EngineId = Convert.ToInt32(elem.Element("EngineId").Value),
						Count = Convert.ToInt32(elem.Element("Count").Value),
						Sum = Convert.ToDecimal(elem.Element("Sum").Value),
						Status = (OrderStatus)Convert.ToInt32(elem.Element("Status").Value),
						DateCreate = Convert.ToDateTime(elem.Element("DateCreate")?.Value),
						DateImplement = String.IsNullOrEmpty(elem.Element("DateImplement").Value) ? DateTime.MinValue : Convert.ToDateTime(elem.Element("DateImplement").Value)
					});
				}
			}
			return list;
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
					var engComp = new Dictionary<int, int>();
					foreach (var component in elem.Element("EngineComponents").Elements("EngineComponent").ToList())
					{
						engComp.Add(Convert.ToInt32(component.Element("Key").Value), Convert.ToInt32(component.Element("Value").Value));
					}
					list.Add(new Engine
					{
						Id = Convert.ToInt32(elem.Attribute("Id").Value),
						EngineName = elem.Element("EngineName").Value,
						Price = Convert.ToDecimal(elem.Element("Price").Value),
						EngineComponents = engComp
					});
				}
			}
			return list;
		}
		private List<Store> LoadStores()
		{
			var list = new List<Store>();
			if (File.Exists(StoreFileName))
			{
				XDocument xDocument = XDocument.Load(StoreFileName);
				var xElements = xDocument.Root.Elements("Store").ToList();
				foreach (var elem in xElements)
				{
					var storeComponents = new Dictionary<int, (string, int)>();
					foreach (var component in elem.Element("StoreComponents").Elements("StoreComponent").ToList())
					{
						var componentData = (component.Element("Component").Element("Name").Value, Convert.ToInt32(component.Element("Component").Element("Count").Value));
						storeComponents.Add(Convert.ToInt32(component.Element("Key").Value), componentData);
					}
					list.Add(new Store
					{
						Id = Convert.ToInt32(elem.Attribute("Id").Value),
						StoreName = elem.Element("StoreName").Value,
						ResponsibleName = elem.Element("ResponsibleName").Value,
						DateCreation = Convert.ToDateTime(elem.Element("DateCreation").Value),
						StoreComponents = storeComponents
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
			if (Orders != null)
			{
				var xElement = new XElement("Orders");
				foreach (var order in Orders)
				{
					xElement.Add(new XElement("Order",
					new XAttribute("Id", order.Id),
					new XElement("EngineId", order.EngineId),
					new XElement("Count", order.Count),
					new XElement("Sum", order.Sum),
					new XElement("Status", (int)order.Status),
					new XElement("DateCreate", order.DateCreate),
					new XElement("DateImplement", order.DateImplement)
					));
				}
				XDocument xDocument = new XDocument(xElement);
				xDocument.Save(OrderFileName);
			}
		}
		private void SaveEngines()
		{
			if (Engines != null)
			{
				var xElement = new XElement("Engines");
				foreach (var engine in Engines)
				{
					var compElement = new XElement("EngineComponents");
					foreach (var component in engine.EngineComponents)
					{
						compElement.Add(new XElement("EngineComponent",
						new XElement("Key", component.Key),
						new XElement("Value", component.Value)));
					}
					xElement.Add(new XElement("Engine",
					new XAttribute("Id", engine.Id),
					new XElement("EngineName", engine.EngineName),
					new XElement("Price", engine.Price),
					compElement));
				}
				XDocument xDocument = new XDocument(xElement);
				xDocument.Save(EngineFileName);
			}
		}
		private void SaveStores()
		{
			if (Stores != null)
			{
				var xElement = new XElement("Stores");
				foreach (var store in Stores)
				{
					var compElement = new XElement("StoreComponents");
					foreach (var component in store.StoreComponents)
					{
						var element = new XElement("Component");
						element.Add(
							new XElement("Name", component.Value.Item1), new XElement("Count", component.Value.Item2)
							);
						compElement.Add(new XElement("StoreComponent",
						new XElement("Key", component.Key),
						element));
					}
					xElement.Add(new XElement("Store",
					new XAttribute("Id", store.Id),
					new XElement("StoreName", store.StoreName),
					new XElement("ResponsibleName", store.ResponsibleName),
					new XElement("DateCreation", store.DateCreation),
					compElement));
				}
				XDocument xDocument = new XDocument(xElement);
				xDocument.Save(StoreFileName);
			}
		}
	}
}