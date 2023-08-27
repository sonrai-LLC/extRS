using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace IO.Swagger.Model {

  /// <summary>
  /// An object that specifies options for a Report HistorySnapshot.
  /// </summary>
  [DataContract]
  public class ReportHistorySnapshotsOptions {
    /// <summary>
    /// A boolean value that specifies whether manual snapshot creation is enabled for this HistorySnapshot.
    /// </summary>
    /// <value>A boolean value that specifies whether manual snapshot creation is enabled for this HistorySnapshot.</value>
    [DataMember(Name="ManualCreationEnabled", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "ManualCreationEnabled")]
    public bool? ManualCreationEnabled { get; set; }

    /// <summary>
    /// A Boolean value that specifies whether execution snapshots are kept.
    /// </summary>
    /// <value>A Boolean value that specifies whether execution snapshots are kept.</value>
    [DataMember(Name="KeepExecutionSnapshots", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "KeepExecutionSnapshots")]
    public bool? KeepExecutionSnapshots { get; set; }

    /// <summary>
    /// A boolean value that specifies whether the default system limit is used for this HistorySnapshot.
    /// </summary>
    /// <value>A boolean value that specifies whether the default system limit is used for this HistorySnapshot.</value>
    [DataMember(Name="UseDefaultSystemLimit", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "UseDefaultSystemLimit")]
    public bool? UseDefaultSystemLimit { get; set; }

    /// <summary>
    /// An Int32 value indicating how many snapshots to keep.
    /// </summary>
    /// <value>An Int32 value indicating how many snapshots to keep.</value>
    [DataMember(Name="ScopedLimit", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "ScopedLimit")]
    public int? ScopedLimit { get; set; }

    /// <summary>
    /// An Int32 value indicating how many snapshots can be kept systemwide.  Default (-1) is unlimited.
    /// </summary>
    /// <value>An Int32 value indicating how many snapshots can be kept systemwide.  Default (-1) is unlimited.</value>
    [DataMember(Name="SystemLimit", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "SystemLimit")]
    public int? SystemLimit { get; set; }

    /// <summary>
    /// Gets or Sets Schedule
    /// </summary>
    [DataMember(Name="Schedule", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Schedule")]
    public ReportHistorySnapshotsOptionsSchedule Schedule { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class ReportHistorySnapshotsOptions {\n");
      sb.Append("  ManualCreationEnabled: ").Append(ManualCreationEnabled).Append("\n");
      sb.Append("  KeepExecutionSnapshots: ").Append(KeepExecutionSnapshots).Append("\n");
      sb.Append("  UseDefaultSystemLimit: ").Append(UseDefaultSystemLimit).Append("\n");
      sb.Append("  ScopedLimit: ").Append(ScopedLimit).Append("\n");
      sb.Append("  SystemLimit: ").Append(SystemLimit).Append("\n");
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
