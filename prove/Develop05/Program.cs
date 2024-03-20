using System;

class Program
{
    static void ADDisplayMenu() {
        Console.Clear();
        Console.WriteLine("Please Select a Mindfulness Activity");
        Console.WriteLine("    1. Breathing Activity");
        Console.WriteLine("    2. Reflection Activity");
        Console.WriteLine("    3. Listing Activity");
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

    static void Main(string[] args) {
        ADDisplayMenu();
        int ADOption = ADGetMenuSelection(); 
        switch (ADOption) {
            case 1: {
                
                break;
            }
            case 2: {

                break;
            }
            case 3: {

                break;
            }
            case 4:
                Console.WriteLine("Thanks for using the Mindfulness Activity Program");
                break;
            default:
                Console.WriteLine("Menu option retrieval system is broken");
                break;
        }
    }
}