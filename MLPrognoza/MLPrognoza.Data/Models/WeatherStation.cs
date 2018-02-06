using FileHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLPrognoza.Data.Models
{
    [DelimitedRecord(",")]
    [IgnoreFirst()]
    [IgnoreEmptyLines()]
    public class WeatherStation
    {

        [FieldQuoted(QuoteMode.OptionalForBoth)]
        public string USAF { get; set; }

        [FieldQuoted(QuoteMode.OptionalForBoth)]
        public string WBAN { get; set; }

        [FieldQuoted(QuoteMode.OptionalForBoth)]
        public string Name { get; set; }

        [FieldQuoted(QuoteMode.OptionalForBoth)]
        public string Country { get; set; }

        [FieldQuoted(QuoteMode.OptionalForBoth)]
        public string State { get; set; }

        [FieldQuoted(QuoteMode.OptionalForBoth)]
        public string ICAO { get; set; }

        [FieldQuoted(QuoteMode.OptionalForBoth)]
        [FieldConverter(ConverterKind.Single)]
        public float? Latitude { get; set; }

        [FieldQuoted(QuoteMode.OptionalForBoth)]
        [FieldConverter(ConverterKind.Single)]
        public float? Longitude { get; set; }

        [FieldQuoted(QuoteMode.OptionalForBoth)]
        [FieldConverter(ConverterKind.Single)]
        public float? Elevation { get; set; }

        [FieldQuoted(QuoteMode.OptionalForBoth)]
        [FieldConverter(ConverterKind.Date, "yyyyMMdd")]
        public DateTime BeginDate { get; set; }

        [FieldQuoted(QuoteMode.OptionalForBoth)]
        [FieldConverter(ConverterKind.Date, "yyyyMMdd")]
        public DateTime EndDate { get; set; }

    }
}
