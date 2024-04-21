using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ReportingServices.Api.Models {

  /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class UserCredentials {
    /// <summary>
    /// A string value that specifies the user name for the user.
    /// </summary>
    /// <value>A string value that specifies the user name for the user.</value>
    [DataMember(Name="UserName", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "UserName")]
    public string UserName { get; set; }

    /// <summary>
    /// A string value that specifies the password for the user.
    /// </summary>
    /// <value>A string value that specifies the password for the user.</value>
    [DataMember(Name="Password", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Password")]
    public string Password { get; set; }

    /// <summary>
    /// A string value that specifies the domain for the user.
    /// </summary>
    /// <value>A string value that specifies the domain for the user.</value>
    [DataMember(Name="Domain", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Domain")]
    public string Domain { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class UserCredentials {\n");
      sb.Append("  UserName: ").Append(UserName).Append("\n");
      sb.Append("  Password: ").Append(Password).Append("\n");
      sb.Append("  Domain: ").Append(Domain).Append("\n");
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
