using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ReportingServices.Api.Models
{
    /// <summary>
    /// Describes the week of the month on which a scheduled report runs.
    /// </summary>
    [DataContract]
    public enum WeekNumberEnum
    {
        FirstWeek,
        SecondWeek,
        ThirdWeek,
        FourthWeek,
        LastWeek
    }
    ///// <summary>
    ///// Get the string presentation of the object
    ///// </summary>
    ///// <returns>String presentation of the object</returns>
    //public override string ToString() {
    //    var sb = new StringBuilder();
    //    sb.Append("class WeekNumberEnum {\n");
    //    sb.Append("}\n");
    //    return sb.ToString();
    //}

    ///// <summary>
    ///// Get the JSON string presentation of the object
    ///// </summary>
    ///// <returns>JSON string presentation of the object</returns>
    //public string ToJson() {
    //    return JsonConvert.SerializeObject(this, Formatting.Indented);
    //}
}
