using System;
using System.Linq;
using System.Text.Json.Nodes;
using System.IO;

public class SnippetManager : MenuUtility.IMenu {
    // Attributes:
    private List<Snippet> _snippets = new();
    private Dictionary<string, ConsoleColor> _colorKey;
    private string _language;
    private string _languageExtension;
    private string _snippetPath;
    private string _scope;

    // Constructors:
    public SnippetManager() {
        // Initialze all data needed for managing snippets
        JsonObject settings = JsonIO.DeserializeJsonObject("settings/settings.json");
        JsonObject languages = JsonIO.DeserializeJsonObject("settings/languages.json");
        _language = settings["selected_language"].ToString();
        string file = languages[_language][0].ToString();
        _snippetPath = settings["snippets_path"].ToString() + file;
        
        // Color and scope
        string scope = file[..file.IndexOf('.')];
        ColorMapper colmap = new(scope);
        _colorKey = colmap.GetColorData();
        if (file.EndsWith(".code-snippets")) {
            _scope = scope;
        } else {
            _scope = null;
        }
        
        // Load Snippets
        if (!File.Exists(_snippetPath)) { 
            // Creates an empty snippets file if if there is none
            JsonIO.CreateEmptyJsonObject(_snippetPath);
        }
        _languageExtension = languages[_language][1].ToString();
        LoadSnippets();
    }

    // Methods:
    private void HandleNoSnippets(Action action) {
        // Pass in methods to make them only run if snippets exist
        if (_snippets.Count == 0) {
            ConsoleUtility.PauseWrite("There are no snippets ", 1000);
        } else {
            action();
        }
    }
    private void DisplaySnippetsList() {
        // Display a list of snippet details
        Console.Clear();
        Console.WriteLine($"{_language} Snippets");
        _snippets.ForEach(snippet => Console.WriteLine(snippet.ToLongString()));
        ConsoleUtility.WaitForUser();
    }
    private void DisplaySnippetCode() {
        // Display code from the snippet of choice
        _snippets[ChooseSnippet()].DisplaySnippet(_colorKey);
        ConsoleUtility.WaitForUser();
    }
    private int ChooseSnippet() {
        // Builds a menu and allows user to pick a snippet
        return MenuUtility.DisplayMenu("Choose a snippet", _snippets.Select(snippet => snippet.ToShortString()).ToList()) - 1;
    }
    private void CreateSnippet() {
        // Gets snippet details from the terminal
        string title = ConsoleUtility.WriteRead("What is the title: ");
        string keyword = ConsoleUtility.WriteRead("What is the keyword: ");
        string description = ConsoleUtility.WriteRead("What is the description: ");

        // Prompts the user to input snippet code in a temporary file.
        string[] body = ReadCodeFile();
        Snippet snippet = new(title, keyword, description, body.ToList(), _scope);
        _snippets.Add(snippet);
        ConsoleUtility.PauseWrite($"{title} Snippet added. ", 1000);
        SaveSnippets();
    }
    private string[] ReadCodeFile(List<string> oldBody = null) {
        // Writes code to a file, waits for the user to edit it, then returns it
        string filePath = $"CustomCode/CustomSnippet.{_languageExtension}";
        if (oldBody == null) {
            using (File.Create(filePath)) {};  
        } else {
            File.WriteAllLines(filePath, oldBody);
        }
        Console.WriteLine($"Insert the code into the file CustomSnippet.{_languageExtension} in the\nCustomCode folder located in the same directory as this file");
        Console.WriteLine("SAVE AND CLOSE THE FIlE, then come back here.");
        ConsoleUtility.WaitForUser();
        string[] body = File.ReadAllLines(filePath);
        File.Delete(filePath);
        return body;
    }
    private void EditSnippet() {
        // Basically creates a snippet, but with preset code and snippet details.
        int snippet_index = ChooseSnippet();
        Snippet newSnippet = new(_snippets[snippet_index]);
        string[] newBody = ReadCodeFile(newSnippet.GetBody());
        newSnippet.UpdateBody(newBody.ToList());
        _snippets[snippet_index] = newSnippet;
        ConsoleUtility.PauseWrite("Snippet updated. ", 1000);
        SaveSnippets();
    }
    private void DeleteSnippet() {
        // Prompts the user to delete a snippet
        int removeOption = ChooseSnippet();
        _snippets.RemoveAt(removeOption);
        SaveSnippets();
    }
    private void LoadSnippets() {
        // Load in snippets from a json file
        JsonObject snippets = JsonIO.DeserializeJsonObject(_snippetPath);
        if (snippets != null) {
            foreach (var kv in snippets.ToList()) {
                Snippet snippet =  new(
                    kv.Key.ToString(), 
                    kv.Value.AsObject()
                );
                _snippets.Add(snippet);
            }
        }     
    }
    private void SaveSnippets() {
        // Write snippets to a json file
        JsonObject saveData = new();
        _snippets.ForEach(snippet => saveData.Add(snippet.ToJson()));
        JsonIO.SerializeJsonObject(_snippetPath, saveData);
        Console.Write("Data Saved ");
        ConsoleUtility.PauseMiliseconds(1000);
    }
    public void RunMenu() {
        // Builds the menu for the snippet manager
        MenuUtility.RunMenu(
            $"{_language} Snippet Menu Options:",
            new List<string> {
                "View all Snippets",
                "View Snippet Code",
                "Create Snippet",
                "Edit Snippet",
                "Delete Snippet",
                "Return to Main Menu"
            },
            new List<Action> {
                () => HandleNoSnippets(DisplaySnippetsList),
                () => HandleNoSnippets(DisplaySnippetCode),
                CreateSnippet,
                () => HandleNoSnippets(EditSnippet),
                () => HandleNoSnippets(DeleteSnippet)
            }
        );
    }
}