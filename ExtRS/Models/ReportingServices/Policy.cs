using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ReportingServices.Api.Models {

  /// <summary>
  /// An object that specifies a set of roles that are grouped together as a policy and can then be assigned as a group of policies to an item using the property GroupUserName.
  /// </summary>
  [DataContract]
  public class Policy {
    /// <summary>
    /// A string value that specifies the name of the user or group to which the policy applies.
    /// </summary>
    /// <value>A string value that specifies the name of the user or group to which the policy applies.</value>
    [DataMember(Name="GroupUserName", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "GroupUserName")]
    public required string GroupUserName { get; set; }

    /// <summary>
    /// An array of objects of type Role that specify the security roles that are included in the Policy.
    /// </summary>
    /// <value>An array of objects of type Role that specify the security roles that are included in the Policy.</value>
    [DataMember(Name="Roles", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Roles")]
    public required List<Role> Roles { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class Policy {\n");
      sb.Append("  GroupUserName: ").Append(GroupUserName).Append("\n");
      sb.Append("  Roles: ").Append(Roles).Append("\n");
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
