using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ReportingServices.Api.Models {

  /// <summary>
  /// An object that contains a specification for cache expiration, expressed either in minutes or by a schedule.
  /// </summary>
  [DataContract]
  public class ExpirationReference {
    /// <summary>
    /// Number of minutes until expiration.
    /// </summary>
    /// <value>Number of minutes until expiration.</value>
    [DataMember(Name="Minutes", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Minutes")]
    public int? Minutes { get; set; }

    /// <summary>
    /// Gets or Sets Schedule
    /// </summary>
    [DataMember(Name="Schedule", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Schedule")]
    public ScheduleReference? Schedule { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class ExpirationReference {\n");
      sb.Append("  Minutes: ").Append(Minutes).Append("\n");
      sb.Append("  Schedule: ").Append(Schedule).Append("\n");
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
