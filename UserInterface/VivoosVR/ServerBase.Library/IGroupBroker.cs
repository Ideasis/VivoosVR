using System;
using System.Collections.Generic;
using Core.Common.Model;

namespace Core.Server.v2.Brokers.Organization
{
    public interface IGroupBroker
    {
        Group Group { get; set; }
        List<Group> Groups { get; set; }

        GroupBroker AddUser(User user);
        GroupBroker Create(string code, string description, string email, Guid branchId, byte[] picture);
        GroupBroker Load(string code);
        GroupBroker Load(Guid id);
        GroupBroker LoadAll(Guid? branchId);
        GroupBroker RunBranchAction(Action<BranchBroker> action);
        GroupBroker SetBranch(Guid branchId);
        GroupBroker SetBranch(Branch branch);
    }
}