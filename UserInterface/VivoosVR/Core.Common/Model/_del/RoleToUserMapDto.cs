using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common.Model
{
    public class RoleToUserMapDto : BaseDto
    {
        private Guid _Id;
        private Guid _UserId;
        private string _RoleCode;
        private DateTime? _ValidUntil;
        private bool _Available;
        private string _RoleDescription;

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
        [DisplayName("Rol Kodu")]
        [DataMember]
        public string RoleCode
        {
            get { return _RoleCode; }
            set
            {
                _RoleCode = value;
                RaisePropertyChanged("RoleCode");
            }
        }
        [DisplayName("Rol Tanım")]
        [DataMember]
        public string RoleDescription
        {
            get { return _RoleDescription; }
            set
            {
                _RoleDescription = value;
                RaisePropertyChanged("RoleDescription");
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
        [DisplayName("Kullanıcı")]
        [DataMember]
        public Guid UserId
        {
            get { return _UserId; }
            set
            {
                _UserId = value;
                RaisePropertyChanged("UserId");
            }
        }
        [DisplayName("Son Kullanma Tarihi")]
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

        public override void Reset()
        {

        }
    }
}
