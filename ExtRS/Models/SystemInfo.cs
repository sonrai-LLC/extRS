using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace IO.Swagger.Model {

  /// <summary>
  /// An object that specifies information about the Report Server.
  /// </summary>
  [DataContract]
  public class SystemInfo {
    /// <summary>
    /// A string value that specifies the URL for the Report Server.
    /// </summary>
    /// <value>A string value that specifies the URL for the Report Server.</value>
    [DataMember(Name="ReportServerAbsoluteUrl", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "ReportServerAbsoluteUrl")]
    public string ReportServerAbsoluteUrl { get; set; }

    /// <summary>
    /// A string value that specifies the Report Server Virtual Directory.
    /// </summary>
    /// <value>A string value that specifies the Report Server Virtual Directory.</value>
    [DataMember(Name="ReportServerRelativeUrl", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "ReportServerRelativeUrl")]
    public string ReportServerRelativeUrl { get; set; }

    /// <summary>
    /// A string value that specifies the URL for the Report Server web portal.
    /// </summary>
    /// <value>A string value that specifies the URL for the Report Server web portal.</value>
    [DataMember(Name="WebPortalRelativeUrl", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "WebPortalRelativeUrl")]
    public string WebPortalRelativeUrl { get; set; }

    /// <summary>
    /// A string value that specifies the name of the product being used.
    /// </summary>
    /// <value>A string value that specifies the name of the product being used.</value>
    [DataMember(Name="ProductName", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "ProductName")]
    public string ProductName { get; set; }

    /// <summary>
    /// A string value that specifies the version of the product being used.
    /// </summary>
    /// <value>A string value that specifies the version of the product being used.</value>
    [DataMember(Name="ProductVersion", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "ProductVersion")]
    public string ProductVersion { get; set; }

    /// <summary>
    /// A string value that specifies the type of the product being used.
    /// </summary>
    /// <value>A string value that specifies the type of the product being used.</value>
    [DataMember(Name="ProductType", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "ProductType")]
    public string ProductType { get; set; }

    /// <summary>
    /// A string value that specifies the timezone being used by the server.
    /// </summary>
    /// <value>A string value that specifies the timezone being used by the server.</value>
    [DataMember(Name="TimeZone", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "TimeZone")]
    public string TimeZone { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class SystemInfo {\n");
      sb.Append("  ReportServerAbsoluteUrl: ").Append(ReportServerAbsoluteUrl).Append("\n");
      sb.Append("  ReportServerRelativeUrl: ").Append(ReportServerRelativeUrl).Append("\n");
      sb.Append("  WebPortalRelativeUrl: ").Append(WebPortalRelativeUrl).Append("\n");
      sb.Append("  ProductName: ").Append(ProductName).Append("\n");
      sb.Append("  ProductVersion: ").Append(ProductVersion).Append("\n");
      sb.Append("  ProductType: ").Append(ProductType).Append("\n");
      sb.Append("  TimeZone: ").Append(TimeZone).Append("\n");
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
