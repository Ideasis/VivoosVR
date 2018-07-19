using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common.Model
{
    public class GroupDto : BaseDto
    {
        private Guid _Id;
        private string _Description;
        private string _Code;

        [DisplayName("Kod")]
        [DataMember]
        public string Code
        {
            get { return _Code; }
            set
            {
                _Code = value;
                RaisePropertyChanged("Code");
            }
        }
        [DisplayName("Açıklama")]
        [DataMember]
        public string Description
        {
            get { return _Description; }
            set
            {
                _Description = value;
                RaisePropertyChanged("Description");
            }
        }
        public override string Error
        {
            get { return null; }
        }
        [DisplayName("Id")]
        [DataMember]
        public Guid Id
        {
            get { return _Id; }
            set
            {
                _Id = value;
                RaisePropertyChanged("Id");
            }
        }
        public override string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    default:
                        break;
                }
                return null;
            }
        }

        public override void Reset()
        {
            Code = null;
            Description = null;
            Id = Guid.Empty;
        }
    }
}
