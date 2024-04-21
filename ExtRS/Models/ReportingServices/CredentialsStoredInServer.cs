using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ReportingServices.Api.Models {

  /// <summary>
  /// This object holds credentials for connections to an external data source. The object is stored on the Report Server for later retrieval and use. NOTE: This object represents a security risk as its properties can be seen in plain text over the HTTP protocol; HTTPS is recommended.
  /// </summary>
  [DataContract]
  public class CredentialsStoredInServer {
    /// <summary>
    /// A string value that contains the user name to be used to connect to an external data source.
    /// </summary>
    /// <value>A string value that contains the user name to be used to connect to an external data source.</value>
    [DataMember(Name="UserName", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "UserName")]
    public string UserName { get; set; }

    /// <summary>
    /// A string value that contains the password to be used to connect to an external data source.
    /// </summary>
    /// <value>A string value that contains the password to be used to connect to an external data source.</value>
    [DataMember(Name="Password", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Password")]
    public string Password { get; set; }

    /// <summary>
    /// A boolean value that indicates whether the supplied credentials should be used as Windows credentials.
    /// </summary>
    /// <value>A boolean value that indicates whether the supplied credentials should be used as Windows credentials.</value>
    [DataMember(Name="UseAsWindowsCredentials", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "UseAsWindowsCredentials")]
    public bool? UseAsWindowsCredentials { get; set; }

    /// <summary>
    /// A boolean value that indicates whether the session should impersonate the user for the supplied credentials.
    /// </summary>
    /// <value>A boolean value that indicates whether the session should impersonate the user for the supplied credentials.</value>
    [DataMember(Name="ImpersonateAuthenticatedUser", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "ImpersonateAuthenticatedUser")]
    public bool? ImpersonateAuthenticatedUser { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class CredentialsStoredInServer {\n");
      sb.Append("  UserName: ").Append(UserName).Append("\n");
      sb.Append("  Password: ").Append(Password).Append("\n");
      sb.Append("  UseAsWindowsCredentials: ").Append(UseAsWindowsCredentials).Append("\n");
      sb.Append("  ImpersonateAuthenticatedUser: ").Append(ImpersonateAuthenticatedUser).Append("\n");
      sb.Append("}\n");
      return sb.ToString();
    }

    /// <summary>
    /// Get the JSON string presentation of the object
    /// </summary>
    /// <returns>JSON string presentation of the object</returns>
    public string ToJson() {
      return JsonConvert.SerializeObject(this, Formatting.Indented);
    }

}
}
