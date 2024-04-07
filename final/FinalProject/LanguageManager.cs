using System.Text.Json;
using System.Text.Json.Nodes;
using System.Linq;
public class LanguageManager : SettingsManager, MenuUtility.IMenu {
    // Attributes:
    private Dictionary<string, List<string>> _languageData;

    // Constructors:
    public LanguageManager() {
        JsonObject filenames = JsonIO.DeserializeJsonObject("settings/languages.json");
        _languageData = filenames.ToDictionary(kv => kv.Key, kv => kv.Value.AsArray().Select(array => array.ToString()).ToList());
    }
    // Methods:
    private void SelectLanguage() {
        var KeyValueList = _languageData.ToList();
        int SelectionIndex = MenuUtility.DisplayMenu("Select a Language", KeyValueList.Select(kv => kv.Key).ToList());
        _settings["selected_language"] = KeyValueList[SelectionIndex - 1].Key;
        UpdateSettings();
    }
    private void AddLanguage() {
        Console.Write("What is the name of the language: ");
        string language = Console.ReadLine();
        Console.Write("Type the name of the file to store snippets (ie css.json): ");
        string filename = Console.ReadLine();
        Console.Write("What is the extension of this language: ");
        string extension = Console.ReadLine();
        _languageData.Add(language, new List<string>{filename, extension});
        SaveLanguages();
    }
    private void RemoveLanguage() {
        var KeyValueList = _languageData.ToList();
        int RemoveIndex = MenuUtility.DisplayMenu("Remove a Language from manager (this will also delete the snippet file)", KeyValueList.Select(kv => kv.Key).ToList());
        string filePath = _settings["snippets_path"] + _languageData[KeyValueList[RemoveIndex - 1].Key][0];
        if (File.Exists(filePath)) {
            File.Delete(filePath);
        }
        KeyValueList.RemoveAt(RemoveIndex - 1);
        _languageData = KeyValueList.ToDictionary(kv => kv.Key, kv => kv.Value);
        SaveLanguages();
    }
    private void SaveLanguages() {
        JsonObject saveData = new();
        _languageData.ToList().ForEach(kv => saveData.Add(kv.Key, JsonValue.Create(kv.Value)));        
        JsonIO.SerializeJsonObject("settings/languages.json", saveData);
        Console.Write("Languages Updated ");
        ConsoleUtility.PauseMiliseconds(1000);
    } 
    public new void RunMenu() {
        MenuUtility.RunMenu(
            "Language Settings:",
            new List<string> {
                "Select Language",
                "Add Language",
                "Remove Language",
                "Return to Settings Menu"
            },
            new List<Action> {
                SelectLanguage,
                AddLanguage,
                RemoveLanguage
            }
        );
    }
}