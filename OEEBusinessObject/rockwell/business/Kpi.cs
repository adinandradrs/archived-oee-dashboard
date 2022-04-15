using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OEEBusinessObject.rockwell.business
{
    public class Kpi
    {
        public int cAvailable { set; get; }
        public int cPerformance { set; get; }
        public int cQuality { set; get; }
        public int wAvailable { set; get; }
        public int wPerformance { set; get; }
        public int wQuality { set; get; }
    }
}
