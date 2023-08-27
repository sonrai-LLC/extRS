using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace IO.Swagger.Model {

  /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class Kpi : CatalogItem {
    /// <summary>
    /// Gets or Sets ValueFormat
    /// </summary>
    [DataMember(Name="ValueFormat", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "ValueFormat")]
    public KpiValueFormat ValueFormat { get; set; }

    /// <summary>
    /// Gets or Sets Visualization
    /// </summary>
    [DataMember(Name="Visualization", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Visualization")]
    public KpiVisualization Visualization { get; set; }

    /// <summary>
    /// Gets or Sets DrillthroughTarget
    /// </summary>
    [DataMember(Name="DrillthroughTarget", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "DrillthroughTarget")]
    public DrillthroughTarget DrillthroughTarget { get; set; }

    /// <summary>
    /// A string value that specifies the currency. Must follow the ISO 4217 Currency codes standard.
    /// </summary>
    /// <value>A string value that specifies the currency. Must follow the ISO 4217 Currency codes standard.</value>
    [DataMember(Name="Currency", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Currency")]
    public string Currency { get; set; }

    /// <summary>
    /// Gets or Sets Values
    /// </summary>
    [DataMember(Name="Values", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Values")]
    public KpiValues Values { get; set; }

    /// <summary>
    /// Gets or Sets Data
    /// </summary>
    [DataMember(Name="Data", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Data")]
    public KpiData Data { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class Kpi {\n");
      sb.Append("  ValueFormat: ").Append(ValueFormat).Append("\n");
      sb.Append("  Visualization: ").Append(Visualization).Append("\n");
      sb.Append("  DrillthroughTarget: ").Append(DrillthroughTarget).Append("\n");
      sb.Append("  Currency: ").Append(Currency).Append("\n");
      sb.Append("  Values: ").Append(Values).Append("\n");
      sb.Append("  Data: ").Append(Data).Append("\n");
      sb.Append("}\n");
      return sb.ToString();
    }

    /// <summary>
    /// Get the JSON string presentation of the object
    /// </summary>
    /// <returns>JSON string presentation of the object</returns>
    public  new string ToJson() {
      return JsonConvert.SerializeObject(this, Formatting.Indented);
    }

}
}
