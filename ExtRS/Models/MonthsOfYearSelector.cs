using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace IO.Swagger.Model {

  /// <summary>
  /// Represents the months of the year in which a scheduled report runs
  /// </summary>
  [DataContract]
  public class MonthsOfYearSelector {
    /// <summary>
    /// Gets or Sets January
    /// </summary>
    [DataMember(Name="January", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "January")]
    public bool? January { get; set; }

    /// <summary>
    /// Gets or Sets February
    /// </summary>
    [DataMember(Name="February", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "February")]
    public bool? February { get; set; }

    /// <summary>
    /// Gets or Sets March
    /// </summary>
    [DataMember(Name="March", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "March")]
    public bool? March { get; set; }

    /// <summary>
    /// Gets or Sets April
    /// </summary>
    [DataMember(Name="April", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "April")]
    public bool? April { get; set; }

    /// <summary>
    /// Gets or Sets May
    /// </summary>
    [DataMember(Name="May", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "May")]
    public bool? May { get; set; }

    /// <summary>
    /// Gets or Sets June
    /// </summary>
    [DataMember(Name="June", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "June")]
    public bool? June { get; set; }

    /// <summary>
    /// Gets or Sets July
    /// </summary>
    [DataMember(Name="July", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "July")]
    public bool? July { get; set; }

    /// <summary>
    /// Gets or Sets August
    /// </summary>
    [DataMember(Name="August", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "August")]
    public bool? August { get; set; }

    /// <summary>
    /// Gets or Sets September
    /// </summary>
    [DataMember(Name="September", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "September")]
    public bool? September { get; set; }

    /// <summary>
    /// Gets or Sets October
    /// </summary>
    [DataMember(Name="October", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "October")]
    public bool? October { get; set; }

    /// <summary>
    /// Gets or Sets November
    /// </summary>
    [DataMember(Name="November", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "November")]
    public bool? November { get; set; }

    /// <summary>
    /// Gets or Sets December
    /// </summary>
    [DataMember(Name="December", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "December")]
    public bool? December { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class MonthsOfYearSelector {\n");
      sb.Append("  January: ").Append(January).Append("\n");
      sb.Append("  February: ").Append(February).Append("\n");
      sb.Append("  March: ").Append(March).Append("\n");
      sb.Append("  April: ").Append(April).Append("\n");
      sb.Append("  May: ").Append(May).Append("\n");
      sb.Append("  June: ").Append(June).Append("\n");
      sb.Append("  July: ").Append(July).Append("\n");
      sb.Append("  August: ").Append(August).Append("\n");
      sb.Append("  September: ").Append(September).Append("\n");
      sb.Append("  October: ").Append(October).Append("\n");
      sb.Append("  November: ").Append(November).Append("\n");
      sb.Append("  December: ").Append(December).Append("\n");
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
