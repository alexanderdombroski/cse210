using System.Text.Json;
using System.Text.Json.Nodes;

public static class JsonIO {
    // Methods
    public static JsonObject DeserializeJsonObject(string filename) {
        try {
            string fileContent = File.ReadAllText(filename);
            JsonObject returnObject = JsonSerializer.Deserialize<JsonObject>(fileContent);
            return returnObject;
        } catch (JsonException) {
            Console.WriteLine("The file contains invalid json. Make sure to remove comments if there are any.");
        } catch (Exception ex) {
            Console.WriteLine($"An error occured in reading the json: {ex}");
        }
        ConsoleUtility.WaitForUser();
        return null;
    }
    public static void SerializeJsonObject(string filename, JsonObject jsonObject) {
        JsonSerializerOptions indentOption = new() {WriteIndented = true};
        string jsonString = JsonSerializer.Serialize(jsonObject, indentOption);
        File.WriteAllText(filename, jsonString);
    }
}