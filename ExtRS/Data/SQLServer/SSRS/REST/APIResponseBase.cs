using Newtonsoft.Json;
using System.Collections.Generic;

namespace ExtRS.Data.MSSQL.SSRS.REST
{
    public class GenericItem
    {
        [JsonProperty("Id")]
        public string Id { get; set; }
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Path")]
        public string Path { get; set; }
    }

    public abstract class APIResponseBase
    {
        [JsonProperty("@odata.context")]
        public string Context { get; set; }
        [JsonProperty("value")]
        public List<object> Value { get; set; }
    }

    public class APIGenericItemsResponse : APIResponseBase
    {
        [JsonProperty("value")]
        public List<GenericItem> GenericItems { get; set; }
    }

    public class APIGenericItemResponse : APIResponseBase
    {
        [JsonProperty("value")]
        public GenericItem GenericItem { get; set; }
    }

    //public class APIFoldersResponse : APIResponseBase
    //{
    //    [JsonProperty("value")]
    //    public List<Folder> Folders { get; set; }
    //}

    //public class APIFolderResponse : APIResponseBase
    //{
    //    [JsonProperty("value")]
    //    public Folder Folder { get; set; }
    //}

    //public class APIReportsResponse : APIResponseBase
    //{
    //    [JsonProperty("value")]
    //    public List<Report> Reports { get; set; }
    //}

    //public class APIReportResponse : APIResponseBase
    //{
    //    [JsonProperty("value")]
    //    public Report Report { get; set; }
    //}
}