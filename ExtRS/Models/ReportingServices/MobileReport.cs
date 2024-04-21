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
  public class MobileReport : CatalogItem {
    /// <summary>
    /// A boolean value that indicates whether the MobileReport allows caching.
    /// </summary>
    /// <value>A boolean value that indicates whether the MobileReport allows caching.</value>
    [DataMember(Name="AllowCaching", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "AllowCaching")]
    public bool? AllowCaching { get; set; }

    /// <summary>
    /// Gets or Sets Manifest
    /// </summary>
    [DataMember(Name="Manifest", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Manifest")]
    public MobileReportManifest Manifest { get; set; }

    /// <summary>
    /// A boolean value that indicates whether the MobileReport has shared DataSets associated with it.
    /// </summary>
    /// <value>A boolean value that indicates whether the MobileReport has shared DataSets associated with it.</value>
    [DataMember(Name="HasSharedDataSets", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "HasSharedDataSets")]
    public bool? HasSharedDataSets { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class MobileReport {\n");
      sb.Append("  AllowCaching: ").Append(AllowCaching).Append("\n");
      sb.Append("  Manifest: ").Append(Manifest).Append("\n");
      sb.Append("  HasSharedDataSets: ").Append(HasSharedDataSets).Append("\n");
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
