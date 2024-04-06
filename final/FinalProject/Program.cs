using System;

class Program {
    static void Main(string[] args) {        
        SnippetManager snippetsManager = new();
        SettingsManager settingsManager = new();
        
        MenuUtility.RunMenu(
            "Main Menu Options:",
            new List<string> {
                "Manage Snippets",
                "Settings",
                "Quit"
            },
            new List<Action> {
                snippetsManager.RunMenu,
                settingsManager.RunMenu
            }
        );
        Console.WriteLine("Good Luck Coding!");
    }
}