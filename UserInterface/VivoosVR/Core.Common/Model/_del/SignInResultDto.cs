using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common.Model
{
    public class SignInResultDto : BaseDto
    {
        private Guid _Id;
        private string _Code;
        private string _Password;
        private string _Description;
        private string _Email;
        private byte[] _Picture;
        private string _GroupCode;
        private string _GroupDescription;
        private BindingList<string> _Claims;
        private Dictionary<string, string> _Roles;

        [DisplayName("Yetkili Kaynaklar")]
        [DataMember]
        public BindingList<string> Claims
        {
            get { return _Claims; }
            set
            {
                _Claims = value;
                RaisePropertyChanged("Claims");
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
        [DisplayName("Grup Kodu")]
        [DataMember]
        public string GroupCode
        {
            get { return _GroupCode; }
            set
            {
                _GroupCode = value;
                RaisePropertyChanged("GroupCode");
            }
        }
        [DisplayName("Grup Açıklama")]
        [DataMember]
        public string GroupDescription
        {
            get { return _GroupDescription; }
            set
            {
                _GroupDescription = value;
                RaisePropertyChanged("GroupDescription");
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
        [DisplayName("Şifre")]
        [DataMember]
        public string Password
        {
            get { return _Password; }
            set
            {
                _Password = value;
                RaisePropertyChanged("Password");
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
        public Dictionary<string, string> Roles
        {
            get { return _Roles; }
            set
            {
                _Roles = value;
                RaisePropertyChanged("Roles");
            }
        }
        public override string this[string columnName]
        {
            get
            {
                //TODO: Column validation eklenecek.
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
            this.Claims = null;
            this.Code = null;
            this.Description = null;
            this.Email = null;
            this.GroupCode = null;
            this.GroupDescription = null;
            this.Id = Guid.Empty;
            this.Password = null;
            this.Picture = null;
            this.Roles = null;
        }
    }
}
