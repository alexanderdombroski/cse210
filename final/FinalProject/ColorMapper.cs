using System.Text.Json.Nodes;
public class ColorMapper {
    // Attributes
    private readonly Dictionary<string, ConsoleColor> _colors = new();

    // Constructors:
    public ColorMapper(string language) {
        JsonObject colorData = JsonIO.DeserializeJsonObject("settings/colors.json");
        var iteratorData = colorData[language];
        Console.WriteLine(language);
        foreach (var kvp in iteratorData.AsObject()) {
            ConsoleColor color = StringToColor(kvp.Key);
            kvp.Value.AsArray().ToList().ForEach(word => _colors.Add(word.ToString(), color));
        }
    }

    // Methods:
    private static ConsoleColor StringToColor(string color) {
        return (ConsoleColor)Enum.Parse(typeof(ConsoleColor), color);
    }
    public Dictionary<string, ConsoleColor> GetColorData() {
        return _colors;
    }
}