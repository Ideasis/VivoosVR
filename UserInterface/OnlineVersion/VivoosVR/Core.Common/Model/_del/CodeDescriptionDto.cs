using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common.Model
{
    public class CodeDescriptionDto : BaseDto
    {
        private string _Code;
        private string _Description;

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
        [DisplayName("Tanım")]
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
        }
    }
}
