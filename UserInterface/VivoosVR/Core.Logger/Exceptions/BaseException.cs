using Core.Log.Faults;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace Core.Log.Exceptions
{
    [Serializable]
    public abstract class BaseException : Exception, ISerializable
    {


        public string Code
        {
            get
            {
                if (Data.Contains("Code")) return Data["Code"].ToString();

                return null;
            }
            set
            {
                Data["Code"] = value;
            }
        }
        public string UserMessage { get; set; }

        public BaseException(string message, params object[] args) : base(string.Format(message, args))
        {
        }

        public BaseException(string code, string message, params object[] args) : base(string.Format(message, args))
        {
            Code = code;
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
