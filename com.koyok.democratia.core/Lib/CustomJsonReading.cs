using System.Text.Json;
using System.Text.Json.Serialization;
using System.Globalization;

namespace com.koyok.democratia.Lib
{

    public class CustomDateTimeConverter : JsonConverter<DateTime>
    {
        
        private readonly string _format = "yyyy-MM-dd HH:mm:ss";

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string dateString = reader.GetString()!;

            if (DateTime.TryParseExact(dateString, _format, CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime date))
            {
                return date;
            }
            return DateTime.Parse(dateString);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(_format, CultureInfo.CurrentCulture));
        }

        public override DateTime ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return base.ReadAsPropertyName(ref reader, typeToConvert, options);
        }
    }
}
