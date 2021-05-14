using MotorPlantBusinessLogic.Enums;
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
		private readonly string ClientFileName = "Client.xml";
		private readonly string ImplementerFileName = "Implementer.xml";
		private readonly string MessageFileName = "Message.xml";
		public List<Component> Components { get; set; }
		public List<Order> Orders { get; set; }
		public List<Engine> Engines { get; set; }
		public List<Client> Clients { get; set; }
		public List<Implementer> Implementers { get; set; }
		public List<MessageInfo> Messages { get; set; }
		private FileDataListSingleton()
		{
			Components = LoadComponents();
			Orders = LoadOrders();
			Engines = LoadEngines();
			Clients = LoadClients();
			Implementers = LoadImplementers();
			Messages = LoadMessages();
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
			SaveClients();
			SaveImplementers();
			SaveMessages();
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
						ClientId = Convert.ToInt32(elem.Element("ClientId")?.Value),
						ImplementerId = Convert.ToInt32(elem.Element("ImplementerId").Value),
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
		private List<Client> LoadClients()
		{
			var list = new List<Client>();
			if (File.Exists(ClientFileName))
			{
				XDocument xDocument = XDocument.Load(ClientFileName);
				var xElements = xDocument.Root.Elements("Clients").ToList();
				foreach (var elem in xElements)
				{
					list.Add(new Client
					{
						Id = Convert.ToInt32(elem.Attribute("Id").Value),
						ClientFIO = elem.Element("ClientFIO").Value,
						Email = elem.Element("Email").Value,
						Password = elem.Element("Password").Value
					});
				}
			}
			return list;
		}
		private List<Implementer> LoadImplementers()
		{
			var list = new List<Implementer>();
			if (File.Exists(ImplementerFileName))
			{
				XDocument xDocument = XDocument.Load(ImplementerFileName);
				var xElements = xDocument.Root.Elements("Implementer").ToList();
				foreach (var elem in xElements)
				{
					list.Add(new Implementer
					{
						Id = Convert.ToInt32(elem.Attribute("Id").Value),
						ImplementerFIO = elem.Element("ImplementerFIO").Value,
						WorkingTime = Convert.ToInt32(elem.Attribute("WorkingTime").Value),
						PauseTime = Convert.ToInt32(elem.Attribute("PauseTime").Value),
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
					new XElement("ClientId", order.ClientId),
					new XElement("ImplementerId", order.ImplementerId),
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
		private List<MessageInfo> LoadMessages()
		{
			var list = new List<MessageInfo>();
			if (File.Exists(MessageFileName))
			{
				XDocument xDocument = XDocument.Load(MessageFileName);
				var xElements = xDocument.Root.Elements("Message").ToList();

				foreach (var elem in xElements)
				{
					list.Add(new MessageInfo
					{
						MessageId = elem.Attribute("Id").Value,
						ClientId = Convert.ToInt32(elem.Element("ClientId").Value),
						SenderName = elem.Element("ClientId").Value,
						Subject = elem.Element("Subject").Value,
						Body = elem.Element("Body").Value,
						DateDelivery = Convert.ToDateTime(elem.Element("DateDelivery").Value)
					});
				}
			}
			return list;
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
		private void SaveClients()
		{
			if (Clients != null)
			{
				var xElement = new XElement("Clients");
				foreach (var client in Clients)
				{
					xElement.Add(new XElement("Client",
					new XAttribute("Id", client.Id),
					new XElement("ClientFIO", client.ClientFIO),
					new XElement("Email", client.Email),
					new XElement("Password", client.Password)));
				}
				XDocument xDocument = new XDocument(xElement);
				xDocument.Save(ClientFileName);
			}
		}
		private void SaveImplementers()
		{
			if (Implementers != null)
			{
				var xElement = new XElement("Implementers");
				foreach (var Implementer in Implementers)
				{
					xElement.Add(new XElement("Implementer",
					new XAttribute("Id", Implementer.Id),
					new XElement("ImplementerFIO", Implementer.ImplementerFIO),
					new XElement("WorkingTime", Implementer.WorkingTime),
					new XElement("PauseTime", Implementer.PauseTime)
					));
				}
				XDocument xDocument = new XDocument(xElement);
				xDocument.Save(ImplementerFileName);
			}
		}
		private void SaveMessages()
		{
			if (Messages != null)
			{
				var xElement = new XElement("Message");
				foreach (var message in Messages)
				{
					xElement.Add(new XElement("Message",
					new XAttribute("MessageId", message.MessageId),
					new XElement("Subject", message.Subject),
					new XElement("SenderName", message.SenderName),
					new XElement("Body", message.Body),
					new XElement("ClientId", message.ClientId),
					new XElement("DateDelivery", message.DateDelivery)));
				}
				XDocument xDocument = new XDocument(xElement);
				xDocument.Save(MessageFileName);
			}
		}
	}
}