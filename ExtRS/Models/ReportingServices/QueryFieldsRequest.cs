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
  public class QueryFieldsRequest {
    /// <summary>
    /// Gets or Sets DataSource
    /// </summary>
    [DataMember(Name="dataSource", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "dataSource")]
    public DataSource? DataSource { get; set; }

    /// <summary>
    /// Gets or Sets Query
    /// </summary>
    [DataMember(Name="query", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "query")]
    public Query? Query { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class QueryFieldsRequest {\n");
      sb.Append("  DataSource: ").Append(DataSource).Append("\n");
      sb.Append("  Query: ").Append(Query).Append("\n");
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
