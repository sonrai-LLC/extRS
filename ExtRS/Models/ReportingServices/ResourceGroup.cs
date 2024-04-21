using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ReportingServices.Api.Models {

  /// <summary>
  /// An object that contains metadata and contents for a ResourceItem.
  /// </summary>
  [DataContract]
  public class ResourceGroup {
    /// <summary>
    /// Gets or Sets Type
    /// </summary>
    [DataMember(Name="Type", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Type")]
    public MobileReportResourceGroupType Type { get; set; }

    /// <summary>
    /// An array of objects of type ResourceItem that contain the contents of the ResourceGroup.
    /// </summary>
    /// <value>An array of objects of type ResourceItem that contain the contents of the ResourceGroup.</value>
    [DataMember(Name="Items", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Items")]
    public List<ResourceItem> Items { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class ResourceGroup {\n");
      sb.Append("  Type: ").Append(Type).Append("\n");
      sb.Append("  Items: ").Append(Items).Append("\n");
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
