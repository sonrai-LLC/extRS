using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ReportingServices.Api.Models
{
    public class ReportExecutionStats
    {
        public readonly string UserName;
        public readonly string Report;
        public readonly int GeneratedNumber;
    }
}
