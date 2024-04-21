using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ReportingServices.Api.Models {

  /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class ODataKpis {
    /// <summary>
    /// Gets or Sets OdataContext
    /// </summary>
    [DataMember(Name="@odata.context", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "@odata.context")]
    public string OdataContext { get; set; }

    /// <summary>
    /// Gets or Sets OdataCount
    /// </summary>
    [DataMember(Name="@odata.count", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "@odata.count")]
    public int? OdataCount { get; set; }

    /// <summary>
    /// Gets or Sets Value
    /// </summary>
    [DataMember(Name="value", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "value")]
    public List<Kpi> Value { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class ODataKpis {\n");
      sb.Append("  OdataContext: ").Append(OdataContext).Append("\n");
      sb.Append("  OdataCount: ").Append(OdataCount).Append("\n");
      sb.Append("  Value: ").Append(Value).Append("\n");
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
