using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyConvertor.Core.Models;
using MyConvertor.Core.Repositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MyConvertor.Data.Repositories
{
    public class ConvertorRepository : Repository<Currency>, ICurrencyRepository
    {
        public Currency finalData;
        private ConvertorDataGet finalData1;

        string url = "http://api.exchangeratesapi.io/";
        public ConvertorRepository(MyCurrencyDbContext context) 
            : base(context)
        { }      

        public async Task<Currency> GetAllDataAsync()

        {
            finalData = new Currency();

            //Using the HTTP Client
            using (var httpClient = new HttpClient())
            {
                //Setting the api link
                using (var response = await httpClient.GetAsync(url + "v1/latest?access_key=24398c31c37c2d91d8afd8153e00160e&format=1"))
                {
                    //Method to deserialise the json response of the api
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    JObject json = JObject.Parse(apiResponse);
                    DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(Currency));
                    byte[] byteArray = Encoding.UTF8.GetBytes(apiResponse);
                    MemoryStream stream = new MemoryStream(byteArray);

                    //The Final deserialised data
                    finalData = (Currency)deserializer.ReadObject(stream);

                    //Looping through all the data to save the results individualy
                    foreach (var val in json.Last.First)
                    {
                        finalData.rates.Add(val.ToString());
                    }


                }
            }
            //Return the final data
            return (finalData);
        }

        public async Task<Currency> GetAllDataDate(int days)
        {
            finalData = new Currency();

            //Setting The Date Variable
            DateTime thisDay = DateTime.Today;
            DateTime reducedDate = DateTime.Now.AddDays(-days);
            string month, day;

            //Making the data returned readable by the Api By adding a dd-mm-yyyy Format
            if (reducedDate.Month <= 9)
            {
                month = "0" + reducedDate.Month.ToString();
            }
            else
            {
                month = reducedDate.Month.ToString();
            }
            if (reducedDate.Day <= 9)
            {
                day = "0" + reducedDate.Day.ToString();
            }
            else
            {
                day = reducedDate.Day.ToString();
            }

            //Saving the correct reduced date
            string date = reducedDate.Year.ToString() + "-" + month + "-" + day;

            //Using the HTTP Client
            using (var httpClient = new HttpClient())
            {
                //Setting the api link
                using (var response = await httpClient.GetAsync(url + "/v1/" + date + "?access_key=24398c31c37c2d91d8afd8153e00160e&base=EUR"))
                {
                    //Method to deserialise the json response of the api
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    JObject json = JObject.Parse(apiResponse);
                    DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(Currency));
                    byte[] byteArray = Encoding.UTF8.GetBytes(apiResponse);
                    MemoryStream stream = new MemoryStream(byteArray);

                    //The Final deserialised data
                    finalData = (Currency)deserializer.ReadObject(stream);

                    //Making data error proof
                    foreach (var val in json.Last.First)
                    {
                        //If data shows error
                        if (json.First.ToString().Contains("false"))
                        {
                            //Show that an error occured
                            return (finalData);
                        }
                        else
                        {
                            //Add the data
                            finalData.rates.Add(val.ToString());
                        }
                    }
                }
            }
            //Return The data to the view
            return (finalData);
        }

        public async Task<Currency> GetAllDataTarget(string target)
        {
            finalData = new Currency();

            //Using the HTTP Client
            using (var httpClient = new HttpClient())
            {
                //Setting the api link
                using (var response = await httpClient.GetAsync(url + "v1/latest?access_key=24398c31c37c2d91d8afd8153e00160e&format=1&Base=" + target))
                {
                    //Method to deserialise the json response of the api
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    JObject json = JObject.Parse(apiResponse);
                    DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(Currency));
                    byte[] byteArray = Encoding.UTF8.GetBytes(apiResponse);
                    MemoryStream stream = new MemoryStream(byteArray);

                    //The Final deserialised data
                    finalData = (Currency)deserializer.ReadObject(stream);
                    //Populating the "target" data
                    finalData.@base = target;

                    //Checking if the data returned is not an error
                    foreach (var val in json.Last.First)
                    {
                        //Return final data which contains error
                        if (json.First.ToString().Contains("error"))
                        {
                            return (finalData);
                        }
                        else
                        //Add the data
                        {
                            finalData.rates.Add(val.ToString());
                        }
                    }
                }
            }
            //Return the data to the view
            return (finalData);
        }
        
        async Task<ConvertorDataGet> ICurrencyRepository.DisplayConvertDataAsync(ConvertorResource resource)
        {
            finalData1 = new ConvertorDataGet();

            //Using the HTTP Client
            using (var httpClient = new HttpClient())
            {
                //Setting the api link
                var values = new Dictionary<string, string>{
                    { "value", resource.Value.ToString() },
                    { "&Base", resource.fromCurrency },
                    { "symbols", resource.toCurrency },                    
                };

                var json1 = JsonConvert.SerializeObject(values, Formatting.Indented);

                var stringContent = new StringContent(json1);

               
                using (var response = await httpClient.PostAsync(url + "v1/latest?access_key=24398c31c37c2d91d8afd8153e00160e&symbols=" + resource.toCurrency + "&Base=" + resource.fromCurrency + "&format=1", stringContent))
                {
                    //Method to deserialise the json response of the api
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    JObject json = JObject.Parse(apiResponse);
                    DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(ConvertorDataGet));
                    byte[] byteArray = Encoding.UTF8.GetBytes(apiResponse);
                    MemoryStream stream = new MemoryStream(byteArray);

                    //The Final deserialised data
                    finalData1 = (ConvertorDataGet)deserializer.ReadObject(stream);

                    finalData1.toCurrency = resource.toCurrency;
                    finalData1.fromCurrency = resource.fromCurrency;
                    finalData1.Value = resource.Value;

                    //1 = 0.8
                    //5 = 
                    //Looping through all the data to save the results individualy
                    foreach (var val in json.Last.First)
                    {
                        float total = (float)val.Last;                        
                        finalData1.result = (resource.Value * total);
                    }


                }
            }
            //Return the final data
            return (finalData1);
        }       
    }
}