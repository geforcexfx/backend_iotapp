using Iot_app.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Iot_app.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options){}
        public DbSet<DHT22> dHT22s { get; set; }
        public DbSet<BME680> bME680s  { get; set; }
    }
}
