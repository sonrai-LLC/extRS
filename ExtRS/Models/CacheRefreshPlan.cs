using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace IO.Swagger.Model {

  /// <summary>
  /// An object that specifies a plan for data cache refresh. The plan can be stored and re-used by multiple CatalogItems.
  /// </summary>
  [DataContract]
  public class CacheRefreshPlan {
    /// <summary>
    /// A unique UUID value that specifies the identifier by which this CacheRefreshPlan can be referenced in the definition of other items.
    /// </summary>
    /// <value>A unique UUID value that specifies the identifier by which this CacheRefreshPlan can be referenced in the definition of other items.</value>
    [DataMember(Name="Id", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Id")]
    public Guid? Id { get; set; }

    /// <summary>
    /// A string value that specifies the owner of the CacheRefreshPlan.
    /// </summary>
    /// <value>A string value that specifies the owner of the CacheRefreshPlan.</value>
    [DataMember(Name="Owner", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Owner")]
    public string Owner { get; set; }

    /// <summary>
    /// A string value that contains descriptive text about the CacheRefreshPlan.
    /// </summary>
    /// <value>A string value that contains descriptive text about the CacheRefreshPlan.</value>
    [DataMember(Name="Description", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Description")]
    public string Description { get; set; }

    /// <summary>
    /// A string value that contains the fully qualified URL path location of the CatalogItem that represents the CacheRefreshPlan.
    /// </summary>
    /// <value>A string value that contains the fully qualified URL path location of the CatalogItem that represents the CacheRefreshPlan.</value>
    [DataMember(Name="CatalogItemPath", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "CatalogItemPath")]
    public string CatalogItemPath { get; set; }

    /// <summary>
    /// A string value that specifies which EventType to use for logging.
    /// </summary>
    /// <value>A string value that specifies which EventType to use for logging.</value>
    [DataMember(Name="EventType", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "EventType")]
    public string EventType { get; set; }

    /// <summary>
    /// Gets or Sets Schedule
    /// </summary>
    [DataMember(Name="Schedule", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "Schedule")]
    public ScheduleReference Schedule { get; set; }

    /// <summary>
    /// A date-time value that specifies the date-time of the last run of the CacheRefreshPlan.
    /// </summary>
    /// <value>A date-time value that specifies the date-time of the last run of the CacheRefreshPlan.</value>
    [DataMember(Name="LastRunTime", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "LastRunTime")]
    public DateTime? LastRunTime { get; set; }

    /// <summary>
    /// A string value that contains the network username of the last user to modify the CacheRefreshPlan.
    /// </summary>
    /// <value>A string value that contains the network username of the last user to modify the CacheRefreshPlan.</value>
    [DataMember(Name="LastStatus", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "LastStatus")]
    public string LastStatus { get; set; }

    /// <summary>
    /// A string value that contains the network user name of the last user to modify the CacheRefreshPlan
    /// </summary>
    /// <value>A string value that contains the network user name of the last user to modify the CacheRefreshPlan</value>
    [DataMember(Name="ModifiedBy", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "ModifiedBy")]
    public string ModifiedBy { get; set; }

    /// <summary>
    /// A string value that contains the date-time of the last modification to the CacheRefreshPlan.
    /// </summary>
    /// <value>A string value that contains the date-time of the last modification to the CacheRefreshPlan.</value>
    [DataMember(Name="ModifiedDate", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "ModifiedDate")]
    public DateTime? ModifiedDate { get; set; }

    /// <summary>
    /// An array of parameter values for the CacheRefreshPlan. All parameters without a default value MUST have a value specified.
    /// </summary>
    /// <value>An array of parameter values for the CacheRefreshPlan. All parameters without a default value MUST have a value specified.</value>
    [DataMember(Name="ParameterValues", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "ParameterValues")]
    public List<ParameterValue> ParameterValues { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class CacheRefreshPlan {\n");
      sb.Append("  Id: ").Append(Id).Append("\n");
      sb.Append("  Owner: ").Append(Owner).Append("\n");
      sb.Append("  Description: ").Append(Description).Append("\n");
      sb.Append("  CatalogItemPath: ").Append(CatalogItemPath).Append("\n");
      sb.Append("  EventType: ").Append(EventType).Append("\n");
      sb.Append("  Schedule: ").Append(Schedule).Append("\n");
      sb.Append("  LastRunTime: ").Append(LastRunTime).Append("\n");
      sb.Append("  LastStatus: ").Append(LastStatus).Append("\n");
      sb.Append("  ModifiedBy: ").Append(ModifiedBy).Append("\n");
      sb.Append("  ModifiedDate: ").Append(ModifiedDate).Append("\n");
      sb.Append("  ParameterValues: ").Append(ParameterValues).Append("\n");
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
