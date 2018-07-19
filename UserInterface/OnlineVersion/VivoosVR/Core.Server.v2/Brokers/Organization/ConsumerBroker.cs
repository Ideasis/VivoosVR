using Core.Common.DataModel.Core;
using Core.Log.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Server.Brokers.Organization
{
    public class ConsumerBroker : CoreBaseBroker
    {
        protected Consumer Consumer { get; set; }

        public ConsumerBroker AddConsumerProperty(int typeNo, string value)
        {
            ConsumerProperty consumerProperty = new ConsumerProperty()
            {
                Available = true,
                Id = Guid.NewGuid(),
                EntryDate = DateTime.Now,
                Consumer = this.Consumer,
                ConsumerId = this.Consumer.Id,
                Value = value,
                TypeNo = typeNo
            };

            Consumer.ConsumerProperties.Add(consumerProperty);

            return this;
        }

        public ConsumerBroker AddConsumerPropertyType(int no, string name)
        {
            if (CoreEntities.ConsumerPropertyTypes.Any(x => x.No == no))
            {
                throw new ApplicationException($"{no} olarak verilen özellik türü sistemde zaten var.");
            }

            ConsumerPropertyType consumerPropertyType = new ConsumerPropertyType()
            {
                Name = name,
                No = no
            };

            CoreEntities.ConsumerPropertyTypes.Add(consumerPropertyType);

            return this;
        }

        public ConsumerBroker RemoveConsumerProperties(int typeNo)
        {
            IEnumerable<ConsumerProperty> properties = CoreEntities.ConsumerProperties.Where(x => x.TypeNo == typeNo);

            properties.ToList().ForEach(x => CoreEntities.ConsumerProperties.Remove(x));

            return this;
        }

        public ConsumerBroker RemoveConsumerPropertyType(int no)
        {
            ConsumerPropertyType consumerPropertyType = CoreEntities.ConsumerPropertyTypes.FirstOrDefault(x => x.No == no);

            if (consumerPropertyType == null)
            {
                throw new ApplicationException("{0} Numaralı istemci özelliği sistemde bulunamadı.");
            }

            CoreEntities.ConsumerPropertyTypes.Remove(consumerPropertyType);

            return this;
        }

        public new ConsumerBroker Save()
        {
            base.Save();

            return this;
        }

        public ConsumerBroker SetCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code)) throw new ApplicationException("Kod boş olarak ayarlanamaz.");
            if (CoreEntities.Consumers.Any(x => x.Code == code)) throw new ApplicationException("Bu kod sistemde zaten tanımlı.");
            Consumer.Code = code;

            return this;
        }

        public ConsumerBroker SetDescription(string description)
        {
            Consumer.Description = description;

            return this;
        }

        public ConsumerBroker SetEmail(string email)
        {
            Consumer.Email = email;

            return this;
        }
    }
}
