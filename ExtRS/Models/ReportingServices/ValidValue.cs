using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ReportingServices.Api.Models {

  /// <summary>
  /// An object that contains a value that is valid for use in its context, in the form of a label-value pair.
  /// </summary>
  [DataContract]
  public class ValidValue {
    /// <summary>
    /// A string value that specifies the label for the ValidValue.
    /// </summary>
    /// <value>A string value that specifies the label for the ValidValue.</value>
    [DataMember(Name="Label", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Label")]
    public string Label { get; set; }

    /// <summary>
    /// A string value that specifies the value of the ValidValue.
    /// </summary>
    /// <value>A string value that specifies the value of the ValidValue.</value>
    [DataMember(Name="Value", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Value")]
    public string Value { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class ValidValue {\n");
      sb.Append("  Label: ").Append(Label).Append("\n");
      sb.Append("  Value: ").Append(Value).Append("\n");
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
