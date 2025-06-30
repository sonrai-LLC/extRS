using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ReportingServices.Api.Models {

  /// <summary>
  /// Represents the result of testing a DataSource connection
  /// </summary>
  [DataContract]
  public class DataSourceCheckResult {
    /// <summary>
    /// Gets or Sets IsSuccessful
    /// </summary>
    [DataMember(Name="IsSuccessful", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "IsSuccessful")]
    public bool? IsSuccessful { get; set; }

    /// <summary>
    /// Gets or Sets ErrorMessage
    /// </summary>
    [DataMember(Name="ErrorMessage", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "ErrorMessage")]
    public string? ErrorMessage { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class DataSourceCheckResult {\n");
      sb.Append("  IsSuccessful: ").Append(IsSuccessful).Append("\n");
      sb.Append("  ErrorMessage: ").Append(ErrorMessage).Append("\n");
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
