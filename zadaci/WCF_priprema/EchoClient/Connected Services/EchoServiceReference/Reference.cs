﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EchoClient.EchoServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CompositeType", Namespace="http://schemas.datacontract.org/2004/07/Echo")]
    [System.SerializableAttribute()]
    public partial class CompositeType : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool BoolValueField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StringValueField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool BoolValue {
            get {
                return this.BoolValueField;
            }
            set {
                if ((this.BoolValueField.Equals(value) != true)) {
                    this.BoolValueField = value;
                    this.RaisePropertyChanged("BoolValue");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string StringValue {
            get {
                return this.StringValueField;
            }
            set {
                if ((object.ReferenceEquals(this.StringValueField, value) != true)) {
                    this.StringValueField = value;
                    this.RaisePropertyChanged("StringValue");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="EchoServiceReference.IEchoService")]
    public interface IEchoService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEchoService/Echo", ReplyAction="http://tempuri.org/IEchoService/EchoResponse")]
        string Echo(string value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEchoService/Echo", ReplyAction="http://tempuri.org/IEchoService/EchoResponse")]
        System.Threading.Tasks.Task<string> EchoAsync(string value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEchoService/GetCompositeData", ReplyAction="http://tempuri.org/IEchoService/GetCompositeDataResponse")]
        EchoClient.EchoServiceReference.CompositeType GetCompositeData(EchoClient.EchoServiceReference.CompositeType composite);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEchoService/GetCompositeData", ReplyAction="http://tempuri.org/IEchoService/GetCompositeDataResponse")]
        System.Threading.Tasks.Task<EchoClient.EchoServiceReference.CompositeType> GetCompositeDataAsync(EchoClient.EchoServiceReference.CompositeType composite);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IEchoServiceChannel : EchoClient.EchoServiceReference.IEchoService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class EchoServiceClient : System.ServiceModel.ClientBase<EchoClient.EchoServiceReference.IEchoService>, EchoClient.EchoServiceReference.IEchoService {
        
        public EchoServiceClient() {
        }
        
        public EchoServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public EchoServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public EchoServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public EchoServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string Echo(string value) {
            return base.Channel.Echo(value);
        }
        
        public System.Threading.Tasks.Task<string> EchoAsync(string value) {
            return base.Channel.EchoAsync(value);
        }
        
        public EchoClient.EchoServiceReference.CompositeType GetCompositeData(EchoClient.EchoServiceReference.CompositeType composite) {
            return base.Channel.GetCompositeData(composite);
        }
        
        public System.Threading.Tasks.Task<EchoClient.EchoServiceReference.CompositeType> GetCompositeDataAsync(EchoClient.EchoServiceReference.CompositeType composite) {
            return base.Channel.GetCompositeDataAsync(composite);
        }
    }
}
