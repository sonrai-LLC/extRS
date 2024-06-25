using System.ComponentModel;

namespace Sonrai.ExtRS.Models.Enums
{
    public enum RenderFormats
    {
        [Description("PDF")]
        PDF,
        [Description("EXCEL")]
        EXCELOPENXML,
        [Description("TIFF")]
        TIFF,
        [Description("CSV")]
        CSV,
        [Description("XML")]
        XML
    }
}
