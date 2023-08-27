using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace IO.Swagger.Model {

  /// <summary>
  /// An object that specifies the type of the target of a drillthrough operation.
  /// </summary>
  [DataContract]
  public class DrillthroughTarget {
    /// <summary>
    /// Gets or Sets DrillthroughTargetType
    /// </summary>
    [DataMember(Name="DrillthroughTargetType", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "DrillthroughTargetType")]
    public DrillthroughTargetType DrillthroughTargetType { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class DrillthroughTarget {\n");
      sb.Append("  DrillthroughTargetType: ").Append(DrillthroughTargetType).Append("\n");
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
