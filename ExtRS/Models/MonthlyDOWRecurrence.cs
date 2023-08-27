using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace IO.Swagger.Model {

  /// <summary>
  /// Represents the dates on which a scheduled report runs, typically by month, week, and day of the week.
  /// </summary>
  [DataContract]
  public class MonthlyDOWRecurrence {
    /// <summary>
    /// Gets or Sets WhichWeek
    /// </summary>
    [DataMember(Name="WhichWeek", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "WhichWeek")]
    public WeekNumberEnum WhichWeek { get; set; }

    /// <summary>
    /// Specifies whether week is specified
    /// </summary>
    /// <value>Specifies whether week is specified</value>
    [DataMember(Name="WhichWeekSpecified", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "WhichWeekSpecified")]
    public bool? WhichWeekSpecified { get; set; }

    /// <summary>
    /// Gets or Sets DaysOfWeek
    /// </summary>
    [DataMember(Name="DaysOfWeek", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "DaysOfWeek")]
    public DaysOfWeekSelector DaysOfWeek { get; set; }

    /// <summary>
    /// Gets or Sets MonthsOfYear
    /// </summary>
    [DataMember(Name="MonthsOfYear", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "MonthsOfYear")]
    public MonthsOfYearSelector MonthsOfYear { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class MonthlyDOWRecurrence {\n");
      sb.Append("  WhichWeek: ").Append(WhichWeek).Append("\n");
      sb.Append("  WhichWeekSpecified: ").Append(WhichWeekSpecified).Append("\n");
      sb.Append("  DaysOfWeek: ").Append(DaysOfWeek).Append("\n");
      sb.Append("  MonthsOfYear: ").Append(MonthsOfYear).Append("\n");
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
