using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace IO.Swagger.Model {

  /// <summary>
  /// Represents the intervals at which a scheduled report runs. Intervals are specified in days.
  /// </summary>
  [DataContract]
  public class DailyRecurrence {
    /// <summary>
    /// An Int32 value representing interval in days.
    /// </summary>
    /// <value>An Int32 value representing interval in days.</value>
    [DataMember(Name="DaysInterval", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "DaysInterval")]
    public int? DaysInterval { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class DailyRecurrence {\n");
      sb.Append("  DaysInterval: ").Append(DaysInterval).Append("\n");
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
