using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ReportingServices.Api.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class DataSet : CatalogItem
    {

        // the uri of the RS catalog item
        public string? Uri { get; set; }
        /// <summary>
        /// A boolean value that indicates whether the dataset definition contains parameters.
        /// </summary>
        /// <value>A boolean value that indicates whether the dataset definition contains parameters.</value>
        [DataMember(Name = "HasParameters", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "HasParameters")]
        public bool? HasParameters { get; set; }

        /// <summary>
        /// An Int32 value that indicates the query execution timeout in seconds.
        /// </summary>
        /// <value>An Int32 value that indicates the query execution timeout in seconds.</value>
        [DataMember(Name = "QueryExecutionTimeOut", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "QueryExecutionTimeOut")]
        public int? QueryExecutionTimeOut { get; set; }


        /// <summary>
        /// Get the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class DataSet {\n");
            sb.Append("  HasParameters: ").Append(HasParameters).Append("\n");
            sb.Append("  QueryExecutionTimeOut: ").Append(QueryExecutionTimeOut).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Get the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public new string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

    }
}
