using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Iot_app.Dtos
{
    public class GetDataDto
    {
        public int id { get; set; }
        public float temperature { get; set; }
        public float heat_index { get; set; }
        public float dew_point { get; set; }
        public float humidity { get; set; }
        public String comfort { get; set; }
        public float light { get; set; }
        public DateTime Time { get; set; }

    }
}
