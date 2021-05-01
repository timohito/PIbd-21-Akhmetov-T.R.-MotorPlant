using MotorPlantBusinessLogic.BindingModels;
using MotorPlantBusinessLogic.Interfaces;
using MotorPlantBusinessLogic.ViewModels;
using MotorPlantDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MotorPlantDatabaseImplement.Implements
{
    public class ClientStorage : IClientStorage
    {
        public List<ClientViewModel> GetFullList()
        {
            using (var context = new MotorPlantDatabase())
            {
                return context.Clients
                .Select(CreateModel).ToList();
            }
        }

        public List<ClientViewModel> GetFilteredList(ClientBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new MotorPlantDatabase())
            {
                return context.Clients
                    .Where(rec =>
                    rec.ClientFIO.Contains(model.ClientFIO) || (rec.Email.Equals(model.Email) && rec.Password.Equals(model.Password)))
                    .Select(CreateModel).ToList();
            }
        }

        public ClientViewModel GetElement(ClientBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new MotorPlantDatabase())
            {
                var client = context.Clients
                .FirstOrDefault(rec => rec.Email.Equals(model.Email) || rec.Id == model.Id);
                return client != null ?
                CreateModel(client) : null;
            }
        }

        public void Insert(ClientBindingModel model)
        {
            using (var context = new MotorPlantDatabase())
            {
                context.Clients.Add(CreateModel(model, new Client()));
                context.SaveChanges();
            }
        }

        public void Update(ClientBindingModel model)
        {
            using (var context = new MotorPlantDatabase())
            {
                var element = context.Clients.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Client not found");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }

        public void Delete(ClientBindingModel model)
        {
            using (var context = new MotorPlantDatabase())
            {
                Client element = context.Clients.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Clients.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Client not found");
                }
            }
        }

        private Client CreateModel(ClientBindingModel model, Client client)
        {
            client.ClientFIO = model.ClientFIO;
            client.Email = model.Email;
            client.Password = model.Password;
            return client;
        }

        private ClientViewModel CreateModel(Client client)
        {
            return new ClientViewModel
            {
                Id = client.Id,
                ClientFIO = client.ClientFIO,
                Email = client.Email,
                Password = client.Password
            };
        }
    }
}
