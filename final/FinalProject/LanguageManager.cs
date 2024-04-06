using System.Text.Json;
using System.Text.Json.Nodes;
using System.Linq;
public class LanguageManager : SettingsManager, MenuUtility.IMenu {
    // Attributes:
    private Dictionary<string, List<string>> _filenames;

    // Constructors:
    public LanguageManager() {
        JsonObject filenames = JsonIO.DeserializeJsonObject("settings/languages.json");
        _filenames = filenames.ToDictionary(kv => kv.Key, kv => kv.Value.AsArray().Select(array => array.ToString()).ToList());
    }
    // Methods:
    public void DisplayLangugages() {
        Console.Clear();
        _filenames.ToList().ForEach(kv => Console.WriteLine($" > {kv.Key} - {kv.Value[0]} - .{kv.Value[1]}"));
        int pauseTime = int.Min(int.Max(_filenames.Count, 2), 10) * 1000;
        ConsoleUtility.PauseMiliseconds(pauseTime);
    }
    private void SelectLanguage() {
        var KeyValueList = _filenames.ToList();
        int SelectionIndex = MenuUtility.DisplayMenu("Select a Language", KeyValueList.Select(kv => kv.Key).ToList());
        _settings["selected_language"] = KeyValueList[SelectionIndex - 1].Key;
        UpdateSettings();
    }
    public void AddLanguage() {
        Console.Write("What is the name of the language: ");
        string language = Console.ReadLine();
        Console.Write("Type the name of the file to store snippets (ie css.json): ");
        string filename = Console.ReadLine();
        Console.Write("What is the extension of this language: ");
        string extension = Console.ReadLine();
        _filenames.Add(language, new List<string>{filename, extension});
    }
    public void RemoveLanguage() {
        var KeyValueList = _filenames.ToList();
        int RemoveIndex = MenuUtility.DisplayMenu("Remove a Language from manager (this will not delete the file)", KeyValueList.Select(kv => kv.Key).ToList());
        KeyValueList.RemoveAt(RemoveIndex - 1);
        _filenames = KeyValueList.ToDictionary(kv => kv.Key, kv => kv.Value);
    }
    public void SaveLanguages() {
        JsonObject saveData = new();
        _filenames.ToList().ForEach(kv => saveData.Add(kv.Key, JsonValue.Create(kv.Value)));        
        JsonIO.SerializeJsonObject("settings/languages.json", saveData);
    } 
    public new void RunMenu() {
        MenuUtility.RunMenu(
            "Language Settings:",
            new List<string> {
                "Display Languages",
                "Select Language",
                "Add Language",
                "Remove Language",
                "Update Language File",
                "Return to Settings Menu"
            },
            new List<Action> {
                DisplayLangugages,
                SelectLanguage,
                AddLanguage,
                RemoveLanguage,
                SaveLanguages
            }
        );
    }
}