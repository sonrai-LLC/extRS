using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ReportingServices.Api.Models {

  /// <summary>
  /// An object that specifies the metadata source for the component parts of a KPI.
  /// </summary>
  [DataContract]
  public class KpiData {
    /// <summary>
    /// Gets or Sets Value
    /// </summary>
    [DataMember(Name="Value", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Value")]
    public KpiDataItem Value { get; set; }

    /// <summary>
    /// Gets or Sets Goal
    /// </summary>
    [DataMember(Name="Goal", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Goal")]
    public KpiDataItem Goal { get; set; }

    /// <summary>
    /// Gets or Sets Status
    /// </summary>
    [DataMember(Name="Status", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Status")]
    public KpiDataItem Status { get; set; }

    /// <summary>
    /// Gets or Sets TrendSet
    /// </summary>
    [DataMember(Name="TrendSet", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "TrendSet")]
    public KpiDataItem TrendSet { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class KpiData {\n");
      sb.Append("  Value: ").Append(Value).Append("\n");
      sb.Append("  Goal: ").Append(Goal).Append("\n");
      sb.Append("  Status: ").Append(Status).Append("\n");
      sb.Append("  TrendSet: ").Append(TrendSet).Append("\n");
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
