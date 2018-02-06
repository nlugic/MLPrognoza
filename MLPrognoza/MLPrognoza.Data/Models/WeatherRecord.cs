using FileHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLPrognoza.Data.Models
{
    [DelimitedRecord("\t")]
    [IgnoreFirst()]
    [IgnoreEmptyLines()]
    public class WeatherRecord
    {
        [FieldQuoted(QuoteMode.OptionalForBoth)]
        public string USAF { get; set; }

        [FieldQuoted(QuoteMode.OptionalForBoth)]
        public string WBAN { get; set; }

        [FieldQuoted(QuoteMode.OptionalForBoth)]
        [FieldConverter(ConverterKind.Date, "yyyyMMddHHmm")]
        public DateTime ObservationTime { get; set; }

        [FieldQuoted(QuoteMode.OptionalForBoth)]
        [FieldConverter(ConverterKind.Int32)]
        public int? WindDirecton { get; set; }

        [FieldQuoted(QuoteMode.OptionalForBoth)]
        [FieldConverter(ConverterKind.Int32)]
        public int? WindSpeed { get; set; }

        [FieldQuoted(QuoteMode.OptionalForBoth)]
        [FieldConverter(ConverterKind.Int32)]
        public int? WindGust { get; set; }

        [FieldQuoted(QuoteMode.OptionalForBoth)]
        [FieldConverter(ConverterKind.Int32)]
        public int? SkyCeilling { get; set; }

        [FieldQuoted(QuoteMode.OptionalForBoth)]
        public string SkyCoverCode { get; set; }

        [FieldQuoted(QuoteMode.OptionalForBoth)]
        public string SkyCondLowCode { get; set; }

        [FieldQuoted(QuoteMode.OptionalForBoth)]
        public string SkyCondMidCode { get; set; }

        [FieldQuoted(QuoteMode.OptionalForBoth)]
        public string SkyCondHighCode { get; set; }

        [FieldQuoted(QuoteMode.OptionalForBoth)]
        [FieldConverter(ConverterKind.Int32)]
        public int? Visibility { get; set; }

        [FieldQuoted(QuoteMode.OptionalForBoth)]
        public string WeatherObsManualCode1 { get; set; }

        [FieldQuoted(QuoteMode.OptionalForBoth)]
        public string WeatherObsManualCode2 { get; set; }

        [FieldQuoted(QuoteMode.OptionalForBoth)]
        public string WeatherObsManualCode3 { get; set; }

        [FieldQuoted(QuoteMode.OptionalForBoth)]
        public string WeatherObsManualCode4 { get; set; }

        [FieldQuoted(QuoteMode.OptionalForBoth)]
        public string WeatherObsAutoCode1 { get; set; }

        [FieldQuoted(QuoteMode.OptionalForBoth)]
        public string WeatherObsAutoCode2 { get; set; }

        [FieldQuoted(QuoteMode.OptionalForBoth)]
        public string WeatherObsAutoCode3 { get; set; }

        [FieldQuoted(QuoteMode.OptionalForBoth)]
        public string WeatherObsAutoCode4 { get; set; }

        [FieldQuoted(QuoteMode.OptionalForBoth)]
        public string W { get; set; }

        [FieldQuoted(QuoteMode.OptionalForBoth)]
        [FieldConverter(ConverterKind.Int32)]
        public int? Temperature { get; set; }

        [FieldQuoted(QuoteMode.OptionalForBoth)]
        [FieldConverter(ConverterKind.Int32)]
        public int? DewPointTemperature { get; set; }

        [FieldQuoted(QuoteMode.OptionalForBoth)]
        [FieldConverter(ConverterKind.Single)]
        public float? SeaLevelPressure { get; set; }

        [FieldQuoted(QuoteMode.OptionalForBoth)]
        [FieldConverter(ConverterKind.Single)]
        public float? AltimeterPressure { get; set; }

        [FieldQuoted(QuoteMode.OptionalForBoth)]
        [FieldConverter(ConverterKind.Single)]
        public float? StationPressure { get; set; }

        [FieldQuoted(QuoteMode.OptionalForBoth)]
        [FieldConverter(ConverterKind.Int32)]
        public int? Max { get; set; }

        [FieldQuoted(QuoteMode.OptionalForBoth)]
        [FieldConverter(ConverterKind.Int32)]
        public int? Min { get; set; }

        [FieldQuoted(QuoteMode.OptionalForBoth)]
        [FieldConverter(ConverterKind.Single)]
        public float? PrecipitationLastHour { get; set; }

        [FieldQuoted(QuoteMode.OptionalForBoth)]
        [FieldConverter(ConverterKind.Single)]
        public float? PrecipitationLast6Hours { get; set; }

        [FieldQuoted(QuoteMode.OptionalForBoth)]
        [FieldConverter(ConverterKind.Single)]
        public float? PrecipitationLast12Hours { get; set; }

        [FieldQuoted(QuoteMode.OptionalForBoth)]
        [FieldConverter(ConverterKind.Single)]
        public float? PrecipitationLast24Hours { get; set; }

        [FieldQuoted(QuoteMode.OptionalForBoth)]
        [FieldConverter(ConverterKind.Single)]
        public float? SnowDepth { get; set; }

    }
}
