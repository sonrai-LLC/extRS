using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ReportingServices.Api.Models
{

    /// <summary>
    /// An object that contains history snapshot information.
    /// </summary>
    [DataContract]
    public class HistorySnapshot
    {
        public string? Uri { get; set; }
        /// <summary>
        /// A unique UUID value that specifies the identifier of the CatalogItem for which this is a HistorySnapshot.
        /// </summary>
        /// <value>A unique UUID value that specifies the identifier of the CatalogItem for which this is a HistorySnapshot.</value>
        [DataMember(Name = "Id", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "Id")]
        public Guid? Id { get; set; }

        /// <summary>
        /// A string that contains the date-time of the execution of the HistorySnapshot. This, together with the Id of the CatalogItem, identifies this HistorySnapshot
        /// </summary>
        /// <value>A string that contains the date-time of the execution of the HistorySnapshot. This, together with the Id of the CatalogItem, identifies this HistorySnapshot</value>
        [DataMember(Name = "HistoryId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "HistoryId")]
        public string? HistoryId { get; set; }

        /// <summary>
        /// A string that contains the date-time of the creation of the HistorySnapshot.
        /// </summary>
        /// <value>A string that contains the date-time of the creation of the HistorySnapshot.</value>
        [DataMember(Name = "CreationDate", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "CreationDate")]
        public DateTime? CreationDate { get; set; }

        /// <summary>
        /// Size of the HistorySnapshot.
        /// </summary>
        /// <value>Size of the HistorySnapshot.</value>
        [DataMember(Name = "Size", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "Size")]
        public int? Size { get; set; }


        /// <summary>
        /// Get the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class HistorySnapshot {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  HistoryId: ").Append(HistoryId).Append("\n");
            sb.Append("  CreationDate: ").Append(CreationDate).Append("\n");
            sb.Append("  Size: ").Append(Size).Append("\n");
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
