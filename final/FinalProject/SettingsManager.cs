using System.Text.Json.Nodes;
public class SettingsManager : MenuUtility.IMenu {
    // Attributes:
    protected Dictionary<string, string> _settings;

    // Constructors:
    public SettingsManager() {
        // Initailizes the _settings dictionary
        JsonObject settings = JsonIO.DeserializeJsonObject("settings/settings.json");
        _settings = settings.ToDictionary(kv => kv.Key, kv => kv.Value.ToString());
    }

    // Methods:
    private void ConfigureSnippetDestination() {
        // Allows the user to change the snippet destination, and saves their choice.
        int menuChoice = MenuUtility.DisplayMenu(
            "Pick a folder to store snippet json files in: ",
            new List<string> {
                "Choose My Own",
                "Use Default",
                $"Keep my current choice: {_settings["snippets_path"]}"
            }
        );
        switch (menuChoice) {
            case 1:
                _settings["snippets_path"] = ConsoleUtility.GetAbsolutePath("Type the full file path (include final '/'): ");
                break;
            case 2:
                _settings["snippets_path"] = "snippets/";
                break;
        }
        Console.Write("Settings Updated ");
        ConsoleUtility.PauseMiliseconds(1000);
        UpdateSettings();
    }
    private void ManageLanguages() {
        // Open the language settings menu
        LanguageManager languageManager = new();
        languageManager.RunMenu();
    }
    protected void UpdateSettings() {
        // Updates the settings.json file with the current settings
        JsonObject saveData = new();
        _settings.ToList().ForEach(kv => saveData.Add(kv.Key, kv.Value));        
        JsonIO.SerializeJsonObject("settings/settings.json", saveData);
    }
    public void RunMenu() {
        // Builds a settings menu
        MenuUtility.RunMenu(
            "Settings Menu:",
            new List<string> {
                "Configure Snippet Destination",
                "Manage Languages",
                "Return to Main Menu"
            },
            new List<Action> {
                ConfigureSnippetDestination,
                ManageLanguages
            }
        );
    }
}