// Alex Dombroski - 03/08/2024
// CREATIVITY is in comments (use CTRL-F)
/* A Name is associated with each Journal File. 
The User doesn't have to enter their name more than 
once, if they load their journal. */
/* Every input is checked, so the user can't type in an 
invalid menu option or filename to be saved/loaded */

using System;
using System.IO.Enumeration;
using Microsoft.VisualBasic;
using System.Linq;

class Program {
    static void ADDisplayMenu() {
        // Displays the menu
        Console.WriteLine("\nWhat would you like to do?");
        Console.WriteLine("    1. Write an entry");
        Console.WriteLine("    2. Display your Journal");
        Console.WriteLine("    3. Save your Journal");
        Console.WriteLine("    4. Load a previous Jornal");
        Console.WriteLine("    5. Quit");
    }

    static int ADGetValidInt(string p_Prompt = "Type in a number: ") {
        // Gets a valid integer. If invalid int is typed, asks again, using the same prompt.
        bool ADInvalidResponse;
        int ADIntResponse = 0;
        do {
            ADInvalidResponse = false;
            Console.Write(p_Prompt);
            string ADResponse = Console.ReadLine();
            try {
                ADIntResponse = int.Parse(ADResponse);
            } catch {
                ADInvalidResponse = true;
            }
        } while (ADInvalidResponse);
        return ADIntResponse;
    }

    static int ADGetMenuSelection() {
        // CREATIVITY: Repeatedly asks user for a menu option choice until a valid answer is given
        int ADOption;
        do {
            ADOption = ADGetValidInt("Type the number of the option of choice: ");
        } while (ADOption < 1 || ADOption > 5);
        return ADOption;
    }
    
    static void ADWriteEntry(ADJournal p_Journal, ADPromptGenerator p_PromptGen) {
        // Creates and stores the data of a new entry using the ADEntry class
        ADEntry ADNewEntry = new() {
            _ADPrompt = p_PromptGen.ADPickPrompt(),
        };
        Console.WriteLine(ADNewEntry._ADPrompt);
        Console.Write("> ");
        ADNewEntry._ADResponse = Console.ReadLine();
        p_Journal._ADEntries.Add(ADNewEntry);
    }

    static void ADDisplayJournal(ADJournal p_Journal) {
        // Gets name, if not already stored
        p_Journal._ADName ??= ADGetUserName();

        // Writes the Journal
        List<string> ADEntries = p_Journal.ADToString();
        ADEntries.ForEach(Console.WriteLine);
    }
    
    static string ADCheckFilename(string p_Filename, string p_Extension = ".txt") {
        // CREATIVITY - Returns a given string as a filename, if not already
        if (p_Filename == "") {
            Console.Write("Type the name of the text file: ");
            p_Filename = Console.ReadLine();
            p_Filename = p_Filename.Contains('.') ? p_Filename : p_Filename + p_Extension;
        }
        return p_Filename;
    }

    static void ADSaveJournal(ADJournal p_Journal, string p_Filename = "") {
        // User picks a file, if one isn't provided
        p_Filename = ADCheckFilename(p_Filename);

        // CREATIVITY - User enters there name, if not already stored
        p_Journal._ADName ??= ADGetUserName();

        // Writes the data to a file
        using (StreamWriter ADOutputFile = new(p_Filename)) {
            List<string> ADLines = p_Journal.ADToCsv();
            ADLines.ForEach(ADOutputFile.Write);
        }
    }

    static void ADLoadJournal(ADJournal p_Journal, string p_Filename = "") {
        // User picks a file, if one isn't provided
        p_Filename = ADCheckFilename(p_Filename);

        // Loads the file data if it exists, and displays success or failure
        if (File.Exists(p_Filename)) {
            p_Journal._ADEntries = new();
            string[] ADLines = File.ReadAllLines(p_Filename);
            
            // Handle first line
            p_Journal._ADName = ADLines.FirstOrDefault().Trim();
            IEnumerable<string> ADData = ADLines.Skip(1);

            foreach (string line in ADData) {
                string[] parts = line.Trim().Split('|');
                ADEntry ADLoadedEntry = new() {
                    _ADDate = parts[0],
                    _ADPrompt = parts[1],
                    _ADResponse = parts[2]
                };
                p_Journal._ADEntries.Add(ADLoadedEntry);
            };
            Console.WriteLine($"{p_Filename} successfully loaded");
        } else {
            Console.WriteLine("File doesn't exist");
        }
    }

    static string ADGetUserName() {
        // CREATIVITY  - Gets the users name
        Console.Write("What is your name? ");
        return Console.ReadLine();
    }

    static void Main(string[] args) {
        Console.WriteLine("Welcome to the Journal Program!");
        ADJournal ADUserJournal = new();
        ADPromptGenerator ADPromptGen = new();
        int ADMenuOption;
        
        // Main() Menu Loop
        do {
            ADDisplayMenu();
            ADMenuOption = ADGetMenuSelection();
            switch (ADMenuOption) {
                case 1:
                    ADWriteEntry(ADUserJournal, ADPromptGen);
                    break;
                case 2:
                    ADDisplayJournal(ADUserJournal);
                    break;
                case 3:
                    ADSaveJournal(ADUserJournal);
                    break;
                case 4:
                    ADLoadJournal(ADUserJournal);
                    break;
                case 5:
                    if (ADUserJournal._ADName == null) {
                        Console.WriteLine("Thanks for using the Journal Program");
                    } else {
                        Console.WriteLine("Thanks for using the Journal Program, " + ADUserJournal._ADName);
                    }
                    break;
                default:
                    Console.WriteLine("You've selected an invalid option and somehow broken the program");
                    break;
            }
        } while (ADMenuOption != 5);
    }
}