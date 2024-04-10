// Created by Alex Dombroski on 4/8/2024

using System;

class Program {
    static void RunClass<T>() where T : MenuUtility.IMenu {
        T classInstance = (T)Activator.CreateInstance(typeof(T));
        classInstance.RunMenu();
    }
    static void Main(string[] args) {    
        // Builds the main menu    
        MenuUtility.RunMenu(
            "Main Menu Options:",
            new List<string> {
                "Manage Snippets",
                "Manage Languages",
                "Manage Snippet Storage Locations",
                "Quit"
            },
            new List<Action> {
                RunClass<SnippetManager>,
                RunClass<Language>,
                RunClass<DirectoryMap>
            }
        );
        Console.WriteLine("Good Luck Coding!");
    }
}