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
  public class ManifestItem {
    /// <summary>
    /// A unique UUID value that specifies the identifier by which this defined item can be referenced in requests or in other defined objects.
    /// </summary>
    /// <value>A unique UUID value that specifies the identifier by which this defined item can be referenced in requests or in other defined objects.</value>
    [DataMember(Name="Id", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Id")]
    public Guid? Id { get; set; }

    /// <summary>
    /// A string value that contains the complete URL for the defined item.
    /// </summary>
    /// <value>A string value that contains the complete URL for the defined item.</value>
    [DataMember(Name="Path", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Path")]
    public string? Path { get; set; }

    /// <summary>
    /// A string value that specifies the name for the item. This name will typically be displayed in the user interface.
    /// </summary>
    /// <value>A string value that specifies the name for the item. This name will typically be displayed in the user interface.</value>
    [DataMember(Name="Name", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Name")]
    public string? Name { get; set; }

    /// <summary>
    /// A string value that contains an SHA256 hash of the contents of the item.
    /// </summary>
    /// <value>A string value that contains an SHA256 hash of the contents of the item.</value>
    [DataMember(Name="Hash", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Hash")]
    public string? Hash { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class ManifestItem {\n");
      sb.Append("  Id: ").Append(Id).Append("\n");
      sb.Append("  Path: ").Append(Path).Append("\n");
      sb.Append("  Name: ").Append(Name).Append("\n");
      sb.Append("  Hash: ").Append(Hash).Append("\n");
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
