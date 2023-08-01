using Newtonsoft.Json;
using NLog.Layouts;

namespace Sonrai.ExtRS.Models
{
    public class ReportParameterDefinitions
    {
        [JsonProperty("@odata.context")]
        public string context;
        public List<ReportParameterDefinition> value;
    }

    public class ReportParameterDefinition
    {
        public bool AllowBlank;
        public string[] DefaultValues;
        public bool DefaultValuesIsNull;
        public bool DefaultValuesQueryBased;
        public string[] Dependencies;
        public string ErrorMessage;
        public bool MultiValue;
        public string Name;
        public bool Nullable;
        public ReportParameterState ParameterState;
        public ReportParameterType ParameterType;
        public ReportParameterVisibility ParameterVisibility;
        public string Prompt;
        public bool PromptUser;
        public bool QueryParameter;
        public List<ValidValue> ValidValues;
        public bool ValidValuesIsNull;
        public bool ValidValuesQueryBased;
    }

    public class ParameterValue
    {
        public string Name;
        public string Value;
        public bool IsValueFieldReference;
    }

    public class ValidValue
    {
        public string Label;
        public string Value;
    }

    public class DataSetParameter
    {
        public string Name;
        public string Value;
    }

    public class DataSetParameterInfo
    {
        public string Name;
        public string DefaultValue;
        public bool Nullable;
        public ReportParameterType DataType;
        public bool IsExpression;
        public bool IsMultiValued;
    }

    public enum ReportParameterState
    {
        HasValidValue,
        MissingValidValue,
        HasOutstandingDependencies,
        DynamicValuesUnavailable
    }

    public enum ReportParameterType
    {
        Boolean,
        DateTime,
        Integer,
        Float,
        String
    }

    public enum ReportParameterVisibility
    {
        Visible,
        Hidden,
        Internal
    }
}
