using System;

class Program
{
    static void ADDisplayMenu() {
        Console.WriteLine("Welcome to the scripture program!");
        Console.WriteLine("    1. Choose Scripture");
        Console.WriteLine("    2. Pick Random Scripture");
        Console.WriteLine("    3. Add Scripture");
        Console.WriteLine("    4. Quit");
    }
    static int ADGetMenuSelection() {
        // Repeatedly asks user for a menu option choice until a valid answer is given
        int ADOption;
        bool ADInvalidResponse;
        do {
            Console.Write("Type the number of the option of choice: ");
            string ADResponse = Console.ReadLine();
            // Check if valid number
            ADInvalidResponse = !int.TryParse(ADResponse, out ADOption);
        } while (ADOption < 1 || ADOption > 4 || ADInvalidResponse);
        return ADOption;
    }

    static string ADGetString(string P_Prompt) {
        Console.Write(P_Prompt);
        return Console.ReadLine();
    }

    static void ADIncrementallyHideScripture(ADScripture P_Scripture) {
        bool ADNotCompletelyHidden;
        string ADUserInput;
        do {
            Console.Clear();
            Console.WriteLine(P_Scripture.ADToString());
            Console.Write("Press ENTER to continue or type quit: ");
            ADUserInput = Console.ReadLine().ToLower();
            ADNotCompletelyHidden = P_Scripture.ADHideWord();
        } while (ADNotCompletelyHidden && ADUserInput != "quit");
    }
    
    static void Main(string[] args) {
        int ADMenuOption;
        do {
            Console.Clear();
            ADDisplayMenu();
            ADMenuOption = ADGetMenuSelection();
            switch (ADMenuOption) {
                case 1: {
                    string ADChosenReference = ADGetString("Type in the reference: ");
                    ADScripture ADChosenPassage = new(ADChosenReference);
                    ADIncrementallyHideScripture(ADChosenPassage);
                    break;
                }
                case 2: {
                    ADScripture ADRandomScripture = new();
                    ADIncrementallyHideScripture(ADRandomScripture);
                    break;
                }
                case 3: {
                    string ADChosenReference = ADGetString("Type in the reference (one verse at a time only, ie 1 Nephi 3:7): ");
                    string ADChosenPassage = ADGetString("Type in the verse (not including the verse number): ");
                    ADScripture.ADRememberScripture(ADChosenReference, ADChosenPassage);
                    break;
                }
                case 4:
                    Console.WriteLine("Thanks for using the scripture memorizer program!");
                    break;
                default:
                    Console.WriteLine("I don't know the menu option of "+ADMenuOption);
                    break;
            }
        } while (ADMenuOption != 4);
    }
}