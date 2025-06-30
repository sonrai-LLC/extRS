using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ReportingServices.Api.Models {

  /// <summary>
  /// Represents a SQL query to be executed. The query cannot contain parameters.
  /// </summary>
  [DataContract]
  public class Query {
    /// <summary>
    /// Command to be executed against given data source
    /// </summary>
    /// <value>Command to be executed against given data source</value>
    [DataMember(Name="CommandText", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "CommandText")]
    public required string CommandText { get; set; }

    /// <summary>
    /// Query Timeout, default is 30 seconds.
    /// </summary>
    /// <value>Query Timeout, default is 30 seconds.</value>
    [DataMember(Name="Timeout", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Timeout")]
    public int? Timeout { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class Query {\n");
      sb.Append("  CommandText: ").Append(CommandText).Append("\n");
      sb.Append("  Timeout: ").Append(Timeout).Append("\n");
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
