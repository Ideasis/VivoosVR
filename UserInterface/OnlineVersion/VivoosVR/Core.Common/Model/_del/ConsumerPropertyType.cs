using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Runtime.Serialization;

namespace Core.Common.Model
{
    [DataContract(IsReference = true)]
    [Table("Parameter.ConsumerPropertyTypes")]
    public partial class ConsumerPropertyType
    {
        public virtual ICollection<ConsumerProperty> ConsumerProperties { get; set; }
        [StringLength(200)]
        public string Name { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int No { get; set; }

        public ConsumerPropertyType()
        {
            ConsumerProperties = new HashSet<ConsumerProperty>();
        }
    }
}
