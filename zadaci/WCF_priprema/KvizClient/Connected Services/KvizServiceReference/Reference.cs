﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KvizClient.KvizServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Pitanje", Namespace="http://schemas.datacontract.org/2004/07/Kviz")]
    [System.SerializableAttribute()]
    public partial class Pitanje : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string[] PonudjeniOdgovoriField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int RedniBrojTacnogOdgovoraField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TekstPitanjaField;
        
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
        public string[] PonudjeniOdgovori {
            get {
                return this.PonudjeniOdgovoriField;
            }
            set {
                if ((object.ReferenceEquals(this.PonudjeniOdgovoriField, value) != true)) {
                    this.PonudjeniOdgovoriField = value;
                    this.RaisePropertyChanged("PonudjeniOdgovori");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int RedniBrojTacnogOdgovora {
            get {
                return this.RedniBrojTacnogOdgovoraField;
            }
            set {
                if ((this.RedniBrojTacnogOdgovoraField.Equals(value) != true)) {
                    this.RedniBrojTacnogOdgovoraField = value;
                    this.RaisePropertyChanged("RedniBrojTacnogOdgovora");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string TekstPitanja {
            get {
                return this.TekstPitanjaField;
            }
            set {
                if ((object.ReferenceEquals(this.TekstPitanjaField, value) != true)) {
                    this.TekstPitanjaField = value;
                    this.RaisePropertyChanged("TekstPitanja");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="KvizServiceReference.IKvizService", SessionMode=System.ServiceModel.SessionMode.Required)]
    public interface IKvizService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IKvizService/DodajPitanje", ReplyAction="http://tempuri.org/IKvizService/DodajPitanjeResponse")]
        void DodajPitanje(KvizClient.KvizServiceReference.Pitanje pitanje);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IKvizService/DodajPitanje", ReplyAction="http://tempuri.org/IKvizService/DodajPitanjeResponse")]
        System.Threading.Tasks.Task DodajPitanjeAsync(KvizClient.KvizServiceReference.Pitanje pitanje);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IKvizService/VratiPitanja", ReplyAction="http://tempuri.org/IKvizService/VratiPitanjaResponse")]
        KvizClient.KvizServiceReference.Pitanje[] VratiPitanja();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IKvizService/VratiPitanja", ReplyAction="http://tempuri.org/IKvizService/VratiPitanjaResponse")]
        System.Threading.Tasks.Task<KvizClient.KvizServiceReference.Pitanje[]> VratiPitanjaAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IKvizService/IzmeniPitanje", ReplyAction="http://tempuri.org/IKvizService/IzmeniPitanjeResponse")]
        void IzmeniPitanje(int redniBroj, KvizClient.KvizServiceReference.Pitanje novoPitanje);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IKvizService/IzmeniPitanje", ReplyAction="http://tempuri.org/IKvizService/IzmeniPitanjeResponse")]
        System.Threading.Tasks.Task IzmeniPitanjeAsync(int redniBroj, KvizClient.KvizServiceReference.Pitanje novoPitanje);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IKvizService/EvaluirajRezultat", ReplyAction="http://tempuri.org/IKvizService/EvaluirajRezultatResponse")]
        double EvaluirajRezultat(int[] datiOdgovori);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IKvizService/EvaluirajRezultat", ReplyAction="http://tempuri.org/IKvizService/EvaluirajRezultatResponse")]
        System.Threading.Tasks.Task<double> EvaluirajRezultatAsync(int[] datiOdgovori);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IKvizServiceChannel : KvizClient.KvizServiceReference.IKvizService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class KvizServiceClient : System.ServiceModel.ClientBase<KvizClient.KvizServiceReference.IKvizService>, KvizClient.KvizServiceReference.IKvizService {
        
        public KvizServiceClient() {
        }
        
        public KvizServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public KvizServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public KvizServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public KvizServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void DodajPitanje(KvizClient.KvizServiceReference.Pitanje pitanje) {
            base.Channel.DodajPitanje(pitanje);
        }
        
        public System.Threading.Tasks.Task DodajPitanjeAsync(KvizClient.KvizServiceReference.Pitanje pitanje) {
            return base.Channel.DodajPitanjeAsync(pitanje);
        }
        
        public KvizClient.KvizServiceReference.Pitanje[] VratiPitanja() {
            return base.Channel.VratiPitanja();
        }
        
        public System.Threading.Tasks.Task<KvizClient.KvizServiceReference.Pitanje[]> VratiPitanjaAsync() {
            return base.Channel.VratiPitanjaAsync();
        }
        
        public void IzmeniPitanje(int redniBroj, KvizClient.KvizServiceReference.Pitanje novoPitanje) {
            base.Channel.IzmeniPitanje(redniBroj, novoPitanje);
        }
        
        public System.Threading.Tasks.Task IzmeniPitanjeAsync(int redniBroj, KvizClient.KvizServiceReference.Pitanje novoPitanje) {
            return base.Channel.IzmeniPitanjeAsync(redniBroj, novoPitanje);
        }
        
        public double EvaluirajRezultat(int[] datiOdgovori) {
            return base.Channel.EvaluirajRezultat(datiOdgovori);
        }
        
        public System.Threading.Tasks.Task<double> EvaluirajRezultatAsync(int[] datiOdgovori) {
            return base.Channel.EvaluirajRezultatAsync(datiOdgovori);
        }
    }
}
