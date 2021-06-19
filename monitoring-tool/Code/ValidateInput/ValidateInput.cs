using System;
using System.ComponentModel.DataAnnotations;
namespace monitoring_tool
{
    public class ValidateInput //Necessary for input validation on diferent forms
    {
        [Range(0, 100)]
        public int MemoryThreshold { get; set; }

        [Range(0, 100)]
        public int CpuThreshold { get; set; }

        [Range(0, 100)]
        public int VolumeThreshold { get; set; }

        [EmailAddress]
        public string Email { get; set; }
    }
}
