using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace IO.Swagger.Model {

  /// <summary>
  /// An object that contains the definition of a Reporting Services extension&#39;s parameter.
  /// </summary>
  [DataContract]
  public class ExtensionParameter {
    /// <summary>
    /// A string value that specifies the name for the ExtensionParameter.
    /// </summary>
    /// <value>A string value that specifies the name for the ExtensionParameter.</value>
    [DataMember(Name="Name", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Name")]
    public string Name { get; set; }

    /// <summary>
    /// The name of the extension setting that is displayed to the user.
    /// </summary>
    /// <value>The name of the extension setting that is displayed to the user.</value>
    [DataMember(Name="DisplayName", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "DisplayName")]
    public string DisplayName { get; set; }

    /// <summary>
    /// Indicates whether the value is required.
    /// </summary>
    /// <value>Indicates whether the value is required.</value>
    [DataMember(Name="Required", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Required")]
    public bool? Required { get; set; }

    /// <summary>
    /// Indicates whether the setting is read-only.
    /// </summary>
    /// <value>Indicates whether the setting is read-only.</value>
    [DataMember(Name="ReadOnly", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "ReadOnly")]
    public bool? ReadOnly { get; set; }

    /// <summary>
    /// A string that represents the value of an extension parameter.
    /// </summary>
    /// <value>A string that represents the value of an extension parameter.</value>
    [DataMember(Name="Value", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Value")]
    public string Value { get; set; }

    /// <summary>
    /// An error that describes a problem with the value of the setting.
    /// </summary>
    /// <value>An error that describes a problem with the value of the setting.</value>
    [DataMember(Name="Error", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Error")]
    public string Error { get; set; }

    /// <summary>
    /// Indicates whether the extension parameter value should be encrypted in the Report Server database.
    /// </summary>
    /// <value>Indicates whether the extension parameter value should be encrypted in the Report Server database.</value>
    [DataMember(Name="Encrypted", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Encrypted")]
    public bool? Encrypted { get; set; }

    /// <summary>
    /// A Boolean value that indicates whether the ExtensionParameter is a password.
    /// </summary>
    /// <value>A Boolean value that indicates whether the ExtensionParameter is a password.</value>
    [DataMember(Name="IsPassword", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "IsPassword")]
    public bool? IsPassword { get; set; }

    /// <summary>
    /// A set of values that can be configured for the setting.
    /// </summary>
    /// <value>A set of values that can be configured for the setting.</value>
    [DataMember(Name="ValidValues", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "ValidValues")]
    public List<ValidValue> ValidValues { get; set; }

    /// <summary>
    /// A Boolean value that indicates whether the ValidValues property is null.
    /// </summary>
    /// <value>A Boolean value that indicates whether the ValidValues property is null.</value>
    [DataMember(Name="ValidValuesIsNull", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "ValidValuesIsNull")]
    public bool? ValidValuesIsNull { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class ExtensionParameter {\n");
      sb.Append("  Name: ").Append(Name).Append("\n");
      sb.Append("  DisplayName: ").Append(DisplayName).Append("\n");
      sb.Append("  Required: ").Append(Required).Append("\n");
      sb.Append("  ReadOnly: ").Append(ReadOnly).Append("\n");
      sb.Append("  Value: ").Append(Value).Append("\n");
      sb.Append("  Error: ").Append(Error).Append("\n");
      sb.Append("  Encrypted: ").Append(Encrypted).Append("\n");
      sb.Append("  IsPassword: ").Append(IsPassword).Append("\n");
      sb.Append("  ValidValues: ").Append(ValidValues).Append("\n");
      sb.Append("  ValidValuesIsNull: ").Append(ValidValuesIsNull).Append("\n");
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
