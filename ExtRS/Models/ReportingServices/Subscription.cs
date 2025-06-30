using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ReportingServices.Api.Models
{
    /// <summary>
    /// An object that defines a subscription. A Reporting Services subscription allows a user to subscribe to a Report or Data Source and then to automatically receive an update whenever the item is updated.
    /// </summary>
    [DataContract]
    public class Subscription
    {
        // the delivery schedule
        [DataMember(Name = "Schedule", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "Schedule")]
        public Schedule? Schedule { get; set; }

        // the uri of the RS catalog item
        public string? Uri { get; set; }

        /// <summary>
        /// A unique UUID value that specifies the identifier by which this Subscription can be referenced in requests or in other defined objects.
        /// </summary>
        /// <value>A unique UUID value that specifies the identifier by which this Subscription can be referenced in requests or in other defined objects.</value>
        [DataMember(Name = "Id", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "Id")]
        public Guid? Id { get; set; }

        /// <summary>
        /// A string value that specifies the owner of the Subscription.
        /// </summary>
        /// <value>A string value that specifies the owner of the Subscription.</value>
        [DataMember(Name = "Owner", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "Owner")]
        public string? Owner { get; set; }

        /// <summary>
        /// A boolean value that specifies whether the members of the distribution list for the subscription are computed based on data.
        /// </summary>
        /// <value>A boolean value that specifies whether the members of the distribution list for the subscription are computed based on data.</value>
        [DataMember(Name = "IsDataDriven", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "IsDataDriven")]
        public bool? IsDataDriven { get; set; }

        /// <summary>
        /// A string value that contains descriptive text about the Subscription.
        /// </summary>
        /// <value>A string value that contains descriptive text about the Subscription.</value>
        [DataMember(Name = "Description", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "Description")]
        public string? Description { get; set; }

        /// <summary>
        /// A string value that specifies the path of the report for this Subscription.
        /// </summary>
        /// <value>A string value that specifies the path of the report for this Subscription.</value>
        [DataMember(Name = "Report", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "Report")]
        public string? Report { get; set; }

        /// <summary>
        /// A boolean value that specifies whether the Subscription is currently active.
        /// </summary>
        /// <value>A boolean value that specifies whether the Subscription is currently active.</value>
        [DataMember(Name = "IsActive", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "IsActive")]
        public bool? IsActive { get; set; }

        /// <summary>
        /// A string specifying the type of event that triggers the Subscription.
        /// </summary>
        /// <value>A string specifying the type of event that triggers the Subscription.</value>
        [DataMember(Name = "EventType", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "EventType")]
        public string? EventType { get; set; }

        /// <summary>
        /// A string value that contains descriptive text about the schedule referenced in the Schedule property.
        /// </summary>
        /// <value>A string value that contains descriptive text about the schedule referenced in the Schedule property.</value>
        [DataMember(Name = "ScheduleDescription", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "ScheduleDescription")]
        public string? ScheduleDescription { get; set; }

        /// <summary>
        /// A string value that contains the date-time that the schedule was last run.
        /// </summary>
        /// <value>A string value that contains the date-time that the schedule was last run.</value>
        [DataMember(Name = "LastRunTime", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "LastRunTime")]
        public DateTime? LastRunTime { get; set; }

        /// <summary>
        /// A string specifying the Status of the last run.
        /// </summary>
        /// <value>A string specifying the Status of the last run.</value>
        [DataMember(Name = "LastStatus", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "LastStatus")]
        public string? LastStatus { get; set; }

        /// <summary>
        /// Gets or Sets ExtensionSettings
        /// </summary>
        [DataMember(Name = "ExtensionSettings", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "ExtensionSettings")]
        public ExtensionSettings? ExtensionSettings { get; set; }

        /// <summary>
        /// An object that specifies the DeliveryExtension that will be used with this Schedule's report delivery.
        /// </summary>
        /// <value>An object that specifies the DeliveryExtension that will be used with this Schedule's report delivery.</value>
        [DataMember(Name = "DeliveryExtension", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "DeliveryExtension")]
        public string? DeliveryExtension { get; set; }

        /// <summary>
        /// Localized version of extension name when available.
        /// </summary>
        /// <value>Localized version of extension name when available.</value>
        [DataMember(Name = "LocalizedDeliveryExtensionName", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "LocalizedDeliveryExtensionName")]
        public string? LocalizedDeliveryExtensionName { get; set; }

        /// <summary>
        /// A string value that contains the network user name of the last user to modify the subscription.
        /// </summary>
        /// <value>A string value that contains the network user name of the last user to modify the subscription.</value>
        [DataMember(Name = "ModifiedBy", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "ModifiedBy")]
        public string? ModifiedBy { get; set; }

        /// <summary>
        /// A string value that contains the date-time of the last modification to the subscription.
        /// </summary>
        /// <value>A string value that contains the date-time of the last modification to the subscription.</value>
        [DataMember(Name = "ModifiedDate", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "ModifiedDate")]
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// An array of items of type ParameterValue that specify the parameter values for the subscription.
        /// </summary>
        /// <value>An array of items of type ParameterValue that specify the parameter values for the subscription.</value>
        [DataMember(Name = "ParameterValues", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "ParameterValues")]
        public List<ParameterValue>? ParameterValues { get; set; }


        /// <summary>
        /// Get the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Subscription {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Owner: ").Append(Owner).Append("\n");
            sb.Append("  IsDataDriven: ").Append(IsDataDriven).Append("\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
            sb.Append("  Report: ").Append(Report).Append("\n");
            sb.Append("  IsActive: ").Append(IsActive).Append("\n");
            sb.Append("  EventType: ").Append(EventType).Append("\n");
            sb.Append("  ScheduleDescription: ").Append(ScheduleDescription).Append("\n");
            sb.Append("  LastRunTime: ").Append(LastRunTime).Append("\n");
            sb.Append("  LastStatus: ").Append(LastStatus).Append("\n");
            sb.Append("  ExtensionSettings: ").Append(ExtensionSettings).Append("\n");
            sb.Append("  DeliveryExtension: ").Append(DeliveryExtension).Append("\n");
            sb.Append("  LocalizedDeliveryExtensionName: ").Append(LocalizedDeliveryExtensionName).Append("\n");
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
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

    }
}
