using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ReportingServices.Api.Models {

  /// <summary>
  /// Represents the days of the week on which a scheduled report runs.
  /// </summary>
  [DataContract]
  public class DaysOfWeekSelector {
    /// <summary>
    /// Gets or Sets Sunday
    /// </summary>
    [DataMember(Name="Sunday", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Sunday")]
    public bool Sunday { get; set; }

    /// <summary>
    /// Gets or Sets Monday
    /// </summary>
    [DataMember(Name="Monday", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Monday")]
    public bool Monday { get; set; }

    /// <summary>
    /// Gets or Sets Tuesday
    /// </summary>
    [DataMember(Name="Tuesday", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Tuesday")]
    public bool Tuesday { get; set; }

    /// <summary>
    /// Gets or Sets Wednesday
    /// </summary>
    [DataMember(Name="Wednesday", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Wednesday")]
    public bool Wednesday { get; set; }

    /// <summary>
    /// Gets or Sets Thursday
    /// </summary>
    [DataMember(Name="Thursday", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Thursday")]
    public bool Thursday { get; set; }

    /// <summary>
    /// Gets or Sets Friday
    /// </summary>
    [DataMember(Name="Friday", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Friday")]
    public bool Friday { get; set; }

    /// <summary>
    /// Gets or Sets Saturday
    /// </summary>
    [DataMember(Name="Saturday", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Saturday")]
    public bool Saturday { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class DaysOfWeekSelector {\n");
      sb.Append("  Sunday: ").Append(Sunday).Append("\n");
      sb.Append("  Monday: ").Append(Monday).Append("\n");
      sb.Append("  Tuesday: ").Append(Tuesday).Append("\n");
      sb.Append("  Wednesday: ").Append(Wednesday).Append("\n");
      sb.Append("  Thursday: ").Append(Thursday).Append("\n");
      sb.Append("  Friday: ").Append(Friday).Append("\n");
      sb.Append("  Saturday: ").Append(Saturday).Append("\n");
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
