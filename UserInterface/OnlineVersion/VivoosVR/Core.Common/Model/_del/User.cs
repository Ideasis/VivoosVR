using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Runtime.Serialization;

namespace Core.Common.Model
{
    [Table("Core.Users")]
    [DataContract(IsReference = true)]
    public partial class User : Consumer
    {
        [DataMember]
        public bool Available { get; set; }
        [DataMember]
        public DateTime? ExpireDate { get; set; }
        [DataMember]
        public virtual Group Group { get; set; }
        [DataMember]
        public Guid GroupId { get; set; }
        [DataMember]
        [Required]
        [StringLength(100)]
        public string Password { get; set; }
        [DataMember]
        [Column(TypeName = "image")]
        public byte[] Picture { get; set; }
        [DataMember]
        public virtual ICollection<RoleToUserMap> RoleToUserMaps { get; set; }

        public User()
        {
            RoleToUserMaps = new HashSet<RoleToUserMap>();
        }
    }
}
