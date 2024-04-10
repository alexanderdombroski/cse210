public class DirectoryMap : Settings {
    // Attributes:
    Dictionary<string, string> _fileMap;

    // Constructors:
    public DirectoryMap() : base() {
        _fileMap = JsonIO.ReadJsonStringObject("settings/directories.json");
    }

    // Methods:
    public override void RunMenu() {
        // Creates a language settings menu and maps each option to a function
        MenuUtility.RunMenu(
            "Snippet Location Settings",
            new List<string> {
                "Select a Snippet Location",
                "Add a Storage Location",
                "Forget a Known Folder",
                "Return to Settings Menu"
            },
            new List<Action> {
                SelectOption,
                AddOptions,
                DeleteOption
            }
        );
    }
    private (List<KeyValuePair<string, string>>, int) ChooseFolder() {
        // Makes the dictionary into an ordered format and user picks one.
        var keyValueList = _fileMap.ToList();
        int option = MenuUtility.DisplayMenu("Select a Language", keyValueList.Select(kv => kv.Key).ToList()) - 1;
        return new(keyValueList, option);
    }
    protected override void SelectOption() {
        // Allows the user to change the snippet destination, and saves their choice.
        var (keyValueList, option) = ChooseFolder();
        _settings["snippets_path"] = keyValueList[option].Value;
        UpdateSettings();
    }
    protected override void AddOptions() {
        // Allows the user to add data need for a new programming language
        string filepath = ConsoleUtility.GetAbsolutePath("Type the path to store the snippets: ");
        string foldername = ConsoleUtility.WriteRead("Type a name to remember this folder by: ");
        _fileMap.Add(foldername, filepath);
        SaveOptions();
    }
    protected override void DeleteOption() {
        // Deletes a directory for the json file, but doesn't delete the folder itself
        var (keyValueList, option) = ChooseFolder();
        keyValueList.RemoveAt(option);
        _fileMap = keyValueList.ToDictionary(kv => kv.Key, kv => kv.Value);
        SaveOptions();
    }
    protected override void SaveOptions() {
        // Saves the language data to the file
        JsonIO.SerializeJsonObject("settings/directories.json", JsonIO.DictToObject(_fileMap));
        ConsoleUtility.PauseWrite("Known Directories Updated ", 1000);
    }
}