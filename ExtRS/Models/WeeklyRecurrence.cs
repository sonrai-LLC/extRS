using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ReportingServices.Api.Models {

  /// <summary>
  /// Represents the intervals at which a scheduled report runs. Intervals are specified in weeks and on which days of the week.
  /// </summary>
  [DataContract]
  public class WeeklyRecurrence {
    /// <summary>
    /// An Int32 value representing interval in weeks.
    /// </summary>
    /// <value>An Int32 value representing interval in weeks.</value>
    [DataMember(Name="WeeksInterval", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "WeeksInterval")]
    public int? WeeksInterval { get; set; }

    /// <summary>
    /// True if using WeeksInterval.
    /// </summary>
    /// <value>True if using WeeksInterval.</value>
    [DataMember(Name="WeeksIntervalSpecified", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "WeeksIntervalSpecified")]
    public bool? WeeksIntervalSpecified { get; set; }

    /// <summary>
    /// Gets or Sets DaysOfWeek
    /// </summary>
    [DataMember(Name="DaysOfWeek", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "DaysOfWeek")]
    public DaysOfWeekSelector DaysOfWeek { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class WeeklyRecurrence {\n");
      sb.Append("  WeeksInterval: ").Append(WeeksInterval).Append("\n");
      sb.Append("  WeeksIntervalSpecified: ").Append(WeeksIntervalSpecified).Append("\n");
      sb.Append("  DaysOfWeek: ").Append(DaysOfWeek).Append("\n");
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
