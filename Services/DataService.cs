using AutoMapper;
using Iot_app.Data;
using Iot_app.Dtos;
using Iot_app.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Iot_app.Services
{
    public class DataService : IDataService
    {
        private static List<DHT22> dataPs = new List<DHT22> { new DHT22(),
             new DHT22 {id = 1, temperature = 90 } };
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public DataService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<ServiceResponse<List<GetDataDto>>> AddData(AddDataDto newData)
        {
            ServiceResponse<List<GetDataDto>> serviceResponse = new ServiceResponse<List<GetDataDto>>();
            //dataPs.Add(_mapper.Map<DHT22>(newData));
            DHT22 dht = _mapper.Map<DHT22>(newData);
            //serviceResponse.Data = dataPs.Select(d=> _mapper.Map<GetDataDto>(d)).ToList();
            Console.WriteLine(dht.Time);
            await _context.dHT22s.AddAsync(dht);
            await _context.SaveChangesAsync();
            serviceResponse.Data = (_context.dHT22s.Select(c => _mapper.Map<GetDataDto>(c))).ToList();
            return serviceResponse;
        }
        public async Task<ServiceResponse<List<GetBMEDto>>> AddBMEData(AddBMEDto newData)
        {
            ServiceResponse<List<GetBMEDto>> serviceResponse = new ServiceResponse<List<GetBMEDto>>();
            //dataPs.Add(_mapper.Map<DHT22>(newData));
            BME680 bme = _mapper.Map<BME680>(newData);
            //serviceResponse.Data = dataPs.Select(d=> _mapper.Map<GetDataDto>(d)).ToList();
            Console.WriteLine(bme.Time);
            await _context.bME680s.AddAsync(bme);
            await _context.SaveChangesAsync();
            serviceResponse.Data = (_context.bME680s.Select(c => _mapper.Map<GetBMEDto>(c))).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetDataDto>> GetData(int id)
        {
            ServiceResponse<GetDataDto> serviceResponse = new ServiceResponse<GetDataDto>();
            DHT22 dbDHT22 = await _context.dHT22s.FirstOrDefaultAsync(c => c.id == id);
            serviceResponse.Data = _mapper.Map<GetDataDto>(dbDHT22);
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetDataDto>>> GetDataDefinedList(int length)
        {
            ServiceResponse<List<GetDataDto>> serviceResponse = new ServiceResponse<List<GetDataDto>>();
            //List<DHT22> dbDHT22 = await _context.dHT22s.OrderByDescending(c => c).Take(length).ToListAsync();
            List<DHT22> dbDHT22 = await _context.dHT22s.FromSqlRaw("SELECT * FROM (SELECT * FROM sensors.dht22s ORDER BY id DESC LIMIT " + length + " ) sub").ToListAsync();
            serviceResponse.Data = dbDHT22.Select(d => _mapper.Map<GetDataDto>(d)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetDataDto>>> GetDataList()
        {
            ServiceResponse<List<GetDataDto>> serviceResponse = new ServiceResponse<List<GetDataDto>>();
            List<DHT22> dbDHT22 = await _context.dHT22s.OrderByDescending(c => c).Take(20).ToListAsync();
            serviceResponse.Data = dbDHT22.Select(d => _mapper.Map<GetDataDto>(d)).ToList();
            return serviceResponse;
        }
        public async Task<ServiceResponse<List<GetBMEDto>>> GetBMEList()
        {
            ServiceResponse<List<GetBMEDto>> serviceResponse = new ServiceResponse<List<GetBMEDto>>();
            List<BME680> bME680s = await _context.bME680s.OrderByDescending(c => c).Take(20).ToListAsync();
            serviceResponse.Data = bME680s.Select(d => _mapper.Map<GetBMEDto>(d)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetBMEDto>>> GetPartBMEList(int length)
        {
            ServiceResponse<List<GetBMEDto>> serviceResponse = new ServiceResponse<List<GetBMEDto>>();
            List<BME680> dbBME = await _context.bME680s.FromSqlRaw("SELECT * FROM (SELECT * FROM sensors.bme680s ORDER BY id DESC LIMIT "+length+" ) sub").ToListAsync();//.OrderByDescending(c => c).Take(length).ToListAsync();
            serviceResponse.Data = dbBME.Select(d => _mapper.Map<GetBMEDto>(d)).ToList();
            return serviceResponse;
        }
    }
}
