﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Configuration;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;

namespace TemplateDesignerModelTypes
{
    

    public class DesignerExceptionBehavior : BehaviorExtensionElement, IEndpointBehavior
    {
        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            SilverlightFaultMessageInspector inspector = new SilverlightFaultMessageInspector();
            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(inspector);
        }
        public class SilverlightFaultMessageInspector : IDispatchMessageInspector
        {
            public void BeforeSendReply(ref Message reply, object correlationState)
            {
                if (reply.IsFault)
                {
                    HttpResponseMessageProperty property = new HttpResponseMessageProperty();

                    // Here the response code is changed to 200.
                    property.StatusCode = System.Net.HttpStatusCode.OK;
                    reply.Properties[HttpResponseMessageProperty.Name] = property;
                }
            }
            public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
            {
                // Do nothing to the incoming message.
                return null;
            }
        }
        // The following methods are stubs and not relevant.
        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }
        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
        }
        public void Validate(ServiceEndpoint endpoint)
        {
        }
        public override System.Type BehaviorType
        {
            get { return typeof(DesignerExceptionBehavior); }
        }
        protected override object CreateBehavior()
        {
            return new DesignerExceptionBehavior();
        }
    }

}
