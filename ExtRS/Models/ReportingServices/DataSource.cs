using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ReportingServices.Api.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class DataSource : CatalogItem
    {
        // the uri of the RS catalog item
        public string Uri { get; set; }

        /// <summary>
        /// A Boolean value that specifies whether the DataSource is enabled for use.
        /// </summary>
        /// <value>A Boolean value that specifies whether the DataSource is enabled for use.</value>
        [DataMember(Name = "IsEnabled", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "IsEnabled")]
        public bool? IsEnabled { get; set; }

        /// <summary>
        /// A string value that can be passed to a data source in order to begin the process of establishing connection.
        /// </summary>
        /// <value>A string value that can be passed to a data source in order to begin the process of establishing connection.</value>
        [DataMember(Name = "ConnectionString", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "ConnectionString")]
        public string ConnectionString { get; set; }

        /// <summary>
        /// DataSource extension such as 'SQL'.
        /// </summary>
        /// <value>DataSource extension such as 'SQL'.</value>
        [DataMember(Name = "DataSourceType", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "DataSourceType")]
        public string DataSourceType { get; set; }

        /// <summary>
        /// Indicates whether the original connection string for the data source was expression-based.
        /// </summary>
        /// <value>Indicates whether the original connection string for the data source was expression-based.</value>
        [DataMember(Name = "IsOriginalConnectionStringExpressionBased", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "IsOriginalConnectionStringExpressionBased")]
        public bool? IsOriginalConnectionStringExpressionBased { get; set; }

        /// <summary>
        /// Specifies whether the original connection string is overridden.
        /// </summary>
        /// <value>Specifies whether the original connection string is overridden.</value>
        [DataMember(Name = "IsConnectionStringOverridden", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "IsConnectionStringOverridden")]
        public bool? IsConnectionStringOverridden { get; set; }

        /// <summary>
        /// Gets or Sets CredentialsByUser
        /// </summary>
        [DataMember(Name = "CredentialsByUser", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "CredentialsByUser")]
        public CredentialsSuppliedByUser CredentialsByUser { get; set; }

        /// <summary>
        /// Gets or Sets CredentialsInServer
        /// </summary>
        [DataMember(Name = "CredentialsInServer", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "CredentialsInServer")]
        public CredentialsStoredInServer CredentialsInServer { get; set; }

        /// <summary>
        /// Indicates whether this is a reference to a shared data source or an embedded data source.
        /// </summary>
        /// <value>Indicates whether this is a reference to a shared data source or an embedded data source.</value>
        [DataMember(Name = "IsReference", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "IsReference")]
        public bool? IsReference { get; set; }

        /// <summary>
        /// Gets or Sets Subscriptions
        /// </summary>
        [DataMember(Name = "Subscriptions", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "Subscriptions")]
        public Subscription Subscriptions { get; set; }


        /// <summary>
        /// Get the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class DataSource {\n");
            sb.Append("  IsEnabled: ").Append(IsEnabled).Append("\n");
            sb.Append("  ConnectionString: ").Append(ConnectionString).Append("\n");
            sb.Append("  DataSourceType: ").Append(DataSourceType).Append("\n");
            sb.Append("  IsOriginalConnectionStringExpressionBased: ").Append(IsOriginalConnectionStringExpressionBased).Append("\n");
            sb.Append("  IsConnectionStringOverridden: ").Append(IsConnectionStringOverridden).Append("\n");
            sb.Append("  CredentialsByUser: ").Append(CredentialsByUser).Append("\n");
            sb.Append("  CredentialsInServer: ").Append(CredentialsInServer).Append("\n");
            sb.Append("  IsReference: ").Append(IsReference).Append("\n");
            sb.Append("  Subscriptions: ").Append(Subscriptions).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Get the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public new string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

    }
}
