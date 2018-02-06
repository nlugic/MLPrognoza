using FileHelpers;
using MLPrognoza.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLPrognoza.Data
{
    public class WeatherData
    {

        public static IEnumerable<WeatherStation> GetWeatherStationData(string fileName, string country)
        {
            FileHelperEngine<WeatherStation> engine = new FileHelperEngine<WeatherStation>();
            ICollection<WeatherStation> stationData = engine.ReadFile(fileName);
            
            return (from WeatherStation station in stationData
                    where station.Country == country
                    orderby station.Name ascending
                    select station).ToList();
        }

        public static IEnumerable<WeatherRecord> GetWeatherRecordsData(string fileName)
        {
            FileHelperEngine<WeatherRecord> engine = new FileHelperEngine<WeatherRecord>();
            ICollection<WeatherRecord> weatherData = engine.ReadFile(fileName);

            return weatherData;
        }
        
        public static ICollection<WeatherModel> GetWeatherModelData(ICollection<string> downloadedFiles)
        {
            ICollection<string> csvFileNames = new List<string>();
            foreach (string fileName in downloadedFiles)
                csvFileNames.Add(ISHParser.ConvertISHFile(fileName));

            List<WeatherRecord> weatherRecords = new List<WeatherRecord>();
            foreach (string csvFileName in csvFileNames)
                weatherRecords.AddRange(GetWeatherRecordsData(csvFileName));

            return (from WeatherRecord record in weatherRecords
                    select new WeatherModel(record)).ToList();
        }

    }
}
