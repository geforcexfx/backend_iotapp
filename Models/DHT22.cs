using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Iot_app.Models
{
    public class DHT22
    {
      
        public int id { get; set; }
        public float temperature { get; set; } 
        public float heat_index { get; set; } 
        public float dew_point { get; set; }
        public float humidity { get; set; } 
        public String comfort { get; set; } 
        public float light { get; set; }
        //public String Time { get; set; }
        public DateTime Time { get; set; } 


    }
}
