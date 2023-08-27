using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace IO.Swagger.Model {

  /// <summary>
  /// An object that contains additional dataset properties for the MobileReportManifest
  /// </summary>
  [DataContract]
  public class DataSetItem {
    /// <summary>
    /// The time unit for the DataSetItem. The possible values for this string are the following: 'Year', 'Quarter', 'Month', 'Weekday', 'Hour'.
    /// </summary>
    /// <value>The time unit for the DataSetItem. The possible values for this string are the following: 'Year', 'Quarter', 'Month', 'Weekday', 'Hour'.</value>
    [DataMember(Name="TimeUnit", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "TimeUnit")]
    public string TimeUnit { get; set; }

    /// <summary>
    /// A string value that specifies the name of the column in the DataSetItem that represents date and time.
    /// </summary>
    /// <value>A string value that specifies the name of the column in the DataSetItem that represents date and time.</value>
    [DataMember(Name="DateTimeColumn", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "DateTimeColumn")]
    public string DateTimeColumn { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class DataSetItem {\n");
      sb.Append("  TimeUnit: ").Append(TimeUnit).Append("\n");
      sb.Append("  DateTimeColumn: ").Append(DateTimeColumn).Append("\n");
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
