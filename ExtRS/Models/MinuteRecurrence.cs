using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ReportingServices.Api.Models {

  /// <summary>
  /// Represents the intervals at which a scheduled report runs. Intervals are specified in minutes.
  /// </summary>
  [DataContract]
  public class MinuteRecurrence {
    /// <summary>
    /// An Int32 value representing interval in minutes.
    /// </summary>
    /// <value>An Int32 value representing interval in minutes.</value>
    [DataMember(Name="MinutesInterval", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "MinutesInterval")]
    public int? MinutesInterval { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class MinuteRecurrence {\n");
      sb.Append("  MinutesInterval: ").Append(MinutesInterval).Append("\n");
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
