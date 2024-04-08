// Created by Alex Dombroski on 4/8/2024

using System;

class Program {
    static void InitSnippets() {
        SnippetManager snippetsManager = new();
        snippetsManager.RunMenu();
    }
    static void InitSettings() {
        SettingsManager settingsManager = new();
        settingsManager.RunMenu();
    }
    static void Main(string[] args) {    
        // Builds the main menu    
        MenuUtility.RunMenu(
            "Main Menu Options:",
            new List<string> {
                "Manage Snippets",
                "Settings",
                "Quit"
            },
            new List<Action> {
                InitSnippets,
                InitSettings
            }
        );
        Console.WriteLine("Good Luck Coding!");
    }
}