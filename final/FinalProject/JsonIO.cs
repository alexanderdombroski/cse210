using System.Text.Json;
using System.Text.Json.Nodes;

public static class JsonIO {
    // Methods
    public static JsonObject DeserializeJsonObject(string filename) {
        string fileContent = File.ReadAllText(filename);
        JsonObject returnObject = JsonSerializer.Deserialize<JsonObject>(fileContent);
        return returnObject;
    }
    public static void SerializeJsonObject(string filename, JsonObject jsonObject) {
        JsonSerializerOptions indentOption = new() {WriteIndented = true};
        string jsonString = JsonSerializer.Serialize(jsonObject, indentOption);
        File.WriteAllText(filename, jsonString);
    }
}