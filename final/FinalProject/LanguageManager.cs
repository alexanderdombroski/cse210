using System.Text.Json;
using System.Text.Json.Nodes;
using System.Linq;
public class LanguageManager : SettingsManager, MenuUtility.IMenu {
    // Attributes:
    private Dictionary<string, List<string>> _languageData;

    // Constructors:
    public LanguageManager() {
        // Initializes the _languageData dictionary
        JsonObject filenames = JsonIO.DeserializeJsonObject("settings/languages.json");
        _languageData = filenames.ToDictionary(kv => kv.Key, kv => kv.Value.AsArray().Select(array => array.ToString()).ToList());
    }
    // Methods:
    private void SelectLanguage() {
        // Prompts the user to select a language, and updates it in the file
        var KeyValueList = _languageData.ToList();
        int SelectionIndex = MenuUtility.DisplayMenu("Select a Language", KeyValueList.Select(kv => kv.Key).ToList());
        _settings["selected_language"] = KeyValueList[SelectionIndex - 1].Key;
        UpdateSettings();
    }
    private void AddLanguage() {
        // Allows the user to add data need for a new programming language
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
        // Creates a menu and allows the user to delete language-related data and snippets 
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
        // Updates the language settings
        JsonObject saveData = new();
        _languageData.ToList().ForEach(kv => saveData.Add(kv.Key, JsonValue.Create(kv.Value)));        
        JsonIO.SerializeJsonObject("settings/languages.json", saveData);
        Console.Write("Languages Updated ");
        ConsoleUtility.PauseMiliseconds(1000);
    } 
    public new void RunMenu() {
        // Creates a language settings menu and maps each option to a function
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