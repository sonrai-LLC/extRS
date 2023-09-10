using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ReportingServices.Api.Models {

  /// <summary>
  /// An object that defines a schedule including a start date-time and an optional end date-time.
  /// </summary>
  [DataContract]
  public class ScheduleDefinition {
    /// <summary>
    /// A string that specifies the date-time of the start of the schedule.
    /// </summary>
    /// <value>A string that specifies the date-time of the start of the schedule.</value>
    [DataMember(Name="StartDateTime", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "StartDateTime")]
    public DateTime? StartDateTime { get; set; }

    /// <summary>
    /// A string that specifies the date-time of the end of the schedule.
    /// </summary>
    /// <value>A string that specifies the date-time of the end of the schedule.</value>
    [DataMember(Name="EndDate", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "EndDate")]
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// A boolean value that indicates whether the schedule end is specified.
    /// </summary>
    /// <value>A boolean value that indicates whether the schedule end is specified.</value>
    [DataMember(Name="EndDateSpecified", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "EndDateSpecified")]
    public bool? EndDateSpecified { get; set; }

    /// <summary>
    /// Gets or Sets Recurrence
    /// </summary>
    [DataMember(Name="Recurrence", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Recurrence")]
    public ScheduleRecurrence Recurrence { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class ScheduleDefinition {\n");
      sb.Append("  StartDateTime: ").Append(StartDateTime).Append("\n");
      sb.Append("  EndDate: ").Append(EndDate).Append("\n");
      sb.Append("  EndDateSpecified: ").Append(EndDateSpecified).Append("\n");
      sb.Append("  Recurrence: ").Append(Recurrence).Append("\n");
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
