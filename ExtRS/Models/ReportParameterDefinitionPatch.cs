using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace IO.Swagger.Model {

  /// <summary>
  /// An object that specifies the parts of a report parameter that can be modified outside of the RDL definition.
  /// </summary>
  [DataContract]
  public class ReportParameterDefinitionPatch {
    /// <summary>
    /// A string value that specifies the name of the ReportParameter.
    /// </summary>
    /// <value>A string value that specifies the name of the ReportParameter.</value>
    [DataMember(Name="Name", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Name")]
    public string Name { get; set; }

    /// <summary>
    /// Array of default values
    /// </summary>
    /// <value>Array of default values</value>
    [DataMember(Name="DefaultValues", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "DefaultValues")]
    public List<string> DefaultValues { get; set; }

    /// <summary>
    /// A string value that specifies text used to prompt a user for the value of the ReportParameter.
    /// </summary>
    /// <value>A string value that specifies text used to prompt a user for the value of the ReportParameter.</value>
    [DataMember(Name="Prompt", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Prompt")]
    public string Prompt { get; set; }

    /// <summary>
    /// Gets or Sets ParameterVisibility
    /// </summary>
    [DataMember(Name="ParameterVisibility", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "ParameterVisibility")]
    public ReportParameterVisibility ParameterVisibility { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class ReportParameterDefinitionPatch {\n");
      sb.Append("  Name: ").Append(Name).Append("\n");
      sb.Append("  DefaultValues: ").Append(DefaultValues).Append("\n");
      sb.Append("  Prompt: ").Append(Prompt).Append("\n");
      sb.Append("  ParameterVisibility: ").Append(ParameterVisibility).Append("\n");
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
