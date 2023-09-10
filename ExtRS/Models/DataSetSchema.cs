using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ReportingServices.Api.Models {

  /// <summary>
  /// An object that reprents the schema for a DataSet
  /// </summary>
  [DataContract]
  public class DataSetSchema {
    /// <summary>
    /// The name of the DataSet.
    /// </summary>
    /// <value>The name of the DataSet.</value>
    [DataMember(Name="Name", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Name")]
    public string Name { get; set; }

    /// <summary>
    /// The fields of the DataSet.
    /// </summary>
    /// <value>The fields of the DataSet.</value>
    [DataMember(Name="Fields", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Fields")]
    public List<DataSetField> Fields { get; set; }

    /// <summary>
    /// The parameters for the DataSet.
    /// </summary>
    /// <value>The parameters for the DataSet.</value>
    [DataMember(Name="Parameters", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Parameters")]
    public List<DataSetParameterInfo> Parameters { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class DataSetSchema {\n");
      sb.Append("  Name: ").Append(Name).Append("\n");
      sb.Append("  Fields: ").Append(Fields).Append("\n");
      sb.Append("  Parameters: ").Append(Parameters).Append("\n");
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
