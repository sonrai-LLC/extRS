using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ReportingServices.Api.Models {

  /// <summary>
  /// An object that holds the definition of a Reporting Services extension.
  /// </summary>
  [DataContract]
  public class Extension {
    /// <summary>
    /// Gets or Sets ExtensionType
    /// </summary>
    [DataMember(Name="ExtensionType", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "ExtensionType")]
    public ExtensionType? ExtensionType { get; set; }

    /// <summary>
    /// A string value that specifies the name for the Extension. This name will typically be displayed in the user interface.
    /// </summary>
    /// <value>A string value that specifies the name for the Extension. This name will typically be displayed in the user interface.</value>
    [DataMember(Name="Name", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Name")]
    public string? Name { get; set; }

    /// <summary>
    /// A string value that specifies a localized name for the Extension.
    /// </summary>
    /// <value>A string value that specifies a localized name for the Extension.</value>
    [DataMember(Name="LocalizedName", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "LocalizedName")]
    public string? LocalizedName { get; set; }

    /// <summary>
    /// A Boolean value that indicates if the Extension is visible. If false, the item will generally not appear in listings of available extensions.
    /// </summary>
    /// <value>A Boolean value that indicates if the Extension is visible. If false, the item will generally not appear in listings of available extensions.</value>
    [DataMember(Name="Visible", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Visible")]
    public bool? Visible { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class Extension {\n");
      sb.Append("  ExtensionType: ").Append(ExtensionType).Append("\n");
      sb.Append("  Name: ").Append(Name).Append("\n");
      sb.Append("  LocalizedName: ").Append(LocalizedName).Append("\n");
      sb.Append("  Visible: ").Append(Visible).Append("\n");
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
