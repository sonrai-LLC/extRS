using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ReportingServices.Api.Models
{

    /// <summary>
    /// An enumeration of values that specifies the type of the CatalogItem.
    /// </summary>
    [DataContract]
    public class CatalogItemType
    {
        /// <summary>
        /// Get the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
