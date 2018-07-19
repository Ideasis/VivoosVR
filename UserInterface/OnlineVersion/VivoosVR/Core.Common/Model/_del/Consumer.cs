using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Runtime.Serialization;

namespace Core.Common.Model
{
    [Table("Core.Consumers")]
    [DataContract(IsReference = true)]
    public partial class Consumer
    {
        [DataMember]
        [Required]
        [StringLength(200)]
        public string Code { get; set; }
        [DataMember]
        public ICollection<ConsumerProperty> ConsumerProperties { get; set; }
        [DataMember]
        [Required]
        public string Description { get; set; }
        [DataMember]
        [Required]
        [StringLength(200)]
        public string Email { get; set; }
        [DataMember]
        public DateTime EntryDate { get; set; }
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public string WindowsCode { get; set; }

        public Consumer()
        {
            ConsumerProperties = new HashSet<ConsumerProperty>();
        }
    }
}
