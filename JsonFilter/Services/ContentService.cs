using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using JsonFilter.Models;


namespace JsonFilter.Services
{
    public class ContentService
    {

        public List<CarModel> cars;
        public ResultModel resultModel;

        //LOCATION
        public readonly string root = "wwwroot";
        public readonly string location = "data";
        public readonly string filename = "car.json";
        
        //This returns current data
        public List<CarModel> ReturnCarJson()
        {
            //Decupled data service 
            //THIS SERVICE WILL START ONCE AND THEN READ , WILL POPULATE FROM cars
            List<CarModel> cars = new List<CarModel>();
            cars = JsonConvert.DeserializeObject<List<CarModel>>(Read(filename, location));
            return cars;
        }

        //This downloads new data from link
        public async Task<List<CarModel>> JsonListDownloadAsync(string jsonurl)
        {
            try
            {
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Clear();
                var contentsUrl = jsonurl;
                var contentsJson = await httpClient.GetStringAsync(contentsUrl);
                try
                {
                    var content = JsonConvert.DeserializeObject<List<CarModel>>(contentsJson);
                    return content;
                }
                catch(ArgumentNullException ex)
                {
                    throw ex;
                }
                
            }
            catch 
            {
                throw new ArgumentException("That link isnt quite right");
            }
        }

        //this reads from the wwwroot folder the current data
        public string Read(string fileName, string location)
        {
            // This will Read the data from the root file
            var path = Path.Combine(
            Directory.GetCurrentDirectory(),
            root,
            location,
            fileName);

            string jsonResult;

            using (StreamReader streamReader = new StreamReader(path))
            {
                jsonResult = streamReader.ReadToEnd();
            }
            return jsonResult;
        }

        //this saves the ServiceContent
        public void Save()
        {
            //This will Write the data to the root file
            var path = Path.Combine(
            Directory.GetCurrentDirectory(),
            root,
            location,
            filename);

            using (var streamWriter = System.IO.File.CreateText(path))
            {
                streamWriter.Write(JsonConvert.SerializeObject(cars));
            }
        }
    }
}
