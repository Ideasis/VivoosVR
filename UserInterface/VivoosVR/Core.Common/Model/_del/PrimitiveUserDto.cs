using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common.Model
{
    public class PrimitiveUserDto : BaseDto
    {
        private Guid _Id;
        private string _Code;
        private string _Description;
        private string _Email;
        private Guid? _GroupId;
        private DateTime? _ExpireDate;
        private byte[] _Picture;
        private bool _Available;
        private BindingList<RoleToUserMapDto> _RoleMaps;
        private string _WindowsCode;

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
        [DisplayName("E-posta")]
        [DataMember]
        public string Email
        {
            get { return _Email; }
            set
            {
                _Email = value;
                RaisePropertyChanged("Email");
            }
        }
        public override string Error
        {
            get { return null; }
        }
        [DisplayName("Son Kullanma Tarihi")]
        [DataMember]
        public DateTime? ExpireDate
        {
            get { return _ExpireDate; }
            set
            {
                _ExpireDate = value;
                RaisePropertyChanged("ExpireDate");
            }
        }
        [DisplayName("Grup")]
        [DataMember]
        public Guid? GroupId
        {
            get { return _GroupId; }
            set
            {
                _GroupId = value;
                RaisePropertyChanged("GroupId");
            }
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
        [DisplayName("Resim")]
        [DataMember]
        public byte[] Picture
        {
            get { return _Picture; }
            set
            {
                _Picture = value;
                RaisePropertyChanged("Picture");
            }
        }
        [DisplayName("Roller")]
        [DataMember]
        public BindingList<RoleToUserMapDto> RoleMaps
        {
            get { return _RoleMaps; }
            set
            {
                _RoleMaps = value;
                RaisePropertyChanged("RoleMaps");
            }
        }
        public override string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "Code":
                        if (string.IsNullOrWhiteSpace(Code))
                        {
                            return "Kod alanı boş bırakılamaz.";
                        }
                        break;
                    case "Description":
                        if (string.IsNullOrWhiteSpace(Description))
                        {
                            return "Tanım alanı boş bırakılamaz.";
                        }
                        break;
                    case "Email":
                        if (string.IsNullOrWhiteSpace(Email))
                        {
                            return "E-posta alanı boş bırakılamaz.";
                        }
                        break;
                    case "GroupId":
                        if (GroupId == null)
                        {
                            return "Grup alanı boş bırakılamaz.";
                        }
                        break;
                    default:
                        break;
                }

                return null;
            }
        }
        [DisplayName("Kullanımda")]
        [DataMember]
        public string WindowsCode
        {
            get { return _WindowsCode; }
            set
            {
                _WindowsCode = value;
                RaisePropertyChanged("WindowsCode");
            }
        }

        public PrimitiveUserDto()
        {
            RoleMaps = new BindingList<RoleToUserMapDto>();
        }

        public override void Reset()
        {
            Code = null;
            Description = null;
            Email = null;
            GroupId = null;
            ExpireDate = null;
            Picture = null;
            Available = true;

            RoleMaps = new BindingList<RoleToUserMapDto>();
        }
    }
}
