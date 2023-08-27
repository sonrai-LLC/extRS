using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace IO.Swagger.Model {

  /// <summary>
  /// An object that contains the specification of the contents of a mobile report.
  /// </summary>
  [DataContract]
  public class MobileReportManifest {
    /// <summary>
    /// An array of items of type ResourceGroup that specify the resources referenced in this MobileReport. A Resource is a generalized object and its content type is undefined.  A client must be able to understand the content returned in the Resource.
    /// </summary>
    /// <value>An array of items of type ResourceGroup that specify the resources referenced in this MobileReport. A Resource is a generalized object and its content type is undefined.  A client must be able to understand the content returned in the Resource.</value>
    [DataMember(Name="Resources", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Resources")]
    public List<ResourceGroup> Resources { get; set; }

    /// <summary>
    /// An array of objects of type DataSetItem that specifies the DataSets referenced in this MobileReport.
    /// </summary>
    /// <value>An array of objects of type DataSetItem that specifies the DataSets referenced in this MobileReport.</value>
    [DataMember(Name="DataSets", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "DataSets")]
    public List<DataSetItem> DataSets { get; set; }

    /// <summary>
    /// An array of items of type ThumbnailItem that contains the Thumbnails associated with the MobileReport.
    /// </summary>
    /// <value>An array of items of type ThumbnailItem that contains the Thumbnails associated with the MobileReport.</value>
    [DataMember(Name="Thumbnails", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Thumbnails")]
    public List<ThumbnailItem> Thumbnails { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class MobileReportManifest {\n");
      sb.Append("  Resources: ").Append(Resources).Append("\n");
      sb.Append("  DataSets: ").Append(DataSets).Append("\n");
      sb.Append("  Thumbnails: ").Append(Thumbnails).Append("\n");
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
