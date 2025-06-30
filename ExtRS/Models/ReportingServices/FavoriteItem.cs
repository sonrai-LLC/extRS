using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ReportingServices.Api.Models {

  /// <summary>
  /// An object that a reference to a CatalogItem that the user has marked as a favorite.
  /// </summary>
  [DataContract]
  public class FavoriteItem {
    /// <summary>
    /// A unique UUID value that specifies the identifier of the CatalogItem that is marked as a favorite.
    /// </summary>
    /// <value>A unique UUID value that specifies the identifier of the CatalogItem that is marked as a favorite.</value>
    [DataMember(Name="Id", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Id")]
    public Guid? Id { get; set; }

    /// <summary>
    /// Gets or Sets Item
    /// </summary>
    [DataMember(Name="Item", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Item")]
    public CatalogItem? Item { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class FavoriteItem {\n");
      sb.Append("  Id: ").Append(Id).Append("\n");
      sb.Append("  Item: ").Append(Item).Append("\n");
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
