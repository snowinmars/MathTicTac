﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MathTicTac.PL.Soap.TestClient.AccountService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="AccountSM", Namespace="http://schemas.datacontract.org/2004/07/MathTicTac.PL.Soap.BindingLib.ServiceMode" +
        "ls")]
    [System.SerializableAttribute()]
    public partial class AccountSM : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int DrawField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int LoseField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UsernameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int WonField;
        
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
        public int Draw {
            get {
                return this.DrawField;
            }
            set {
                if ((this.DrawField.Equals(value) != true)) {
                    this.DrawField = value;
                    this.RaisePropertyChanged("Draw");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Lose {
            get {
                return this.LoseField;
            }
            set {
                if ((this.LoseField.Equals(value) != true)) {
                    this.LoseField = value;
                    this.RaisePropertyChanged("Lose");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Username {
            get {
                return this.UsernameField;
            }
            set {
                if ((object.ReferenceEquals(this.UsernameField, value) != true)) {
                    this.UsernameField = value;
                    this.RaisePropertyChanged("Username");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Won {
            get {
                return this.WonField;
            }
            set {
                if ((this.WonField.Equals(value) != true)) {
                    this.WonField = value;
                    this.RaisePropertyChanged("Won");
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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ResponseResult", Namespace="http://schemas.datacontract.org/2004/07/MathTicTac.Enums")]
    public enum ResponseResult : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        None = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Ok = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        TokenInvalid = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        AccountDataInvalid = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        TurnUnavailiable = 4,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="AccountService.IAccountLogicService")]
    public interface IAccountLogicService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccountLogicService/Add", ReplyAction="http://tempuri.org/IAccountLogicService/AddResponse")]
        MathTicTac.PL.Soap.TestClient.AccountService.ResponseResult Add(MathTicTac.PL.Soap.TestClient.AccountService.AccountSM item, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccountLogicService/Get", ReplyAction="http://tempuri.org/IAccountLogicService/GetResponse")]
        MathTicTac.PL.Soap.TestClient.AccountService.ResponseResult Get(out MathTicTac.PL.Soap.TestClient.AccountService.AccountSM account, int id, string token, string ip);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccountLogicService/LoginById", ReplyAction="http://tempuri.org/IAccountLogicService/LoginByIdResponse")]
        MathTicTac.PL.Soap.TestClient.AccountService.ResponseResult LoginById(out string token, string identifier, string password, string ip);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccountLogicService/LoginByToken", ReplyAction="http://tempuri.org/IAccountLogicService/LoginByTokenResponse")]
        MathTicTac.PL.Soap.TestClient.AccountService.ResponseResult LoginByToken(string token, string ip);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccountLogicService/Logout", ReplyAction="http://tempuri.org/IAccountLogicService/LogoutResponse")]
        MathTicTac.PL.Soap.TestClient.AccountService.ResponseResult Logout(string token, string ip);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAccountLogicServiceChannel : MathTicTac.PL.Soap.TestClient.AccountService.IAccountLogicService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AccountLogicServiceClient : System.ServiceModel.ClientBase<MathTicTac.PL.Soap.TestClient.AccountService.IAccountLogicService>, MathTicTac.PL.Soap.TestClient.AccountService.IAccountLogicService {
        
        public AccountLogicServiceClient() {
        }
        
        public AccountLogicServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public AccountLogicServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AccountLogicServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AccountLogicServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public MathTicTac.PL.Soap.TestClient.AccountService.ResponseResult Add(MathTicTac.PL.Soap.TestClient.AccountService.AccountSM item, string password) {
            return base.Channel.Add(item, password);
        }
        
        public MathTicTac.PL.Soap.TestClient.AccountService.ResponseResult Get(out MathTicTac.PL.Soap.TestClient.AccountService.AccountSM account, int id, string token, string ip) {
            return base.Channel.Get(out account, id, token, ip);
        }
        
        public MathTicTac.PL.Soap.TestClient.AccountService.ResponseResult LoginById(out string token, string identifier, string password, string ip) {
            return base.Channel.LoginById(out token, identifier, password, ip);
        }
        
        public MathTicTac.PL.Soap.TestClient.AccountService.ResponseResult LoginByToken(string token, string ip) {
            return base.Channel.LoginByToken(token, ip);
        }
        
        public MathTicTac.PL.Soap.TestClient.AccountService.ResponseResult Logout(string token, string ip) {
            return base.Channel.Logout(token, ip);
        }
    }
}
