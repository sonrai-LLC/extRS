using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ReportingServices.Api.Models {

  /// <summary>
  /// An object that contains information about prompting a user for credentials for connections to an external data source.
  /// </summary>
  [DataContract]
  public class CredentialsSuppliedByUser {
    /// <summary>
    /// A string value that contains text used to prompt a user to supply credentials for connections to an external data source.
    /// </summary>
    /// <value>A string value that contains text used to prompt a user to supply credentials for connections to an external data source.</value>
    [DataMember(Name="DisplayText", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "DisplayText")]
    public string? DisplayText { get; set; }

    /// <summary>
    /// A boolean value that indicates whether credentials the user supplies in a prompt should be used as Windows credentials.
    /// </summary>
    /// <value>A boolean value that indicates whether credentials the user supplies in a prompt should be used as Windows credentials.</value>
    [DataMember(Name="UseAsWindowsCredentials", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "UseAsWindowsCredentials")]
    public bool? UseAsWindowsCredentials { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class CredentialsSuppliedByUser {\n");
      sb.Append("  DisplayText: ").Append(DisplayText).Append("\n");
      sb.Append("  UseAsWindowsCredentials: ").Append(UseAsWindowsCredentials).Append("\n");
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
