using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ReportingServices.Api.Models {

  /// <summary>
  /// An object that specifies the current set of values for a KPI.
  /// </summary>
  [DataContract]
  public class KpiValues {
    /// <summary>
    /// A string value that specifies the value of the Value Property for the KPI.
    /// </summary>
    /// <value>A string value that specifies the value of the Value Property for the KPI.</value>
    [DataMember(Name="Value", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Value")]
    public string Value { get; set; }

    /// <summary>
    /// A Double value that specifies the value of the Goal Property for the KPI.
    /// </summary>
    /// <value>A Double value that specifies the value of the Goal Property for the KPI.</value>
    [DataMember(Name="Goal", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Goal")]
    public double? Goal { get; set; }

    /// <summary>
    /// A Double value that specifies the value of the Status Property for the KPI.
    /// </summary>
    /// <value>A Double value that specifies the value of the Status Property for the KPI.</value>
    [DataMember(Name="Status", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Status")]
    public double? Status { get; set; }

    /// <summary>
    /// An array of values that specifies the trendset for the KPI.
    /// </summary>
    /// <value>An array of values that specifies the trendset for the KPI.</value>
    [DataMember(Name="TrendSet", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "TrendSet")]
    public List<int?> TrendSet { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class KpiValues {\n");
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
