using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Core.Server.Tools
{
    public class ErrorBehaviorAttribute : Attribute, IServiceBehavior
    {
        Type _handlerType;

        public ErrorBehaviorAttribute(Type handlerType)
        {
            _handlerType = handlerType;
        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
        {
            IErrorHandler errorHandler = (IErrorHandler)Activator.CreateInstance(_handlerType);

            foreach (ChannelDispatcherBase chanDisp in serviceHostBase.ChannelDispatchers)
            {
                ChannelDispatcher disp = chanDisp as ChannelDispatcher;
                disp.ErrorHandlers.Add(errorHandler);
            }
        }

        public void Validate(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
        {
        }
    }
}
