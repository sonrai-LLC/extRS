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
    public class Me
    {
        /// <summary>
        /// A unique UUID value that specifies the identifier by which this defined item can be referenced in requests or in other defined objects.
        /// </summary>
        /// <value>A unique UUID value that specifies the identifier by which this defined item can be referenced in requests or in other defined objects.</value>
        [DataMember(Name = "Id", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "Id")]
        public Guid? Id { get; set; }

        [DataMember(Name = "Username", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "Username")]
        public string Username { get; set; }

        [DataMember(Name = "DisplayName", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "DisplayName")]
        public string DisplayName { get; set; }

        [DataMember(Name = "HasFavoriteItems", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "HasFavoriteItems")]
        public string HasFavoriteItems { get; set; }

        [DataMember(Name = "MyReportsPath", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "MyReportsPath")]
        public string MyReportsPath { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Me {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Username: ").Append(Username).Append("\n");
            sb.Append("  DisplayName: ").Append(DisplayName).Append("\n");
            sb.Append("  HasFavoriteItems: ").Append(HasFavoriteItems).Append("\n");
            sb.Append("  MyReportsPath: ").Append(MyReportsPath).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
