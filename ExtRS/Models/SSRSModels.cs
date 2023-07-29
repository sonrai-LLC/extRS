using Newtonsoft.Json;

namespace Sonrai.ExtRS.Models
{
    public class SSRSConnection
    {
        public HttpClient HttpClient { get; set; }
        public string SqlAuthCookie;
        public string Administrator = "ExtRSAuth";
        public string serName;
        public string UserRole;
        public string ServerName;
        public AuthenticationType AuthenticationType;

        protected bool IsOnline = false;

        public SSRSConnection(string serverName, string adminUser, AuthenticationType authType = AuthenticationType.ExtRSAuth)
        {
            ServerName = serverName;
            Administrator = adminUser ?? Administrator;
            AuthenticationType = authType;
            HttpClient = new HttpClient();
        }
    }

    [JsonObject]
    public class CatalogItem
    {
        [JsonProperty("Id")]
        public string Id;
        [JsonProperty("Name")]
        public string Name;
        [JsonProperty("Description")]
        public string Description;
        [JsonProperty("Path")]
        public string Path;
        [JsonProperty("Type")]
        public string Type;
        [JsonProperty("Hidden")]
        public bool Hidden;
        [JsonProperty("Size")]
        public int Size;
        [JsonProperty("ModifiedBy")]
        public string ModifiedBy;
        [JsonProperty("ModifiedDate")]
        public string ModifiedDate;
        [JsonProperty("CreatedBy")]
        public string CreatedBy;
        [JsonProperty("CreatedDate")]
        public string CreatedDate;
        [JsonProperty("ParentFolderId")]
        public string ParentFolderId;
        [JsonProperty("IsFavorite")]
        public bool IsFavorite;
        [JsonProperty("ContentType")]
        public string ContentType;
        [JsonProperty("Content")]
        public string Content;
        [JsonProperty("Roles")]
        public string[] Roles;
    }

    public class CatalogItems
    {
        [JsonProperty("@odata.context")]
        public string ODataContext;
        [JsonProperty("value")]
        public List<CatalogItem> Value;
    }

    public class CatalogItemResponse
    {
        [JsonProperty("@odata.context")]
        public string ODataContext;
        [JsonProperty("value")]
        public List<CatalogItem> Value;
    }

    public class Report : CatalogItem
    {
        [JsonProperty("@odata.context")]
        public string ODataContext;
        [JsonProperty("HasDataSources")]
        public bool HasDataSources;
        [JsonProperty("HasSharedDataSets")]
        public bool HasSharedDataSets;
        [JsonProperty("HasParameters")]
        public bool HasParameters;
    }

    public class DataSet : CatalogItem
    {
        [JsonProperty("QueryExecutionTimeOut")]
        public bool QueryExecutionTimeOut;
        [JsonProperty("HasParameters")]
        public bool HasParameters;
    }

    public class Folder : CatalogItem
    {

    }

    public class ParameterValue
    {
        public string Name;
        public string Value;
        public bool IsValueFieldReference;
    }

    public enum ParameterState
    {
        HasValidValue, 
        MissingValidValue, 
        HasOutstandingDependencies, 
        DynamicValuesUnavailable
    }

    public enum ParameterType
    {
        Boolean,
        DateTime, 
        Integer, 
        Float, 
        String
    }

    public enum ParameterVisibility
    {
        Visible,
        Hidden,
        Internal
    }
    

    public class DataSource : CatalogItem
    {
        [JsonProperty("IsEnabled")]
        public bool IsEnabled;
        [JsonProperty("ConnectionString")]
        public string ConnectionString;
        [JsonProperty("DataSourceType")]
        public string DataSourceType;
        [JsonProperty("IsOriginalConnectionStringExpressionBased")]
        public bool IsOriginalConnectionStringExpressionBased;
        [JsonProperty("IsConnectionStringOverridden")]
        public bool IsConnectionStringOverridden;
        [JsonProperty("CredentialRetrieval")]
        public string CredentialRetrieval;
        [JsonProperty("IsReference")]
        public bool IsReference;
        [JsonProperty("DataSourceSubType")]
        public string DataSourceSubType;
        [JsonProperty("CredentialsByUser")]
        public string CredentialsByUser;
        [JsonProperty("CredentialsInServer")]
        public string CredentialsInServer;
    }

    public enum AuthenticationType
    {
        Windows,
        Forms,
        ExtRSAuth
    }
}
