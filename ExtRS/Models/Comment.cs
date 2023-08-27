using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace IO.Swagger.Model {

  /// <summary>
  /// An object that specifies a comment that is attached to a CatalogItem.
  /// </summary>
  [DataContract]
  public class Comment {
    /// <summary>
    /// A unique UUID value that specifies the identifier of the comment.
    /// </summary>
    /// <value>A unique UUID value that specifies the identifier of the comment.</value>
    [DataMember(Name="Id", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Id")]
    public Guid? Id { get; set; }

    /// <summary>
    /// A unique UUID value that specifies the identifier of the CatalogItem item to which the comment is attached.
    /// </summary>
    /// <value>A unique UUID value that specifies the identifier of the CatalogItem item to which the comment is attached.</value>
    [DataMember(Name="ItemId", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "ItemId")]
    public Guid? ItemId { get; set; }

    /// <summary>
    ///  A string value that represents the user who created the comment item.
    /// </summary>
    /// <value> A string value that represents the user who created the comment item.</value>
    [DataMember(Name="UserName", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "UserName")]
    public string UserName { get; set; }

    /// <summary>
    /// A unique UUID value that specifies the identifier of the thread of the comment. A comment thread can be used to group comments that are a response to one another in one grouping.
    /// </summary>
    /// <value>A unique UUID value that specifies the identifier of the thread of the comment. A comment thread can be used to group comments that are a response to one another in one grouping.</value>
    [DataMember(Name="ThreadId", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "ThreadId")]
    public Guid? ThreadId { get; set; }

    /// <summary>
    /// A string value that specifies the server path to an attachment that is part of the comment.
    /// </summary>
    /// <value>A string value that specifies the server path to an attachment that is part of the comment.</value>
    [DataMember(Name="AttachmentPath", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "AttachmentPath")]
    public string AttachmentPath { get; set; }

    /// <summary>
    /// A string value that contains the text of the comment.
    /// </summary>
    /// <value>A string value that contains the text of the comment.</value>
    [DataMember(Name="Text", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Text")]
    public string Text { get; set; }

    /// <summary>
    /// A string that contains the date-time of the creation of the comment.
    /// </summary>
    /// <value>A string that contains the date-time of the creation of the comment.</value>
    [DataMember(Name="CreatedDate", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "CreatedDate")]
    public DateTime? CreatedDate { get; set; }

    /// <summary>
    /// A string value that contains the date-time of the last modification to the comment.
    /// </summary>
    /// <value>A string value that contains the date-time of the last modification to the comment.</value>
    [DataMember(Name="ModifiedDate", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "ModifiedDate")]
    public DateTime? ModifiedDate { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class Comment {\n");
      sb.Append("  Id: ").Append(Id).Append("\n");
      sb.Append("  ItemId: ").Append(ItemId).Append("\n");
      sb.Append("  UserName: ").Append(UserName).Append("\n");
      sb.Append("  ThreadId: ").Append(ThreadId).Append("\n");
      sb.Append("  AttachmentPath: ").Append(AttachmentPath).Append("\n");
      sb.Append("  Text: ").Append(Text).Append("\n");
      sb.Append("  CreatedDate: ").Append(CreatedDate).Append("\n");
      sb.Append("  ModifiedDate: ").Append(ModifiedDate).Append("\n");
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
