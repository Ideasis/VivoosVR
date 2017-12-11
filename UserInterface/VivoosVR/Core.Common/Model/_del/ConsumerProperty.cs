using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Runtime.Serialization;

namespace Core.Common.Model
{
    [DataContract(IsReference = true)]
    [Table("Core.ConsumerProperties")]
    public partial class ConsumerProperty
    {
        public bool Available { get; set; }
        public virtual Consumer Consumer { get; set; }
        public Guid ConsumerId { get; set; }
        public virtual ConsumerPropertyType ConsumerPropertyType { get; set; }
        public DateTime EntryDate { get; set; }
        public Guid Id { get; set; }
        public int TypeNo { get; set; }
        [Required]
        public string Value { get; set; }
    }
}
