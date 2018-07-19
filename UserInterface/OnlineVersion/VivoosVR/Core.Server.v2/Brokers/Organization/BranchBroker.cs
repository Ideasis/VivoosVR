using Core.Common.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Server.Brokers.Organization
{
    public class BranchBroker : ConsumerBroker
    {
        public Branch Branch { get; set; }

        private BranchBroker()
        {

        }

        public BranchBroker AddGroup(Group group)
        {
            Branch.Groups.Add(group);

            return this;
        }

        public BranchBroker Create(Company company, Consumer consumer, params Group[] groups)
        {
            Branch = new Branch()
            {
                Id = consumer.Id,
                Company = company,
                Consumer = consumer,
                Groups = groups
            };

            return this;
        }

        public static BranchBroker Init()
        {
            BranchBroker ret = new BranchBroker();

            return ret;
        }

        public BranchBroker Load(string code)
        {
            Branch = CoreEntities.Branches.FirstOrDefault(x => x.Consumer.Code == code);

            return this;
        }

        public BranchBroker Load(Guid id)
        {
            Branch = CoreEntities.Branches.FirstOrDefault(x => x.Id == id);

            return this;
        }

        public BranchBroker RunCompanyAction(Action<CompanyBroker> action)
        {
            CompanyBroker companyBroker = CompanyBroker.Init()
                .Load(Branch.CompanyId);

            action.Invoke(companyBroker);

            return this;
        }

        public BranchBroker SetCompany(Company company)
        {
            Branch.Company = company;

            return this;
        }

        public BranchBroker SetCompany(Guid companyId)
        {
            Branch.CompanyId = companyId;

            return this;
        }
    }
}
