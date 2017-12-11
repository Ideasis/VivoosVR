using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Runtime.Serialization;

namespace Core.Common.Model
{
    [DataContract(IsReference = true)]
    [Table("Core.Roles")]
    public partial class Role
    {
        public bool Available { get; set; }
        public virtual ICollection<Claim> Claims { get; set; }
        [Key]
        [StringLength(100)]
        public string Code { get; set; }
        [Required]
        [StringLength(500)]
        public string Description { get; set; }
        public virtual ICollection<RoleToUserMap> RoleToUserMaps { get; set; }

        public Role()
        {
            Claims = new HashSet<Claim>();
            RoleToUserMaps = new HashSet<RoleToUserMap>();
        }
    }
}
