using System.ComponentModel;

namespace Sonrai.ExtRS.Models.Enums
{
    public enum RenderFormats
    {
        [Description("PDF")]
        PDF,
        [Description("Excel")]
        Excel,
        [Description("TIFF")]
        TIFF,
        [Description("CSV")]
        CSV,
        [Description("XML")]
        XML,
        [Description("Data Feed")]
        DataFeed
    }
}
