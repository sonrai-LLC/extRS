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
  public class LinkedReport : CatalogItem {
    /// <summary>
    /// A boolean value that indicates whether the LinkedReport has parameters.
    /// </summary>
    /// <value>A boolean value that indicates whether the LinkedReport has parameters.</value>
    [DataMember(Name="HasParameters", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "HasParameters")]
    public bool? HasParameters { get; set; }

    /// <summary>
    ///  A string value that specifies the path to the report item that this linked report is linked to.
    /// </summary>
    /// <value> A string value that specifies the path to the report item that this linked report is linked to.</value>
    [DataMember(Name="Link", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Link")]
    public string Link { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class LinkedReport {\n");
      sb.Append("  HasParameters: ").Append(HasParameters).Append("\n");
      sb.Append("  Link: ").Append(Link).Append("\n");
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
