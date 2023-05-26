using System.ComponentModel;

namespace Sonrai.ExtRS.Models.Enums
{
    public enum SkateboardTricks
    {
        [Description("Ollie")]
        Ollie = 1,
        [Description("Shuvit")]
        Shuvit = 2,
        [Description("Pop-Shuvit")]
        PopShuvit = 3,
        [Description("Front-side 180")]
        Fs180 = 4,
        [Description("Back-side 180")]
        Bs180 = 5,
        [Description("Varial Flip")]
        VarialFlip = 6,
    }
}
