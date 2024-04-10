using System.Text.Json.Nodes;
public abstract class Settings : MenuUtility.IMenu {
    // Attributes:
    protected readonly Dictionary<string, string> _settings;
    // Constructors:
    public Settings() {
        _settings = JsonIO.ReadJsonStringObject("settings/settings.json");
    }

    // Methods:
    public abstract void RunMenu();
    protected abstract void SelectOption();
    protected abstract void AddOptions();
    protected abstract void SaveOptions();
    protected abstract void DeleteOption();
    protected void UpdateSettings() {      
        JsonIO.SerializeJsonObject("settings/settings.json", JsonIO.DictToObject(_settings));
        ConsoleUtility.PauseWrite("Settings Updated ", 1000);
    }
}