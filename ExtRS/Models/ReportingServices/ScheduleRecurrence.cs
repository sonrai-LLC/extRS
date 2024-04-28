using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ReportingServices.Api.Models {

  /// <summary>
  /// An object that defines the recurrence of a schedule. When defining the recurrence, only one of the recurrence types can be set.
  /// </summary>
  [DataContract]
  public class ScheduleRecurrence {
    /// <summary>
    /// Gets or Sets MinuteRecurrence
    /// </summary>
    [DataMember(Name="MinuteRecurrence", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "MinuteRecurrence")]
    public MinuteRecurrence? MinuteRecurrence { get; set; }

    /// <summary>
    /// Gets or Sets DailyRecurrence
    /// </summary>
    [DataMember(Name="DailyRecurrence", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "DailyRecurrence")]
    public DailyRecurrence? DailyRecurrence { get; set; }

    /// <summary>
    /// Gets or Sets WeeklyRecurrence
    /// </summary>
    [DataMember(Name="WeeklyRecurrence", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "WeeklyRecurrence")]
    public WeeklyRecurrence? WeeklyRecurrence { get; set; }

    /// <summary>
    /// Gets or Sets MonthlyRecurrence
    /// </summary>
    [DataMember(Name="MonthlyRecurrence", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "MonthlyRecurrence")]
    public MonthlyRecurrence? MonthlyRecurrence { get; set; }

    /// <summary>
    /// Gets or Sets MonthlyDOWRecurrence
    /// </summary>
    [DataMember(Name="MonthlyDOWRecurrence", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "MonthlyDOWRecurrence")]
    public MonthlyDOWRecurrence? MonthlyDOWRecurrence { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class ScheduleRecurrence {\n");
      sb.Append("  MinuteRecurrence: ").Append(MinuteRecurrence).Append("\n");
      sb.Append("  DailyRecurrence: ").Append(DailyRecurrence).Append("\n");
      sb.Append("  WeeklyRecurrence: ").Append(WeeklyRecurrence).Append("\n");
      sb.Append("  MonthlyRecurrence: ").Append(MonthlyRecurrence).Append("\n");
      sb.Append("  MonthlyDOWRecurrence: ").Append(MonthlyDOWRecurrence).Append("\n");
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
