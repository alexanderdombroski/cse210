using System;

class Program {
    
    static void ADDisplayMenu() {
        Console.WriteLine("What would you like to do?");
        Console.WriteLine("\t1. Write an entry");
        Console.WriteLine("\t2. Display your Journal");
        Console.WriteLine("\t3. Save your Journal");
        Console.WriteLine("\t4. Load a previous Jornal");
        Console.WriteLine("\t5. Quit");
    }

    static int ADGetValidInt(string prompt = "Type in a number: ") {
        bool ADInvalidResponse;
        int ADIntResponse = 0;
        do {
            ADInvalidResponse = false;
            Console.Write(prompt);
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
    
    static void Main(string[] args) {
        Console.WriteLine("Welcome to the Journal Program!");
        int ADMenuOption;
        do {
            ADDisplayMenu();
            ADMenuOption = ADGetMenuSelection();
            switch (ADMenuOption) {
                case 1:

                    break;
                case 2:

                    break;
                case 3:

                    break;
                case 4:

                    break;
                case 5:
                    Console.WriteLine("Thanks for using the Journal Program");
                    break;
                default:
                    Console.WriteLine("You've selected an invalid option and the somehow broken the program");
                    break;
            }
        } while (ADMenuOption != 5);
    }
}