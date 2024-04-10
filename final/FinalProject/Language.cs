public class Language : Settings {
    // Attributes:
    private Dictionary<string, List<string>> _languageData; 

    // Constructors:
    public Language() : base() {
        _languageData = JsonIO.ReadJsonListObject("settings/languages.json");
    }

    // Methods:
    public override void RunMenu() {
        // Creates a language settings menu and maps each option to a function
        MenuUtility.RunMenu(
            "Language Settings",
            new List<string> {
                "Select a Language",
                "Add a Language",
                "Delete a Language",
                "Return to Settings Menu" 
            },
            new List<Action> {
                SelectOption,
                AddOptions,
                DeleteOption
            }
        );
    }
    private (List<KeyValuePair<string,List<string>>>, int) ChooseLanguage() {
        // Makes the dictionary into an ordered format and user picks one.
        var keyValueList = _languageData.ToList();
        int option = MenuUtility.DisplayMenu("Select a Language", keyValueList.Select(kv => kv.Key).ToList()) - 1;
        return new(keyValueList, option);
    }
    protected override void SelectOption() {
        // Prompts the user to select a language, and updates it in the file
        var (keyValueList, index) = ChooseLanguage();
        _settings["selected_language"] = keyValueList[index].Key;
        UpdateSettings();
    }
    protected override void AddOptions() {
        // Allows the user to add data need for a new programming language
        string language = ConsoleUtility.WriteRead("What is the name of the language: ");
        string filename = ConsoleUtility.WriteRead("Type the name of the file to store snippets (ie css.json): ");
        string extension = ConsoleUtility.WriteRead("What is the extension of this language: ");
        _languageData.Add(language, new List<string>{filename, extension});
        SaveOptions();
    }
    protected override void DeleteOption() {
        // Creates a menu and allows the user to delete language-related data and snippets 
        var (keyValueList, index) = ChooseLanguage();
        string filePath = _settings["snippets_path"] + _languageData[keyValueList[index].Key][0];
        if (File.Exists(filePath)) {
            File.Delete(filePath);
        }
        keyValueList.RemoveAt(index);
        _languageData = keyValueList.ToDictionary(kv => kv.Key, kv => kv.Value);
        SaveOptions();
    }
    protected override void SaveOptions() {
        // Saves the language data to the file
        JsonIO.SerializeJsonObject("settings/languages.json", JsonIO.DictToObject(_languageData));
        ConsoleUtility.PauseWrite("Languages Updated ", 1000);
    }
}