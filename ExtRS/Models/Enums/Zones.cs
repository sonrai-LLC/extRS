﻿using System.ComponentModel;

namespace Sonrai.ExtRS.Models.Enums
{
    public enum Zones
    {
        Name1 = 1,
        [Description("Here is another")]
        HereIsAnother = 2,
        [Description("Last one")]
        LastOne = 3
    };
}
