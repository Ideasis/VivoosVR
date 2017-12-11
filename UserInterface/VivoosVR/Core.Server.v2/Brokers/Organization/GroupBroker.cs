using Core.Common.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Server.Brokers.Organization
{
    public class GroupBroker : ConsumerBroker
    {
        public Group Group { get; set; }
        public List<Group> Groups { get; set; }
        private GroupBroker()
        {

        }

        public GroupBroker AddUser(User user)
        {
            Group.Users.Add(user);

            return this;
        }

        public GroupBroker Create(Consumer consumer, Branch branch)
        {
            Group = new Group()
            {
                Id = Guid.NewGuid(),
                Branch = branch,
                Consumer = consumer
            };

            return this;
        }

        public static GroupBroker Init()
        {
            GroupBroker ret = new GroupBroker();

            return ret;
        }

        public GroupBroker Load(string code)
        {
            Group = CoreEntities.Groups.FirstOrDefault(x => x.Consumer.Code == code);

            return this;
        }

        public GroupBroker Load(Guid id)
        {
            Group = CoreEntities.Groups.FirstOrDefault(x => x.Id == id);

            return this;
        }

        public GroupBroker LoadAll(Guid? branchId)
        {
            Groups = CoreEntities.Groups.Where(x => (x.BranchId == branchId && branchId != null) || (branchId == null)).ToList();

            return this;
        }

        public GroupBroker RunBranchAction(Action<BranchBroker> action)
        {
            BranchBroker branchBroker = BranchBroker.Init()
                .Load(Group.BranchId);

            action.Invoke(branchBroker);

            return this;
        }

        public GroupBroker SetBranch(Branch branch)
        {
            Group.Branch = branch;

            return this;
        }

        public GroupBroker SetBranch(Guid branchId)
        {
            Group.BranchId = branchId;

            return this;
        }
    }
}
