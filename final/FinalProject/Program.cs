using System;

class Program {
    static void InitSnippets() {
        SnippetManager snippetsManager = new();
        snippetsManager.RunMenu();
    }
    static void Main(string[] args) {        
        SettingsManager settingsManager = new();
        MenuUtility.RunMenu(
            "Main Menu Options:",
            new List<string> {
                "Manage Snippets",
                "Settings",
                "Quit"
            },
            new List<Action> {
                InitSnippets,
                settingsManager.RunMenu
            }
        );
        Console.WriteLine("Good Luck Coding!");
    }
}