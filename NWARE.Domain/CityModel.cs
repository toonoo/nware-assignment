using System;
using System.Collections.Generic;
using System.Text;

namespace NWARE.Domain
{
    public class CityModel
    {
        public string name { get; set; }
        public string country { get; set; }

        public StatModel stat { get; set; }
        public int population => (int)(stat?.population ?? 0);
    }

    public class StatModel
    {
        public string name { get; set; }
        public double population { get; set; }
    }
}
