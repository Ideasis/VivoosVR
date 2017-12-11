using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Runtime.Serialization;

namespace Core.Common.Model
{
    [DataContract(IsReference = true)]
    [Table("Core.Branches")]
    public partial class Branch : Consumer
    {
        public virtual Company Company { get; set; }
        public Guid CompanyId { get; set; }
        public virtual ICollection<Group> Groups { get; set; }

        public Branch()
        {
            Groups = new HashSet<Group>();
        }
    }
}
