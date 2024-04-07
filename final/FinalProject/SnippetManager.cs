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

    // Constructors:
    public SnippetManager() {
        JsonObject settings = JsonIO.DeserializeJsonObject("settings/settings.json");
        JsonObject languages = JsonIO.DeserializeJsonObject("settings/languages.json");
        _language = settings["selected_language"].ToString();
        _snippetPath = settings["snippets_path"].ToString() + languages[_language][0];
        if (!File.Exists(_snippetPath)) {
            JsonIO.CreateEmptyJsonObject(_snippetPath);
        }
        _languageExtension = languages[_language][1].ToString();
        ColorMapper colmap = new(_language);
        _colorKey = colmap.GetColorData();
        LoadSnippets();
    }

    // Methods:
    private void HandleNoSnippets(Action action) {
        if (_snippets.Count == 0) {
            Console.Write("There are no snippets ");
            ConsoleUtility.PauseMiliseconds(1000);
        } else {
            action();
        }
    }
    private void DisplaySnippetsList() {
        Console.Clear();
        Console.WriteLine($"{_language} Snippets");
        _snippets.ForEach(snippet => Console.WriteLine(snippet.ToLongString()));
        ConsoleUtility.WaitForUser();
    }
    private void DisplaySnippetCode() {
        _snippets[ChooseSnippet()].DisplaySnippet(_colorKey);
        ConsoleUtility.WaitForUser();
    }
    private int ChooseSnippet() {
        return MenuUtility.DisplayMenu("Choose a snippet", _snippets.Select(snippet => snippet.ToShortString()).ToList()) - 1;
    }
    private void CreateSnippet() {
        string filePath = $"CustomCode/CustomSnippet.{_languageExtension}";
        Console.Write("What is the title: ");
        string title = Console.ReadLine();
        Console.Write("What is the keyword: ");
        string keyword = Console.ReadLine();
        Console.Write("What is the description: ");
        string description = Console.ReadLine();
        using (File.Create(filePath)) {};
        Console.WriteLine($"Insert the code into the file CustomSnippet.{_languageExtension} in the\nCustomCode folder located in the same directory as this file");
        Console.WriteLine("SAVE AND CLOSE THE FIlE, then come back here.");
        ConsoleUtility.WaitForUser();
        string[] body = CodeReader.ReadInCode(filePath);
        File.Delete(filePath);
        if (body != null) {
            Snippet snippet = new(title, keyword, description, body.ToList());
            _snippets.Add(snippet);
            Console.Write($"{title} Snippet added. ");
        } else {
            Console.Write("Snippet not created due to file reading error. ");
        }
        ConsoleUtility.PauseMiliseconds(1000);
    }
    private void DeleteSnippet() {
        int removeOption = ChooseSnippet();
        _snippets.RemoveAt(removeOption);
    }
    private void LoadSnippets() {
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
        JsonObject saveData = new();
        _snippets.ForEach(snippet => saveData.Add(snippet.ToJson()));
        JsonIO.SerializeJsonObject(_snippetPath, saveData);
        Console.Write("Data Saved ");
        ConsoleUtility.PauseMiliseconds(1000);
    }
    public void RunMenu() {
        MenuUtility.RunMenu(
            $"{_language} Snippet Menu Options:",
            new List<string> {
                "View all Snippets",
                "View Snippet Code",
                "Create Snippet",
                "Delete Snippet",
                "Save Snippets",
                "Return to Main Menu"
            },
            new List<Action> {
                () => HandleNoSnippets(DisplaySnippetsList),
                () => HandleNoSnippets(DisplaySnippetCode),
                CreateSnippet,
                () => HandleNoSnippets(DeleteSnippet),
                () => HandleNoSnippets(SaveSnippets)
            }
        );
    }
}