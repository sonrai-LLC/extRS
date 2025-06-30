using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ReportingServices.Api.Models {

  /// <summary>
  /// An object that contains information about the Report Server user.
  /// </summary>
  [DataContract]
  public class User {
    /// <summary>
    /// A unique UUID value that specifies the identifier by which this User object can be referenced in requests or in other defined objects.
    /// </summary>
    /// <value>A unique UUID value that specifies the identifier by which this User object can be referenced in requests or in other defined objects.</value>
    [DataMember(Name="Id", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Id")]
    public Guid? Id { get; set; }

    /// <summary>
    /// A string value that specifies the network user name for the user.
    /// </summary>
    /// <value>A string value that specifies the network user name for the user.</value>
    [DataMember(Name="Username", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Username")]
    public string? Username { get; set; }

    /// <summary>
    /// A string value that specifies the name to display for the network user.
    /// </summary>
    /// <value>A string value that specifies the name to display for the network user.</value>
    [DataMember(Name="DisplayName", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "DisplayName")]
    public string? DisplayName { get; set; }

    /// <summary>
    /// A boolean value that indicates whether the user has items that are designated as favorite items.
    /// </summary>
    /// <value>A boolean value that indicates whether the user has items that are designated as favorite items.</value>
    [DataMember(Name="HasFavoriteItems", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "HasFavoriteItems")]
    public bool? HasFavoriteItems { get; set; }

    /// <summary>
    /// A string value that specifies a path to a folder where a user's reports are stored by default. (This feature must be enabled by the server administrator).
    /// </summary>
    /// <value>A string value that specifies a path to a folder where a user's reports are stored by default. (This feature must be enabled by the server administrator).</value>
    [DataMember(Name="MyReportsPath", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "MyReportsPath")]
    public string? MyReportsPath { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class User {\n");
      sb.Append("  Id: ").Append(Id).Append("\n");
      sb.Append("  Username: ").Append(Username).Append("\n");
      sb.Append("  DisplayName: ").Append(DisplayName).Append("\n");
      sb.Append("  HasFavoriteItems: ").Append(HasFavoriteItems).Append("\n");
      sb.Append("  MyReportsPath: ").Append(MyReportsPath).Append("\n");
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
