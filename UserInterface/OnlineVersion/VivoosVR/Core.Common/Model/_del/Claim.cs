using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Runtime.Serialization;

namespace Core.Common.Model
{
    [DataContract(IsReference = true)]
    [Table("Core.Claims")]
    public partial class Claim
    {
        public bool Available { get; set; }
        public Guid Id { get; set; }
        [Required]
        [StringLength(450)]
        public string Resource { get; set; }
        public virtual Role Role { get; set; }
        [Required]
        [StringLength(100)]
        public string RoleCode { get; set; }
    }
}
