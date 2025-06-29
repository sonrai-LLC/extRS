using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ReportingServices.Api.Models {

  /// <summary>
  /// Represents the days of the month on which a scheduled report runs.
  /// </summary>
  [DataContract]
  public class MonthlyRecurrence {
    /// <summary>
    /// Specifies days for recurrence.
    /// </summary>
    /// <value>Specifies days for recurrence.</value>
    [DataMember(Name="Days", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Days")]
    public string? Days { get; set; }

    /// <summary>
    /// Gets or Sets MonthsOfYear
    /// </summary>
    [DataMember(Name="MonthsOfYear", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "MonthsOfYear")]
    public MonthsOfYearSelector? MonthsOfYear { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class MonthlyRecurrence {\n");
      sb.Append("  Days: ").Append(Days).Append("\n");
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
