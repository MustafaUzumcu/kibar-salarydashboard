using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SD.MvcWebUI.Models
{
    public class SystemParameterViewModel
    {
        public int ParameterId { get; set; }

        public string ParameterName { get; set; }

        public string ParameterValue { get; set; }

        public string Description { get; set; }

        public bool IsReadOnly { get; set; }
    }
}
