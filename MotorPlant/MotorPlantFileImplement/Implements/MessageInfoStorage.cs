using MotorPlantBusinessLogic.BindingModels;
using MotorPlantBusinessLogic.ViewModels;
using MotorPlantFileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MotorPlantFileImplement.Implements
{
    public class MessageInfoStorage
    {
        private readonly FileDataListSingleton source;

        public MessageInfoStorage()
        {
            source = FileDataListSingleton.GetInstance();
        }

        public List<MessageInfoViewModel> GetFullList()
        {
            return source.Messages
                .Select(rec => new MessageInfoViewModel
                {
                    MessageId = rec.MessageId,
                    SenderName = rec.SenderName,
                    DateDelivery = rec.DateDelivery,
                    Subject = rec.Subject,
                    Body = rec.Body
                })
                .ToList();
        }

        public List<MessageInfoViewModel> GetFilteredList(MessageInfoBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return source.Messages
                .Where(rec => (model.ClientId.HasValue && rec.ClientId ==
                model.ClientId) ||
                (!model.ClientId.HasValue && rec.DateDelivery.Date ==
                model.DateDelivery.Date))
                .Select(rec => new MessageInfoViewModel
                {
                    MessageId = rec.MessageId,
                    SenderName = rec.SenderName,
                    DateDelivery = rec.DateDelivery,
                    Subject = rec.Subject,
                    Body = rec.Body
                })
                .ToList();
        }

        public void Insert(MessageInfoBindingModel model)
        {
            MessageInfo element = source.Messages.FirstOrDefault(rec =>
                rec.MessageId == model.MessageId);
            if (element != null)
            {
                throw new Exception("Уже есть письмо с таким идентификатором");
            }
            source.Messages.Add(new MessageInfo
            {
                MessageId = model.MessageId,
                ClientId = model.ClientId,
                SenderName = model.FromMailAddress,
                DateDelivery = model.DateDelivery,
                Subject = model.Subject,
                Body = model.Body
            });

        }
    }
}
