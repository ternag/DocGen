using System.Text.Json;
using System.Text.Json.Serialization;

namespace DocHose;

[JsonConverter(typeof(PercentJsonConverter))]
public record Percent 
{
    private readonly int _value;

    public Percent(int value)
    {
        if (value < 0) throw new OverflowException("Value must not be < 0");
        if (value > 100) throw new OverflowException("Value must not be > 100");
        _value = value;
    }

    public static implicit operator Percent(int value) => new(value);
    public static implicit operator int(Percent value) => value._value;
}
public class PercentJsonConverter : JsonConverter<Percent>
{
    public override Percent Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => Convert.ToInt32(reader.GetInt32());
    public override void Write(Utf8JsonWriter writer, Percent value, JsonSerializerOptions options) => writer.WriteNumberValue(value);
}

[JsonConverter(typeof(DocumentCountJsonConverter))]
public record DocumentCount
{
    private readonly int _value;

    public DocumentCount(int value)
    {
        if (value < 0) throw new OverflowException("Value must not be < 0");
        _value = value;
    }

    public static implicit operator DocumentCount(int value) => new(value);
    public static implicit operator int(DocumentCount value) => value._value;

    public override string ToString()
    {
        return $"{_value}";
    }
}
public class DocumentCountJsonConverter : JsonConverter<DocumentCount>
{
    public override DocumentCount Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => Convert.ToInt32(reader.GetInt32());
    public override void Write(Utf8JsonWriter writer, DocumentCount value, JsonSerializerOptions options) => writer.WriteNumberValue(value);
}


[JsonConverter(typeof(NumberOfSectionsJsonConverter))]
public record NumberOfSections
{
    private readonly int _value;

    public NumberOfSections(int value)
    {
        if (value < 1) throw new OverflowException("Value must be >= 1");
        _value = value;
    }

    public static implicit operator NumberOfSections(int value) => new(value);
    public static implicit operator int(NumberOfSections value) => value._value;
}
public class NumberOfSectionsJsonConverter : JsonConverter<NumberOfSections>
{
    public override NumberOfSections Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => Convert.ToInt32(reader.GetInt32());
    public override void Write(Utf8JsonWriter writer, NumberOfSections value, JsonSerializerOptions options) => writer.WriteNumberValue(value);
}

//class Int32Converter : JsonConverter<int>
//{
//    public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
//    {
//        if (reader.TokenType == JsonTokenType.Number)
//        {
//            return reader.GetInt32();
//        }

//        throw new JsonException();
//    }

//    public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
//    {
//        writer.WriteNumberValue(value);
//    }
//}

//public class IntegerJsonConverter<T> : JsonConverter<T>
//{
//    public override T Read(
//        ref Utf8JsonReader reader,
//        Type typeToConvert,
//        JsonSerializerOptions options) => Convert.ToInt32(reader.GetInt32());

//    public override void Write(
//        Utf8JsonWriter writer,
//        T value,
//        JsonSerializerOptions options) =>
//        writer.WriteNumberValue(value);
//}

//public class Int32ValueConverter : JsonConverterFactory
//{
//    public override bool CanConvert(Type typeToConvert)
//    {
//        return true;
//    }

//    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
//    {

//    }

//    private class IntegerJsonConverterInner<T> : JsonConverter<int>
//    {
//        public override int Read(
//            ref Utf8JsonReader reader,
//            Type typeToConvert,
//            JsonSerializerOptions options) => Convert.ToInt32(reader.GetInt32());

//        public override void Write(
//            Utf8JsonWriter writer,
//            int value,
//            JsonSerializerOptions options) =>
//            writer.WriteNumberValue(value);
//    }
//}