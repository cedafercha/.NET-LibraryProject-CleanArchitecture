using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

public class JsonDateTimeConverter : JsonConverter<DateTime>
{
    private readonly string _dateFormat;

    public JsonDateTimeConverter(string dateFormat)
    {
        _dateFormat = dateFormat;
    }

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string dateString = reader.GetString();
        if (DateTime.TryParseExact(dateString, _dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
        {
            return date;
        }
        else
        {
            throw new JsonException($"Unable to parse '{dateString}' as DateTime in format {_dateFormat}.");
        }
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(_dateFormat));
    }
}
