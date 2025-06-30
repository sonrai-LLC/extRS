using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ReportingServices.Api.Models {

  /// <summary>
  /// An object that specifies options for a data cache.
  /// </summary>
  [DataContract]
  public class CacheOptions {
    /// <summary>
    /// Gets or Sets ExecutionType
    /// </summary>
    [DataMember(Name="ExecutionType", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "ExecutionType")]
    public required ItemExecutionType ExecutionType { get; set; }

    /// <summary>
    /// Gets or Sets Expiration
    /// </summary>
    [DataMember(Name="Expiration", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Expiration")]
    public required ExpirationReference Expiration { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class CacheOptions {\n");
      sb.Append("  ExecutionType: ").Append(ExecutionType).Append("\n");
      sb.Append("  Expiration: ").Append(Expiration).Append("\n");
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
