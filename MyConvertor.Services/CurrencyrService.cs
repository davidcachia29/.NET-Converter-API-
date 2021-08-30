using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MyConvertor.Core;
using MyConvertor.Core.Models;
using MyConvertor.Core.Services;
using Newtonsoft.Json.Linq;

namespace MyConvertor.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly IUnitOfWork _unitOfWork;

        
        
        public CurrencyService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public string url = "https://api.exchangeratesapi.io/v1/latest?access_key=24398c31c37c2d91d8afd8153e00160e&format=1";

        public async Task<Currency> DisplayTargetDateAsync(int days)
        {
            return await _unitOfWork.Currencys.GetAllDataDate(days);
        }    

        public async Task<Currency> DisplayTargetDataAsync(string target)
        {
            return await _unitOfWork.Currencys.GetAllDataTarget(target);
        }

        public async Task<Currency> DisplayDataAsync()
        {
            return await _unitOfWork.Currencys.GetAllDataAsync();
        }
       

        public async Task<ConvertorDataGet> DisplayConvertDataAsync(ConvertorResource resource)
        {
            return await _unitOfWork.Currencys.DisplayConvertDataAsync(resource);
        }
    }
}