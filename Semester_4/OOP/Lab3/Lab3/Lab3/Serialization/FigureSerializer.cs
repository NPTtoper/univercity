using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public static class FigureSerializer
{
    private static JsonSerializerOptions options = new JsonSerializerOptions
    {
        WriteIndented = true,
        Converters = { new FigureJsonConverter() }
    };

    public static void Serialize(string filePath, List<Figure> figures)
    {
        string json = JsonSerializer.Serialize(figures, options);
        File.WriteAllText(filePath, json);
    }

    public static List<Figure> Deserialize(string filePath)
    {
        string json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<List<Figure>>(json, options);
    }
}

public class FigureJsonConverter : System.Text.Json.Serialization.JsonConverter<Figure>
{
    public override Figure Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument doc = JsonDocument.ParseValue(ref reader);
        JsonElement root = doc.RootElement;

        string figureType = root.GetProperty("FigureType").GetString();
        FigureFactory factory = FigureFactory.GetByFigureType(figureType);
        Figure figure = factory.CreateFigure();

        figure.StartX = root.GetProperty("StartX").GetDouble();
        figure.StartY = root.GetProperty("StartY").GetDouble();
        figure.EndX = root.GetProperty("EndX").GetDouble();
        figure.EndY = root.GetProperty("EndY").GetDouble();

        return figure;
    }

    public override void Write(Utf8JsonWriter writer, Figure value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString("FigureType", value.FigureType);
        writer.WriteNumber("StartX", value.StartX);
        writer.WriteNumber("StartY", value.StartY);
        writer.WriteNumber("EndX", value.EndX);
        writer.WriteNumber("EndY", value.EndY);
        writer.WriteEndObject();
    }
}