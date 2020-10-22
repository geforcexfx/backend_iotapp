using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Iot_app.Models
{
    public class BME680
    {
        public int id { get; set; }
        public float bmetemperature { get; set; }
        public float pressure { get; set; }
        public float gas { get; set; }
        public float humidity { get; set; }
        public float casio { get; set; }
        public float noaa { get; set; }
        public float wiki { get; set; }
        //public String Time { get; set; }
        public DateTime Time { get; set; }
    }
}
