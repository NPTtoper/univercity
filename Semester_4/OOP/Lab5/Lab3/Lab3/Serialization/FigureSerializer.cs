using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Lab3.Factories;
using Lab3.Figures;

namespace Lab3.Serialization
{
    public static class FigureSerializer
    {
        private static JsonSerializerOptions _options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        static FigureSerializer()
        {
            _options.Converters.Add(new FigureJsonConverter());
        }

        public static void Serialize(string filePath, List<Figure> figures, IDataProcessor processor = null)
        {
            string json = JsonSerializer.Serialize(figures, _options);

            if (processor != null)
            {
                json = processor.ProcessBeforeSave(json);
                filePath = filePath + ".encrypted";
            }

            File.WriteAllText(filePath, json);
        }

        public static List<Figure> Deserialize(string filePath, IDataProcessor processor = null)
        {
            string json = File.ReadAllText(filePath);

            if (processor != null)
            {
                json = processor.ProcessAfterLoad(json);
            }

            return JsonSerializer.Deserialize<List<Figure>>(json, _options);
        }
    }

    public class FigureJsonConverter : JsonConverter<Figure>
    {
        public override Figure Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using JsonDocument doc = JsonDocument.ParseValue(ref reader);
            JsonElement root = doc.RootElement;

            string figureType = root.GetProperty("FigureType").GetString();
            IFigureFactory factory = FigureFactoryBase.GetByFigureType(figureType);
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
}