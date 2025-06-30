using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ReportingServices.Api.Models {

  /// <summary>
  /// An object that contains either a reference to a defined schedule or the schedule details for the current context.
  /// </summary>
  [DataContract]
  public class ScheduleReference {
    /// <summary>
    /// The Id property of an existing schedule that will be used in the current context.
    /// </summary>
    /// <value>The Id property of an existing schedule that will be used in the current context.</value>
    [DataMember(Name="ScheduleId", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "ScheduleId")]
    public Guid? ScheduleId { get; set; }

    /// <summary>
    /// Gets or Sets Definition
    /// </summary>
    [DataMember(Name="Definition", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Definition")]
    public ScheduleDefinition? Definition { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class ScheduleReference {\n");
      sb.Append("  ScheduleId: ").Append(ScheduleId).Append("\n");
      sb.Append("  Definition: ").Append(Definition).Append("\n");
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
