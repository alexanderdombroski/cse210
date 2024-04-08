using System;
using System.Linq;
using System.Text.Json.Nodes;
using System.IO;
using System.ComponentModel;

public class SnippetManager : MenuUtility.IMenu {
    // Attributes:
    private List<Snippet> _snippets = new();
    private Dictionary<string, ConsoleColor> _colorKey;
    private string _language;
    private string _languageExtension;
    private string _snippetPath;

    // Constructors:
    public SnippetManager() {
        // Initialze all data needed for managing snippets
        JsonObject settings = JsonIO.DeserializeJsonObject("settings/settings.json");
        JsonObject languages = JsonIO.DeserializeJsonObject("settings/languages.json");
        _language = settings["selected_language"].ToString();
        _snippetPath = settings["snippets_path"].ToString() + languages[_language][0];
        if (!File.Exists(_snippetPath)) { 
            // Creates an empty snippets file if if there is none
            JsonIO.CreateEmptyJsonObject(_snippetPath);
        }
        _languageExtension = languages[_language][1].ToString();
        ColorMapper colmap = new(_language);
        _colorKey = colmap.GetColorData();
        LoadSnippets();
    }

    // Methods:
    private void HandleNoSnippets(Action action) {
        // Pass in methods to make them only run if snippets exist
        if (_snippets.Count == 0) {
            Console.Write("There are no snippets ");
            ConsoleUtility.PauseMiliseconds(1000);
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
        Console.Write("What is the title: ");
        string title = Console.ReadLine();
        Console.Write("What is the keyword: ");
        string keyword = Console.ReadLine();
        Console.Write("What is the description: ");
        string description = Console.ReadLine();

        // Prompts the user to input snippet code in a temporary file.
        string[] body = ReadCodeFile();
        if (body != null) {
            Snippet snippet = new(title, keyword, description, body.ToList());
            _snippets.Add(snippet);
            Console.Write($"{title} Snippet added. ");
        } else {
            Console.Write("Snippet not created due to file reading error. ");
        }
        ConsoleUtility.PauseMiliseconds(1000);
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
        string[] body = CodeReader.ReadInCode(filePath);
        File.Delete(filePath);
        return body;
    }
    private void EditSnippet() {
        int snippet_index = ChooseSnippet();
        Snippet newSnippet = new(_snippets[snippet_index]);
        string[] newBody = ReadCodeFile(newSnippet.GetBody());
        if (newBody != null) {
            Console.Write($"Snippet updated. ");
            newSnippet.UpdateBody(newBody.ToList());
            _snippets[snippet_index] = newSnippet;
        } else {
            Console.Write("Snippet not updated due to file reading error. ");
        }
        ConsoleUtility.PauseMiliseconds(1000);
    }
    private void DeleteSnippet() {
        // Prompts the user to delete a snippet
        int removeOption = ChooseSnippet();
        _snippets.RemoveAt(removeOption);
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
                "Save Snippets",
                "Return to Main Menu"
            },
            new List<Action> {
                () => HandleNoSnippets(DisplaySnippetsList),
                () => HandleNoSnippets(DisplaySnippetCode),
                CreateSnippet,
                () => HandleNoSnippets(EditSnippet),
                () => HandleNoSnippets(DeleteSnippet),
                () => HandleNoSnippets(SaveSnippets)
            }
        );
    }
}