using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ReportingServices.Api.Models {

  /// <summary>
  /// An object that specifies the metadata definition of a parameter for a report.
  /// </summary>
  [DataContract]
  public class ReportParameterDefinition {
    /// <summary>
    /// A boolean value that indicates whether the ReportParamter is allowed to be blank.
    /// </summary>
    /// <value>A boolean value that indicates whether the ReportParamter is allowed to be blank.</value>
    [DataMember(Name="AllowBlank", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "AllowBlank")]
    public bool? AllowBlank { get; set; }

    /// <summary>
    /// An array of string values that specify the ReportParameter's default values. If the parameter is multi-valued then the array can have more than one entry.
    /// </summary>
    /// <value>An array of string values that specify the ReportParameter's default values. If the parameter is multi-valued then the array can have more than one entry.</value>
    [DataMember(Name="DefaultValues", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "DefaultValues")]
    public List<string> DefaultValues { get; set; }

    /// <summary>
    ///  A boolean value that indicates whether the DefaultValues property is NULL.
    /// </summary>
    /// <value> A boolean value that indicates whether the DefaultValues property is NULL.</value>
    [DataMember(Name="DefaultValuesIsNull", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "DefaultValuesIsNull")]
    public bool? DefaultValuesIsNull { get; set; }

    /// <summary>
    /// A boolean value that indicates whether the ReportParamter's default values are obtained from a query (instead of being static specified values).
    /// </summary>
    /// <value>A boolean value that indicates whether the ReportParamter's default values are obtained from a query (instead of being static specified values).</value>
    [DataMember(Name="DefaultValuesQueryBased", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "DefaultValuesQueryBased")]
    public bool? DefaultValuesQueryBased { get; set; }

    /// <summary>
    /// An array of string values that specify the dependencies for the ReportParameter.
    /// </summary>
    /// <value>An array of string values that specify the dependencies for the ReportParameter.</value>
    [DataMember(Name="Dependencies", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Dependencies")]
    public List<string> Dependencies { get; set; }

    /// <summary>
    /// Error returned when validating parameters.
    /// </summary>
    /// <value>Error returned when validating parameters.</value>
    [DataMember(Name="ErrorMessage", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "ErrorMessage")]
    public string ErrorMessage { get; set; }

    /// <summary>
    /// A boolean value that indicates whether the ReportParameter is multivalued.
    /// </summary>
    /// <value>A boolean value that indicates whether the ReportParameter is multivalued.</value>
    [DataMember(Name="MultiValue", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "MultiValue")]
    public bool? MultiValue { get; set; }

    /// <summary>
    /// A string value that specifies the name for the ReportParameter. This name will typically be displayed in the user interface.
    /// </summary>
    /// <value>A string value that specifies the name for the ReportParameter. This name will typically be displayed in the user interface.</value>
    [DataMember(Name="Name", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Name")]
    public string Name { get; set; }

    /// <summary>
    /// A boolean value that indicates whether the ReportParameter is allowed to be null.
    /// </summary>
    /// <value>A boolean value that indicates whether the ReportParameter is allowed to be null.</value>
    [DataMember(Name="Nullable", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Nullable")]
    public bool? Nullable { get; set; }

    /// <summary>
    /// Gets or Sets ParameterState
    /// </summary>
    [DataMember(Name="ParameterState", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "ParameterState")]
    public ReportParameterState ParameterState { get; set; }

    /// <summary>
    /// Gets or Sets ParameterType
    /// </summary>
    [DataMember(Name="ParameterType", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "ParameterType")]
    public ReportParameterType ParameterType { get; set; }

    /// <summary>
    /// Gets or Sets ParameterVisibility
    /// </summary>
    [DataMember(Name="ParameterVisibility", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "ParameterVisibility")]
    public ReportParameterVisibility ParameterVisibility { get; set; }

    /// <summary>
    /// A string value that specifies text used to prompt a user for the value of the ReportParameter.
    /// </summary>
    /// <value>A string value that specifies text used to prompt a user for the value of the ReportParameter.</value>
    [DataMember(Name="Prompt", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Prompt")]
    public string Prompt { get; set; }

    /// <summary>
    /// A boolean value that indicates whether the user should be prompted for the value for the ReportParameter.
    /// </summary>
    /// <value>A boolean value that indicates whether the user should be prompted for the value for the ReportParameter.</value>
    [DataMember(Name="PromptUser", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "PromptUser")]
    public bool? PromptUser { get; set; }

    /// <summary>
    /// A boolean value that indicates whether the ReportParamter is query based.
    /// </summary>
    /// <value>A boolean value that indicates whether the ReportParamter is query based.</value>
    [DataMember(Name="QueryParameter", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "QueryParameter")]
    public bool? QueryParameter { get; set; }

    /// <summary>
    /// Gets or Sets ValidValues
    /// </summary>
    [DataMember(Name="ValidValues", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "ValidValues")]
    public List<ValidValue> ValidValues { get; set; }

    /// <summary>
    /// A boolean value that indicates whether the ValidValues property is NULL.
    /// </summary>
    /// <value>A boolean value that indicates whether the ValidValues property is NULL.</value>
    [DataMember(Name="ValidValuesIsNull", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "ValidValuesIsNull")]
    public bool? ValidValuesIsNull { get; set; }

    /// <summary>
    /// A boolean value that indicates whether the ReportParameter's valid values are obtained from a query (instead of being static specified values).
    /// </summary>
    /// <value>A boolean value that indicates whether the ReportParameter's valid values are obtained from a query (instead of being static specified values).</value>
    [DataMember(Name="ValidValuesQueryBased", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "ValidValuesQueryBased")]
    public bool? ValidValuesQueryBased { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class ReportParameterDefinition {\n");
      sb.Append("  AllowBlank: ").Append(AllowBlank).Append("\n");
      sb.Append("  DefaultValues: ").Append(DefaultValues).Append("\n");
      sb.Append("  DefaultValuesIsNull: ").Append(DefaultValuesIsNull).Append("\n");
      sb.Append("  DefaultValuesQueryBased: ").Append(DefaultValuesQueryBased).Append("\n");
      sb.Append("  Dependencies: ").Append(Dependencies).Append("\n");
      sb.Append("  ErrorMessage: ").Append(ErrorMessage).Append("\n");
      sb.Append("  MultiValue: ").Append(MultiValue).Append("\n");
      sb.Append("  Name: ").Append(Name).Append("\n");
      sb.Append("  Nullable: ").Append(Nullable).Append("\n");
      sb.Append("  ParameterState: ").Append(ParameterState).Append("\n");
      sb.Append("  ParameterType: ").Append(ParameterType).Append("\n");
      sb.Append("  ParameterVisibility: ").Append(ParameterVisibility).Append("\n");
      sb.Append("  Prompt: ").Append(Prompt).Append("\n");
      sb.Append("  PromptUser: ").Append(PromptUser).Append("\n");
      sb.Append("  QueryParameter: ").Append(QueryParameter).Append("\n");
      sb.Append("  ValidValues: ").Append(ValidValues).Append("\n");
      sb.Append("  ValidValuesIsNull: ").Append(ValidValuesIsNull).Append("\n");
      sb.Append("  ValidValuesQueryBased: ").Append(ValidValuesQueryBased).Append("\n");
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
