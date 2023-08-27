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
  public class Report : CatalogItem {
    /// <summary>
    /// A boolean value that indicates whether the Report has DataSources associated with it.
    /// </summary>
    /// <value>A boolean value that indicates whether the Report has DataSources associated with it.</value>
    [DataMember(Name="HasDataSources", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "HasDataSources")]
    public bool? HasDataSources { get; set; }

    /// <summary>
    /// A boolean value that indicates whether the Report has shared DataSets associated with it.
    /// </summary>
    /// <value>A boolean value that indicates whether the Report has shared DataSets associated with it.</value>
    [DataMember(Name="HasSharedDataSets", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "HasSharedDataSets")]
    public bool? HasSharedDataSets { get; set; }

    /// <summary>
    /// A boolean value that indicates whether the Report has parameters.
    /// </summary>
    /// <value>A boolean value that indicates whether the Report has parameters.</value>
    [DataMember(Name="HasParameters", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "HasParameters")]
    public bool? HasParameters { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class Report {\n");
      sb.Append("  HasDataSources: ").Append(HasDataSources).Append("\n");
      sb.Append("  HasSharedDataSets: ").Append(HasSharedDataSets).Append("\n");
      sb.Append("  HasParameters: ").Append(HasParameters).Append("\n");
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
