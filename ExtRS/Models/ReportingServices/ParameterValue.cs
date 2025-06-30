using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ReportingServices.Api.Models {

  /// <summary>
  /// An object that specifies a parameter&#39;s value as a name-value pair.
  /// </summary>
  [DataContract]
  public class ParameterValue {
    /// <summary>
    /// A string that contains the name of the parameter.
    /// </summary>
    /// <value>A string that contains the name of the parameter.</value>
    [DataMember(Name="Name", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Name")]
    public required string Name { get; set; }

    /// <summary>
    /// A string that contains the value for the parameter.
    /// </summary>
    /// <value>A string that contains the value for the parameter.</value>
    [DataMember(Name="Value", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Value")]
    public required string Value { get; set; }

    /// <summary>
    /// A boolean value that indicates if the parameter's value references a field.
    /// </summary>
    /// <value>A boolean value that indicates if the parameter's value references a field.</value>
    [DataMember(Name="IsValueFieldReference", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "IsValueFieldReference")]
    public bool? IsValueFieldReference { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class ParameterValue {\n");
      sb.Append("  Name: ").Append(Name).Append("\n");
      sb.Append("  Value: ").Append(Value).Append("\n");
      sb.Append("  IsValueFieldReference: ").Append(IsValueFieldReference).Append("\n");
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
