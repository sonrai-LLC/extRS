using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ReportingServices.Api.Models;

namespace ExtRS.Models.ReportingServices
{

    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class ODataCatalogItemPolicies
    {
        /// <summary>
        /// Gets or Sets OdataContext
        /// </summary>
        [DataMember(Name = "@odata.context", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "@odata.context")]
        public string ODataContext { get; set; }

        /// <summary>
        /// Gets or Sets OdataCount
        /// </summary>
        //[DataMember(Name = "@odata.count", EmitDefaultValue = false)]
        //[JsonProperty(PropertyName = "@odata.count")]
        //public int? ODataCount { get; set; }

        ///// <summary>
        ///// Gets or Sets Value
        ///// </summary>
        //[DataMember(Name = "Policies", EmitDefaultValue = false)]
        //[JsonProperty(PropertyName = "Policies")]
        //public List<Policy> Policies { get; set; }

        /// <summary>
        /// Gets or Sets Value
        /// </summary>
        [DataMember(Name = "Policies", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "Policies")]
        public List<Policy> Policies { get; set; }

        [DataMember(Name = "InheritParentPolicy", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "InheritParentPolicy")]
        public bool InheritParentPolicy { get; set; }

        [DataMember(Name = "Id", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "Id")]
        public string Id { get; set; }

        //InheritParentPolicy

        /// <summary>
        /// Get the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ODataCatalogItemPolicies {\n");
            sb.Append("  OdataContext: ").Append(ODataContext).Append("\n");
            // sb.Append("  OdataCount: ").Append(ODataCount).Append("\n");
            sb.Append("  Policies: ").Append(Policies).Append("\n");
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
