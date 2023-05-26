using System.ComponentModel;

namespace Sonrai.ExtRS.Models.Enums
{
    public enum Iso800ListsType
    {
        Rfc822Date = 1, // regex
        [Description("Here is another")]
        HereIsAnother = 2,
        [Description("Last one")]
        LastOne = 3
    }
}