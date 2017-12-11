using Core.Common.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Server.Brokers.Organization
{
    public class CompanyBroker : ConsumerBroker
    {
        public Company Company { get; set; }

        private CompanyBroker()
        {

        }

        public CompanyBroker AddBranch(Branch branch)
        {
            Company.Branches.Add(branch);

            return this;
        }

        public CompanyBroker Create(Consumer consumer, params Branch[] branches)
        {
            Company = new Company()
            {
                Consumer = consumer,
                Branches = branches
            };

            return this;
        }

        public static CompanyBroker Init()
        {
            CompanyBroker ret = new CompanyBroker();

            return ret;
        }

        public CompanyBroker Load(string code)
        {
            Company = CoreEntities.Companies.FirstOrDefault(x => x.Consumer.Code == code);

            return this;
        }

        public CompanyBroker Load(Guid id)
        {
            Company = CoreEntities.Companies.FirstOrDefault(x => x.Id == id);

            return this;
        }
    }
}
