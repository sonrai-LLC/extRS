using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace IO.Swagger.Model {

  /// <summary>
  /// An object that contains information about a parameter in a DataSet.
  /// </summary>
  [DataContract]
  public class DataSetParameterInfo {
    /// <summary>
    /// The name of the parameter.
    /// </summary>
    /// <value>The name of the parameter.</value>
    [DataMember(Name="Name", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Name")]
    public string Name { get; set; }

    /// <summary>
    /// The default value for the parameter.
    /// </summary>
    /// <value>The default value for the parameter.</value>
    [DataMember(Name="DefaultValue", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "DefaultValue")]
    public string DefaultValue { get; set; }

    /// <summary>
    /// Specifies whether the parameter can be null.
    /// </summary>
    /// <value>Specifies whether the parameter can be null.</value>
    [DataMember(Name="Nullable", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Nullable")]
    public bool? Nullable { get; set; }

    /// <summary>
    /// Gets or Sets DataType
    /// </summary>
    [DataMember(Name="DataType", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "DataType")]
    public ReportParameterType DataType { get; set; }

    /// <summary>
    /// Specifies whether the parameter is an expression.
    /// </summary>
    /// <value>Specifies whether the parameter is an expression.</value>
    [DataMember(Name="IsExpression", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "IsExpression")]
    public bool? IsExpression { get; set; }

    /// <summary>
    /// Specifies whether the parameter contains multiple values.
    /// </summary>
    /// <value>Specifies whether the parameter contains multiple values.</value>
    [DataMember(Name="IsMultiValued", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "IsMultiValued")]
    public bool? IsMultiValued { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class DataSetParameterInfo {\n");
      sb.Append("  Name: ").Append(Name).Append("\n");
      sb.Append("  DefaultValue: ").Append(DefaultValue).Append("\n");
      sb.Append("  Nullable: ").Append(Nullable).Append("\n");
      sb.Append("  DataType: ").Append(DataType).Append("\n");
      sb.Append("  IsExpression: ").Append(IsExpression).Append("\n");
      sb.Append("  IsMultiValued: ").Append(IsMultiValued).Append("\n");
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
