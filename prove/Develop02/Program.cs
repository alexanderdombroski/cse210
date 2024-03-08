using System;
using System.IO.Enumeration;
using Microsoft.VisualBasic;
using System.Linq;

class Program {
    static void ADDisplayMenu() {
        Console.WriteLine("\nWhat would you like to do?");
        Console.WriteLine("    1. Write an entry");
        Console.WriteLine("    2. Display your Journal");
        Console.WriteLine("    3. Save your Journal");
        Console.WriteLine("    4. Load a previous Jornal");
        Console.WriteLine("    5. Quit");
    }

    static int ADGetValidInt(string p_Prompt = "Type in a number: ") {
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
        int ADOption;
        do {
            ADOption = ADGetValidInt("Type the number of the option of choice: ");
        } while (ADOption < 1 || ADOption > 5);
        return ADOption;
    }
    
    static void ADWriteEntry(ADJournal p_Journal, ADPromptGenerator p_PromptGen) {
        ADEntry ADNewEntry = new() {
            _ADPrompt = p_PromptGen.ADPickPrompt(),
        };
        Console.WriteLine(ADNewEntry._ADPrompt);
        Console.Write("> ");
        ADNewEntry._ADResponse = Console.ReadLine();
        p_Journal._ADEntries.Add(ADNewEntry);
    }

    static void ADDisplayJournal(ADJournal p_Journal) {
        p_Journal._ADName ??= ADGetUserName();
        Console.WriteLine($"----- {p_Journal._ADName}'s Journal -----");
        List<string> ADEntries = p_Journal.ADToString();
        ADEntries.ForEach(Console.WriteLine);
    }
    
    static void ADSaveJournal(ADJournal p_Journal, string p_Filename = "") {
        if (p_Filename == "") {
            Console.Write("Type the name of the text file to save? ");
            p_Filename = Console.ReadLine();
        }
        p_Journal._ADName ??= ADGetUserName();
        using (StreamWriter ADOutputFile = new(p_Filename)) {
            ADOutputFile.WriteLine(p_Journal._ADName);
            List<string> ADLines = p_Journal.ADToCsv();
            ADLines.ForEach(ADOutputFile.Write);
        }
    }

    static void ADLoadJournal(ADJournal p_Journal, string p_Filename = "") {
        if (p_Filename == "") {
            Console.Write("Type the name of the text file to load? ");
            p_Filename = Console.ReadLine();
        }
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
        } else {
            Console.WriteLine("File doesn't exist");
        }
    }

    static string ADGetUserName() {
        Console.Write("What is your name? ");
        return Console.ReadLine();
    }

    static void Main(string[] args) {
        Console.WriteLine("Welcome to the Journal Program!");
        ADJournal ADUserJournal = new();
        ADPromptGenerator ADPromptGen = new();
        int ADMenuOption;
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