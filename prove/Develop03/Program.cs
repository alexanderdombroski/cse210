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

    static string ADGetReference() {
        Console.Write("Type in the reference: ");
        return Console.ReadLine();
    }

    static void ADHideScripture(ADScripture P_Scripture) {
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
                case 1:
                    string ADChosenReference = ADGetReference();
                    ADScripture ADChosenScripture = new(ADChosenReference);
                    ADHideScripture(ADChosenScripture);
                    break;
                case 2:
                    ADScripture ADRandomScripture = new();
                    ADHideScripture(ADRandomScripture);
                    break;
                case 3:

                    break;
                case 4:
                    Console.WriteLine("Thanks for using the scripture memorizer program");
                    break;
                default:
                    Console.WriteLine("I don't know the menu option of "+ADMenuOption);
                    break;
            }
        } while (ADMenuOption != 4);
    }
}