using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Runtime.Serialization;

namespace Core.Common.Model
{
    [DataContract(IsReference = true)]
    [Table("Core.Companies")]
    public partial class Company : Consumer
    {
        public virtual ICollection<Branch> Branches { get; set; }
        [Column(TypeName = "image")]
        public byte[] Logo { get; set; }

        public Company()
        {
            Branches = new HashSet<Branch>();
        }
    }
}
