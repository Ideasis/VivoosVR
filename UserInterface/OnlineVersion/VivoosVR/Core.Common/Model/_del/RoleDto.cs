using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common.Model
{
    public class RoleDto : BaseDto
    {
        private string _Code;
        private string _Description;
        private DateTime? _ValidUntil;
        private bool _Available;

        [DisplayName("Kullanımda")]
        [DataMember]
        public bool Available
        {
            get { return _Available; }
            set
            {
                _Available = value;
                RaisePropertyChanged("Available");
            }
        }
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
            get { return "abc"; }
        }
        public override string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "Code":
                        {
                            if (string.IsNullOrWhiteSpace(Code))
                            {
                                return "Kod alanı boş bırakılamaz";
                            }
                        }
                        break;
                    case "Description":
                        {
                            if (string.IsNullOrWhiteSpace(Description))
                            {
                                return "Tanım alanı boş bırakılamaz";
                            }
                        }
                        break;
                    default:
                        break;
                }

                return null;
            }
        }
        [DisplayName("Son Kullanım Tarihi")]
        [DataMember]
        public DateTime? ValidUntil
        {
            get { return _ValidUntil; }
            set
            {
                _ValidUntil = value;
                RaisePropertyChanged("ValidUntil");
            }
        }

        public RoleDto()
        {
            //Reset();
        }

        public override void Reset()
        {
            Code = null;
            Description = null;
        }
    }
}
