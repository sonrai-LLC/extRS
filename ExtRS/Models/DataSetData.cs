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
  public class DataSetData {
    /// <summary>
    /// Gets or Sets Name
    /// </summary>
    [DataMember(Name="Name", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Name")]
    public string Name { get; set; }

    /// <summary>
    /// Gets or Sets Rows
    /// </summary>
    [DataMember(Name="Rows", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Rows")]
    public List<List<string>> Rows { get; set; }

    /// <summary>
    /// Gets or Sets Columns
    /// </summary>
    [DataMember(Name="Columns", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Columns")]
    public List<DataSetColumns> Columns { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class DataSetData {\n");
      sb.Append("  Name: ").Append(Name).Append("\n");
      sb.Append("  Rows: ").Append(Rows).Append("\n");
      sb.Append("  Columns: ").Append(Columns).Append("\n");
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
