using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Runtime.Serialization;

namespace Core.Common.Model
{
    [DataContract(IsReference = true)]
    [Table("Core.RoleToUserMaps")]
    public partial class RoleToUserMap
    {
        public bool Available { get; set; }
        public DateTime EntryDate { get; set; }
        public Guid Id { get; set; }
        public virtual Role Role { get; set; }
        [Required]
        [StringLength(100)]
        public string RoleCode { get; set; }
        public virtual User User { get; set; }
        public Guid UserId { get; set; }
        public DateTime? ValidUntil { get; set; }
    }
}
