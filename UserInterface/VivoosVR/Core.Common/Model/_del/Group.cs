using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Runtime.Serialization;

namespace Core.Common.Model
{
    [DataContract(IsReference = true)]
    [Table("Core.Groups")]
    public partial class Group : Consumer
    {
        public virtual Branch Branch { get; set; }
        public Guid BranchId { get; set; }
        public virtual ICollection<User> Users { get; set; }

        public Group()
        {
            Users = new HashSet<User>();
        }
    }
}
