using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace IO.Swagger.Model {

  /// <summary>
  /// CatalogItem is an abstract type that contains the common properties of all the types of CatalogItems. Hence, it is the base type from which the other CatalogItem types are derived.
  /// </summary>
  [DataContract]
  public class CatalogItem {
    /// <summary>
    /// A unique UUID value that specifies the identifier by which this CatalogItem can be referenced in requests or by other defined objects.
    /// </summary>
    /// <value>A unique UUID value that specifies the identifier by which this CatalogItem can be referenced in requests or by other defined objects.</value>
    [DataMember(Name="Id", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Id")]
    public Guid? Id { get; set; }

    /// <summary>
    /// A string value that specifies the name for the CatalogItem. This name is typically displayed in the user interface.
    /// </summary>
    /// <value>A string value that specifies the name for the CatalogItem. This name is typically displayed in the user interface.</value>
    [DataMember(Name="Name", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Name")]
    public string Name { get; set; }

    /// <summary>
    /// A string value that contains descriptive text about the CatalogItem.
    /// </summary>
    /// <value>A string value that contains descriptive text about the CatalogItem.</value>
    [DataMember(Name="Description", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Description")]
    public string Description { get; set; }

    /// <summary>
    /// A string value that contains the full server path for the CatalogItem. Path is defined as an alternate key on the CatalogItem and can be used as the parameter by which CatalogItem can be referenced in requests or by other defined objects.
    /// </summary>
    /// <value>A string value that contains the full server path for the CatalogItem. Path is defined as an alternate key on the CatalogItem and can be used as the parameter by which CatalogItem can be referenced in requests or by other defined objects.</value>
    [DataMember(Name="Path", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Path")]
    public string Path { get; set; }

    /// <summary>
    /// Gets or Sets Type
    /// </summary>
    [DataMember(Name="Type", EmitDefaultValue=true)]
    [JsonProperty(PropertyName = "Type")]
    public string? Type { get; set; }

    /// <summary>
    /// A boolean value that indicates if the CatalogItem is hidden. If true, the item will generally not appear in listings of CatalogItems within the parent item.
    /// </summary>
    /// <value>A boolean value that indicates if the CatalogItem is hidden. If true, the item will generally not appear in listings of CatalogItems within the parent item.</value>
    [DataMember(Name="Hidden", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Hidden")]
    public bool? Hidden { get; set; }

    /// <summary>
    /// An Int64 value that contains the size of the CatalogItem in bytes.
    /// </summary>
    /// <value>An Int64 value that contains the size of the CatalogItem in bytes.</value>
    [DataMember(Name="Size", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Size")]
    public long? Size { get; set; }

    /// <summary>
    /// A string value that contains the network user name of the last user to modify the CatalogItem.
    /// </summary>
    /// <value>A string value that contains the network user name of the last user to modify the CatalogItem.</value>
    [DataMember(Name="ModifiedBy", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "ModifiedBy")]
    public string ModifiedBy { get; set; }

    /// <summary>
    /// A string value that contains the date-time of the last modification to the CatalogItem.
    /// </summary>
    /// <value>A string value that contains the date-time of the last modification to the CatalogItem.</value>
    [DataMember(Name="ModifiedDate", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "ModifiedDate")]
    public DateTime? ModifiedDate { get; set; }

    /// <summary>
    /// A string value that represents the network user name of the user who originally created the CatalogItem.
    /// </summary>
    /// <value>A string value that represents the network user name of the user who originally created the CatalogItem.</value>
    [DataMember(Name="CreatedBy", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "CreatedBy")]
    public string CreatedBy { get; set; }

    /// <summary>
    /// A string that contains the date-time of the creation of the CatalogItem.
    /// </summary>
    /// <value>A string that contains the date-time of the creation of the CatalogItem.</value>
    [DataMember(Name="CreatedDate", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "CreatedDate")]
    public DateTime? CreatedDate { get; set; }

    /// <summary>
    /// A unique UUID value that specifies the identifier of the Folder CatalogItem that contains this CatalogItem.
    /// </summary>
    /// <value>A unique UUID value that specifies the identifier of the Folder CatalogItem that contains this CatalogItem.</value>
    [DataMember(Name="ParentFolderId", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "ParentFolderId")]
    public Guid? ParentFolderId { get; set; }

    /// <summary>
    /// A string value that contains a SOAP MIME-type that is associated with the CatalogItem.
    /// </summary>
    /// <value>A string value that contains a SOAP MIME-type that is associated with the CatalogItem.</value>
    [DataMember(Name="ContentType", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "ContentType")]
    public string ContentType { get; set; }

    /// <summary>
    /// A string value that contains binary encoding by base64url encoding rules. The value of this property is not processed by the server. In object creation the server receives and stores a value, and in object retrieval the server returns the previously stored value.
    /// </summary>
    /// <value>A string value that contains binary encoding by base64url encoding rules. The value of this property is not processed by the server. In object creation the server receives and stores a value, and in object retrieval the server returns the previously stored value.</value>
    [DataMember(Name="Content", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Content")]
    public string Content { get; set; }

    /// <summary>
    /// A boolean value that specifies whether the CatalogItem is designated as a Favorite.
    /// </summary>
    /// <value>A boolean value that specifies whether the CatalogItem is designated as a Favorite.</value>
    [DataMember(Name="IsFavorite", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "IsFavorite")]
    public bool? IsFavorite { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class CatalogItem {\n");
      sb.Append("  Id: ").Append(Id).Append("\n");
      sb.Append("  Name: ").Append(Name).Append("\n");
      sb.Append("  Description: ").Append(Description).Append("\n");
      sb.Append("  Path: ").Append(Path).Append("\n");
      sb.Append("  Type: ").Append(Type).Append("\n");
      sb.Append("  Hidden: ").Append(Hidden).Append("\n");
      sb.Append("  Size: ").Append(Size).Append("\n");
      sb.Append("  ModifiedBy: ").Append(ModifiedBy).Append("\n");
      sb.Append("  ModifiedDate: ").Append(ModifiedDate).Append("\n");
      sb.Append("  CreatedBy: ").Append(CreatedBy).Append("\n");
      sb.Append("  CreatedDate: ").Append(CreatedDate).Append("\n");
      sb.Append("  ParentFolderId: ").Append(ParentFolderId).Append("\n");
      sb.Append("  ContentType: ").Append(ContentType).Append("\n");
      sb.Append("  Content: ").Append(Content).Append("\n");
      sb.Append("  IsFavorite: ").Append(IsFavorite).Append("\n");
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
