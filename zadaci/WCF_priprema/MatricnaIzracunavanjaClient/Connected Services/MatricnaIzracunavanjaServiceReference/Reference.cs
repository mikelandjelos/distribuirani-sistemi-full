﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MatricnaIzracunavanjaClient.MatricnaIzracunavanjaServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Matrica", Namespace="http://schemas.datacontract.org/2004/07/MatricnaIzracunavanja")]
    [System.SerializableAttribute()]
    public partial class Matrica : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int BrojKolonaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int BrojVrstaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int[] ElementiField;
        
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
        public int BrojKolona {
            get {
                return this.BrojKolonaField;
            }
            set {
                if ((this.BrojKolonaField.Equals(value) != true)) {
                    this.BrojKolonaField = value;
                    this.RaisePropertyChanged("BrojKolona");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int BrojVrsta {
            get {
                return this.BrojVrstaField;
            }
            set {
                if ((this.BrojVrstaField.Equals(value) != true)) {
                    this.BrojVrstaField = value;
                    this.RaisePropertyChanged("BrojVrsta");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int[] Elementi {
            get {
                return this.ElementiField;
            }
            set {
                if ((object.ReferenceEquals(this.ElementiField, value) != true)) {
                    this.ElementiField = value;
                    this.RaisePropertyChanged("Elementi");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Rezultat", Namespace="http://schemas.datacontract.org/2004/07/MatricnaIzracunavanja")]
    [System.SerializableAttribute()]
    public partial class Rezultat : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private MatricnaIzracunavanjaClient.MatricnaIzracunavanjaServiceReference.Matrica MatricaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PorukaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool UspehField;
        
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
        public MatricnaIzracunavanjaClient.MatricnaIzracunavanjaServiceReference.Matrica Matrica {
            get {
                return this.MatricaField;
            }
            set {
                if ((object.ReferenceEquals(this.MatricaField, value) != true)) {
                    this.MatricaField = value;
                    this.RaisePropertyChanged("Matrica");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Poruka {
            get {
                return this.PorukaField;
            }
            set {
                if ((object.ReferenceEquals(this.PorukaField, value) != true)) {
                    this.PorukaField = value;
                    this.RaisePropertyChanged("Poruka");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Uspeh {
            get {
                return this.UspehField;
            }
            set {
                if ((this.UspehField.Equals(value) != true)) {
                    this.UspehField = value;
                    this.RaisePropertyChanged("Uspeh");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="MatricnaIzracunavanjaServiceReference.IMatricnaIzracunavanjaService")]
    public interface IMatricnaIzracunavanjaService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMatricnaIzracunavanjaService/Postavi", ReplyAction="http://tempuri.org/IMatricnaIzracunavanjaService/PostaviResponse")]
        MatricnaIzracunavanjaClient.MatricnaIzracunavanjaServiceReference.Rezultat Postavi(MatricnaIzracunavanjaClient.MatricnaIzracunavanjaServiceReference.Matrica matrica);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMatricnaIzracunavanjaService/Postavi", ReplyAction="http://tempuri.org/IMatricnaIzracunavanjaService/PostaviResponse")]
        System.Threading.Tasks.Task<MatricnaIzracunavanjaClient.MatricnaIzracunavanjaServiceReference.Rezultat> PostaviAsync(MatricnaIzracunavanjaClient.MatricnaIzracunavanjaServiceReference.Matrica matrica);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMatricnaIzracunavanjaService/PreuzmiMatricu", ReplyAction="http://tempuri.org/IMatricnaIzracunavanjaService/PreuzmiMatricuResponse")]
        MatricnaIzracunavanjaClient.MatricnaIzracunavanjaServiceReference.Rezultat PreuzmiMatricu();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMatricnaIzracunavanjaService/PreuzmiMatricu", ReplyAction="http://tempuri.org/IMatricnaIzracunavanjaService/PreuzmiMatricuResponse")]
        System.Threading.Tasks.Task<MatricnaIzracunavanjaClient.MatricnaIzracunavanjaServiceReference.Rezultat> PreuzmiMatricuAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMatricnaIzracunavanjaService/Sabiranje", ReplyAction="http://tempuri.org/IMatricnaIzracunavanjaService/SabiranjeResponse")]
        MatricnaIzracunavanjaClient.MatricnaIzracunavanjaServiceReference.Rezultat Sabiranje(MatricnaIzracunavanjaClient.MatricnaIzracunavanjaServiceReference.Matrica matrica);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMatricnaIzracunavanjaService/Sabiranje", ReplyAction="http://tempuri.org/IMatricnaIzracunavanjaService/SabiranjeResponse")]
        System.Threading.Tasks.Task<MatricnaIzracunavanjaClient.MatricnaIzracunavanjaServiceReference.Rezultat> SabiranjeAsync(MatricnaIzracunavanjaClient.MatricnaIzracunavanjaServiceReference.Matrica matrica);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMatricnaIzracunavanjaService/MnozenjeMatricom", ReplyAction="http://tempuri.org/IMatricnaIzracunavanjaService/MnozenjeMatricomResponse")]
        MatricnaIzracunavanjaClient.MatricnaIzracunavanjaServiceReference.Rezultat MnozenjeMatricom(MatricnaIzracunavanjaClient.MatricnaIzracunavanjaServiceReference.Matrica matrica);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMatricnaIzracunavanjaService/MnozenjeMatricom", ReplyAction="http://tempuri.org/IMatricnaIzracunavanjaService/MnozenjeMatricomResponse")]
        System.Threading.Tasks.Task<MatricnaIzracunavanjaClient.MatricnaIzracunavanjaServiceReference.Rezultat> MnozenjeMatricomAsync(MatricnaIzracunavanjaClient.MatricnaIzracunavanjaServiceReference.Matrica matrica);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMatricnaIzracunavanjaService/MnozenjeSkalarom", ReplyAction="http://tempuri.org/IMatricnaIzracunavanjaService/MnozenjeSkalaromResponse")]
        MatricnaIzracunavanjaClient.MatricnaIzracunavanjaServiceReference.Rezultat MnozenjeSkalarom(int matrica);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMatricnaIzracunavanjaService/MnozenjeSkalarom", ReplyAction="http://tempuri.org/IMatricnaIzracunavanjaService/MnozenjeSkalaromResponse")]
        System.Threading.Tasks.Task<MatricnaIzracunavanjaClient.MatricnaIzracunavanjaServiceReference.Rezultat> MnozenjeSkalaromAsync(int matrica);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMatricnaIzracunavanjaService/Transponuj", ReplyAction="http://tempuri.org/IMatricnaIzracunavanjaService/TransponujResponse")]
        MatricnaIzracunavanjaClient.MatricnaIzracunavanjaServiceReference.Rezultat Transponuj();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMatricnaIzracunavanjaService/Transponuj", ReplyAction="http://tempuri.org/IMatricnaIzracunavanjaService/TransponujResponse")]
        System.Threading.Tasks.Task<MatricnaIzracunavanjaClient.MatricnaIzracunavanjaServiceReference.Rezultat> TransponujAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IMatricnaIzracunavanjaServiceChannel : MatricnaIzracunavanjaClient.MatricnaIzracunavanjaServiceReference.IMatricnaIzracunavanjaService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class MatricnaIzracunavanjaServiceClient : System.ServiceModel.ClientBase<MatricnaIzracunavanjaClient.MatricnaIzracunavanjaServiceReference.IMatricnaIzracunavanjaService>, MatricnaIzracunavanjaClient.MatricnaIzracunavanjaServiceReference.IMatricnaIzracunavanjaService {
        
        public MatricnaIzracunavanjaServiceClient() {
        }
        
        public MatricnaIzracunavanjaServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public MatricnaIzracunavanjaServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MatricnaIzracunavanjaServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MatricnaIzracunavanjaServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public MatricnaIzracunavanjaClient.MatricnaIzracunavanjaServiceReference.Rezultat Postavi(MatricnaIzracunavanjaClient.MatricnaIzracunavanjaServiceReference.Matrica matrica) {
            return base.Channel.Postavi(matrica);
        }
        
        public System.Threading.Tasks.Task<MatricnaIzracunavanjaClient.MatricnaIzracunavanjaServiceReference.Rezultat> PostaviAsync(MatricnaIzracunavanjaClient.MatricnaIzracunavanjaServiceReference.Matrica matrica) {
            return base.Channel.PostaviAsync(matrica);
        }
        
        public MatricnaIzracunavanjaClient.MatricnaIzracunavanjaServiceReference.Rezultat PreuzmiMatricu() {
            return base.Channel.PreuzmiMatricu();
        }
        
        public System.Threading.Tasks.Task<MatricnaIzracunavanjaClient.MatricnaIzracunavanjaServiceReference.Rezultat> PreuzmiMatricuAsync() {
            return base.Channel.PreuzmiMatricuAsync();
        }
        
        public MatricnaIzracunavanjaClient.MatricnaIzracunavanjaServiceReference.Rezultat Sabiranje(MatricnaIzracunavanjaClient.MatricnaIzracunavanjaServiceReference.Matrica matrica) {
            return base.Channel.Sabiranje(matrica);
        }
        
        public System.Threading.Tasks.Task<MatricnaIzracunavanjaClient.MatricnaIzracunavanjaServiceReference.Rezultat> SabiranjeAsync(MatricnaIzracunavanjaClient.MatricnaIzracunavanjaServiceReference.Matrica matrica) {
            return base.Channel.SabiranjeAsync(matrica);
        }
        
        public MatricnaIzracunavanjaClient.MatricnaIzracunavanjaServiceReference.Rezultat MnozenjeMatricom(MatricnaIzracunavanjaClient.MatricnaIzracunavanjaServiceReference.Matrica matrica) {
            return base.Channel.MnozenjeMatricom(matrica);
        }
        
        public System.Threading.Tasks.Task<MatricnaIzracunavanjaClient.MatricnaIzracunavanjaServiceReference.Rezultat> MnozenjeMatricomAsync(MatricnaIzracunavanjaClient.MatricnaIzracunavanjaServiceReference.Matrica matrica) {
            return base.Channel.MnozenjeMatricomAsync(matrica);
        }
        
        public MatricnaIzracunavanjaClient.MatricnaIzracunavanjaServiceReference.Rezultat MnozenjeSkalarom(int matrica) {
            return base.Channel.MnozenjeSkalarom(matrica);
        }
        
        public System.Threading.Tasks.Task<MatricnaIzracunavanjaClient.MatricnaIzracunavanjaServiceReference.Rezultat> MnozenjeSkalaromAsync(int matrica) {
            return base.Channel.MnozenjeSkalaromAsync(matrica);
        }
        
        public MatricnaIzracunavanjaClient.MatricnaIzracunavanjaServiceReference.Rezultat Transponuj() {
            return base.Channel.Transponuj();
        }
        
        public System.Threading.Tasks.Task<MatricnaIzracunavanjaClient.MatricnaIzracunavanjaServiceReference.Rezultat> TransponujAsync() {
            return base.Channel.TransponujAsync();
        }
    }
}
