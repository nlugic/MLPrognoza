using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLPrognoza.Data.Models
{
    public class WeatherModel
    {

        public DateTime Time { get; set; } // vreme merenja
        public int WindSpeed { get; set; } // brzina vetra u m/s
        public int Temperature { get; set; } // temperatura u C
        public int DewPointTemperature { get; set; } // temperatura kondenzacije u C
        public float AtmosphericPressure { get; set; } // atmosferski pritisak
        public float[] Precipitation { get; set; } // padavine u mm poslednjih 1/6/12/24 sata
        public float SnowDepth { get; set; } // dubina snega u mm

        public WeatherModel(WeatherRecord record)
        {
            Time = record.ObservationTime;
            WindSpeed = record.WindSpeed ?? 0;
            Temperature = record.Temperature ?? 0;
            DewPointTemperature = record.DewPointTemperature ?? 0;

            // uzimamo tacan pritisak sa mesta stanice ili pretpostavljam na neki nacin aproksimiranu vrednost
            AtmosphericPressure = record.StationPressure ?? (record.AltimeterPressure ?? 0.0f);

            Precipitation = new float[4];
            Precipitation[0] = record.PrecipitationLastHour ?? 0.0f;
            Precipitation[1] = record.PrecipitationLast6Hours ?? 0.0f;
            Precipitation[2] = record.PrecipitationLast12Hours ?? 0.0f;
            Precipitation[3] = record.PrecipitationLast24Hours ?? 0.0f;

            SnowDepth = record.SnowDepth ?? 0.0f;
        }

    }
}
