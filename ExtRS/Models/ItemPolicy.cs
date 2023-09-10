using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ReportingServices.Api.Models {

  /// <summary>
  /// An object that specifies the access policy for the item.
  /// </summary>
  [DataContract]
  public class ItemPolicy {
    /// <summary>
    /// A Boolean value that indicates whether the access policy is to be inherited from the item's parent item.
    /// </summary>
    /// <value>A Boolean value that indicates whether the access policy is to be inherited from the item's parent item.</value>
    [DataMember(Name="InheritParentPolicy", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "InheritParentPolicy")]
    public bool? InheritParentPolicy { get; set; }

    /// <summary>
    /// An array of objects of type Policy that specify the access policies to be applied to the item.      
    /// </summary>
    /// <value>An array of objects of type Policy that specify the access policies to be applied to the item.      </value>
    [DataMember(Name="Policies", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Policies")]
    public List<Policy> Policies { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class ItemPolicy {\n");
      sb.Append("  InheritParentPolicy: ").Append(InheritParentPolicy).Append("\n");
      sb.Append("  Policies: ").Append(Policies).Append("\n");
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
