using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ReportingServices.Api.Models {

  /// <summary>
  /// An object that defines parameter values that are used for a Reporting Services extension.
  /// </summary>
  [DataContract]
  public class ExtensionSettings {
    /// <summary>
    /// A string value that specifies the name of the Reporting Services extension that the settings in the object apply to.
    /// </summary>
    /// <value>A string value that specifies the name of the Reporting Services extension that the settings in the object apply to.</value>
    [DataMember(Name="Extension", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Extension")]
    public string? Extension { get; set; }

    /// <summary>
    /// Gets or Sets ParameterValues
    /// </summary>
    [DataMember(Name="ParameterValues", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "ParameterValues")]
    public List<ParameterValue>? ParameterValues { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class ExtensionSettings {\n");
      sb.Append("  Extension: ").Append(Extension).Append("\n");
      sb.Append("  ParameterValues: ").Append(ParameterValues).Append("\n");
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
