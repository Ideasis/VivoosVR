using Core.Log;
using Core.Log.Configuration;
using Core.Log.Exceptions;
using Core.Log.Faults;
using System;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Core.Server.Tools
{
    public class CoreErrorHandler : IErrorHandler
    {
        public bool HandleError(Exception error)
        {
            return false;
        }

        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            CoreFault faultDetail = null;
            OperationDescription operation = null;

            //Find fault contracts for current operation.
            OperationContext operationContext = OperationContext.Current;
            if (operationContext == null)
            {
                error.LogException();

                faultDetail = (new CoreException(error.ToString())).GetFault();

            }
            else
            {

                ServiceDescription hostDescription = operationContext.Host.Description;
                //ServiceEndpoint endpoint = hostDescription.Endpoints.Find(operationContext.IncomingMessageHeaders.To);
                ServiceEndpoint endpoint = operationContext.Host.Description.Endpoints.Find(operationContext.EndpointDispatcher.EndpointAddress.Uri);
                string action = operationContext.IncomingMessageHeaders.Action;

                //operation = (from i in endpoint.Contract.Operations
                //             where i.DeclaringContract.Namespace + "/" + i.DeclaringContract.Name + "/" + i.Name == action
                //             select i).FirstOrDefault();

                DispatchOperation dispatchOperation = operationContext.EndpointDispatcher.DispatchRuntime.Operations.FirstOrDefault(op => op.Action == operationContext.IncomingMessageHeaders.Action);
                operation = endpoint.Contract.Operations.Find(dispatchOperation.Name);
            }

            if (error is ApplicationException)
            {
                ApplicationException applicationException = error as ApplicationException;
                applicationException.LogException();

                faultDetail = new CoreFault(applicationException, applicationException.Message);

                CreateFault(operation, faultDetail, version, ref fault);
                return;
            }

            //if (error is UserException)
            //{
            //    UserException userException = error as UserException;
            //    userException.LogException();

            //    faultDetail = userException.GetFault();

            //    CreateFault(operation, faultDetail, version, ref fault);
            //    return;
            //}

            if (error is CoreException)
            {
                CoreException coreException = error as CoreException;
                coreException.LogException();

                faultDetail = coreException.GetFault();

                CreateFault(operation, faultDetail, version, ref fault);
                return;
            }

            if (error is DataException)
            {
                DataException dee = error as DataException;
                dee.LogException();

                faultDetail = (new CoreException("Kayıt işlemi sırasında bir hata oluştu.")).GetFault();

                CreateFault(operation, faultDetail, version, ref fault);
                return;
            }


            error.LogException();

            faultDetail = (new CoreException(LogConfiguration.Configuration.SystemExceptionMessage)).GetFault();
            CreateFault(operation, faultDetail, version, ref fault);
            return;

        }

        private void CreateFault(OperationDescription operation, CoreFault faultDetail, MessageVersion version, ref Message fault)
        {
            if (operation != null && faultDetail != null)
            {
                foreach (FaultDescription faultDescription in operation.Faults)
                {
                    if (faultDescription.DetailType.IsAssignableFrom(faultDetail.GetType()))
                    {
                        fault = Message.CreateMessage(
                            version,
                            FaultCode.CreateSenderFaultCode(faultDescription.Name, faultDescription.Namespace),
                            faultDetail.Message,
                            faultDetail,
                            faultDescription.Action);
                        break;
                    }
                }
            }
        }
    }
}
