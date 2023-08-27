using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace IO.Swagger.Model {

  /// <summary>
  /// An object that has a reference to a history snapshot option for a given catalog.
  /// </summary>
  [DataContract]
  public class HistorySnapshotOptions {
    /// <summary>
    /// The Id of the CatalogItem that this HistorySnapshotOptions belongs to.
    /// </summary>
    /// <value>The Id of the CatalogItem that this HistorySnapshotOptions belongs to.</value>
    [DataMember(Name="CatalogItemId", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "CatalogItemId")]
    public Guid? CatalogItemId { get; set; }

    /// <summary>
    /// Gets or Sets HistorySnapshotsOptions
    /// </summary>
    [DataMember(Name="HistorySnapshotsOptions", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "HistorySnapshotsOptions")]
    public ReportHistorySnapshotsOptions HistorySnapshotsOptions { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class HistorySnapshotOptions {\n");
      sb.Append("  CatalogItemId: ").Append(CatalogItemId).Append("\n");
      sb.Append("  HistorySnapshotsOptions: ").Append(HistorySnapshotsOptions).Append("\n");
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
