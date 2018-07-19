using Core.Log.Configuration;
using Core.Log.Exceptions;
using Core.Log.Faults;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.ServiceModel;

namespace Core.Log
{
    public static class Logger
    {
        private static TraceSource Source { get; set; }
        private static TraceSource UserWindowSource { get; set; }

        static Logger()
        {
            Source = new TraceSource(LogConfiguration.Configuration.Source);
            UserWindowSource = new TraceSource(LogConfiguration.Configuration.UserWindowSource);
        }

        private static void Log(this Exception exc)
        {
            if (exc != null)
            {
                Source.TraceData(TraceEventType.Error, 0, exc);
                UserWindowSource.TraceData(TraceEventType.Error, 0, exc);
            }
        }

        public static void Log(this LogThread thread)
        {
            if (thread != null)
            {
                Source.TraceData(TraceEventType.Error, 0, thread);
                UserWindowSource.TraceData(TraceEventType.Error, 0, thread);
            }
        }

        private static void Log(this DbEntityValidationException exc)
        {
            if (exc != null)
            {
                Source.TraceData(TraceEventType.Error, 0, exc);
                UserWindowSource.TraceData(TraceEventType.Error, 0, exc);

                foreach (var result in exc.EntityValidationErrors)
                {
                    foreach (var error in result.ValidationErrors)
                    {
                        Source.TraceData(TraceEventType.Error, 0, string.Format("{0}:{1}", error.PropertyName, error.ErrorMessage));
                        UserWindowSource.TraceData(TraceEventType.Error, 0, string.Format("{0}:{1}", error.PropertyName, error.ErrorMessage));
                    }
                }
            }
        }

        private static void Log(this FaultException<IBaseException> exc)
        {
            if (exc != null)
            {
                Source.TraceData(TraceEventType.Error, 0, exc.Detail);
                UserWindowSource.TraceData(TraceEventType.Error, 0, exc.Detail.ToString());
            }
        }

        public static void LogCritical(this string message, params object[] args)
        {
            if (message != null)
            {
                Source.TraceData(TraceEventType.Critical, 0, string.Format(message, args));
                UserWindowSource.TraceData(TraceEventType.Critical, 0, string.Format(message, args));
            }
        }

        public static void LogError(this string message, params object[] args)
        {
            if (message != null)
            {
                Source.TraceData(TraceEventType.Error, 0, string.Format(message, args));
                UserWindowSource.TraceData(TraceEventType.Error, 0, string.Format(message, args));
            }
        }

        public static void LogException(this BaseException exc)
        {
            exc.Log();
        }

        public static void LogException(this Exception exc)
        {
            exc.Log();
        }

        public static void LogException(this DbEntityValidationException exc)
        {
            exc.Log();
        }

        public static void LogFault(this FaultException<IBaseException> fault)
        {
            fault.Log();
        }

        public static void LogInformation(this string message, params object[] args)
        {
            if (message != null)
            {
                Source.TraceData(TraceEventType.Information, 0, string.Format(message, args));
                UserWindowSource.TraceData(TraceEventType.Information, 0, string.Format(message, args));
            }
        }
    }
}
