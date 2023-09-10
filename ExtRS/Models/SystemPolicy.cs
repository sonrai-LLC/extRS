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
  public class SystemPolicy {
    /// <summary>
    /// A unique UUID value that specifies the identifier of the SystemPolicy.
    /// </summary>
    /// <value>A unique UUID value that specifies the identifier of the SystemPolicy.</value>
    [DataMember(Name="Id", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Id")]
    public Guid? Id { get; set; }

    /// <summary>
    /// An array of objects of type Policy that specify the access policies to be applied to the System.
    /// </summary>
    /// <value>An array of objects of type Policy that specify the access policies to be applied to the System.</value>
    [DataMember(Name="Policies", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Policies")]
    public List<Policy> Policies { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class SystemPolicy {\n");
      sb.Append("  Id: ").Append(Id).Append("\n");
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
