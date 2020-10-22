using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Iot_app.Data;
using Iot_app.Dtos;
using Iot_app.Models;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace Iot_app
{
    public class msgHub : Hub
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public msgHub(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task SendValue(String value)
        {
            Console.WriteLine(value);
            string res = value.Substring(7, 8);
            Console.WriteLine(res);
            if (res.Contains("bme"))
            {
                try { 
                var statJson = JsonConvert.DeserializeObject<AddBMEDto>(value);
                Console.WriteLine(statJson);
                BME680 bme = _mapper.Map<BME680>(statJson);
                System.Diagnostics.Debug.WriteLine(statJson.bmetemperature);
                bme.Time = DateTime.Now;
                Console.WriteLine(bme.Time);
                await _context.bME680s.AddAsync(bme);
                await _context.SaveChangesAsync();
                await Clients.All.SendAsync("ReceiveValue", value);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                try
                {
                    var statJson = JsonConvert.DeserializeObject<AddDataDto>(value);
                    DHT22 dht = _mapper.Map<DHT22>(statJson);
                    //serviceResponse.Data = dataPs.Select(d=> _mapper.Map<GetDataDto>(d)).ToList();
                    dht.Time = DateTime.Now;
                    Console.WriteLine(dht.Time);
                    await _context.dHT22s.AddAsync(dht);
                    await _context.SaveChangesAsync();
                    await Clients.All.SendAsync("ReceiveValue", value);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
            }
        }
    }
}
