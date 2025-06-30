using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ReportingServices.Api.Models {

  /// <summary>
  /// Result of bulk operations like MoveItems and DeleteItems
  /// </summary>
  [DataContract]
  public class BulkOperationsResult {
    /// <summary>
    /// Gets or Sets FailedOperations
    /// </summary>
    [DataMember(Name="FailedOperations", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "FailedOperations")]
    public List<string>? FailedOperations { get; set; }

    /// <summary>
    /// Gets or Sets HasErrors
    /// </summary>
    [DataMember(Name="HasErrors", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "HasErrors")]
    public bool? HasErrors { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class BulkOperationsResult {\n");
      sb.Append("  FailedOperations: ").Append(FailedOperations).Append("\n");
      sb.Append("  HasErrors: ").Append(HasErrors).Append("\n");
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
