using Iot_app.Dtos;
using Iot_app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Iot_app.Services
{
    public interface IDataService
    {
        Task<ServiceResponse<List<GetDataDto>>> GetDataList();
        Task<ServiceResponse<List<GetBMEDto>>> GetBMEList();
        Task<ServiceResponse<List<GetBMEDto>>> GetPartBMEList(int length);
        Task<ServiceResponse<List<GetDataDto>>> GetDataDefinedList(int length);
        Task<ServiceResponse<GetDataDto>> GetData(int id);
        Task<ServiceResponse<List<GetDataDto>>> AddData(AddDataDto newData);
        Task<ServiceResponse<List<GetBMEDto>>> AddBMEData(AddBMEDto newData);
    }
}
