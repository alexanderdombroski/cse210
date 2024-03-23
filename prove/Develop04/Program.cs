// Alex Dombroski - March 23 2024
// Mindfulness Program
//
/* CREATIVITY - USE CTRL F
    * Additional Gratititude Activity
    * Pull in Constructor parameters from a JSON file 
    * Used several different pause animations
    * Learned additional concepts to simplify each activity's child code
 * Additional Concepts Learned
    * Method Overloading
    * Working with JSON files
    * Using Action and Func<T> datatypes
    * Using interfaces to require methods in child classes
    * Basic understanding of Generic methods and polymorphism
*/ // C# is a pretty epic programming language!

using System;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Data;

class Program {

    static JsonObject ADGetJsonObject(string P_Filepath) {
        // Deserialize a JSON object from a given file and return it
        string ADJsonString = File.ReadAllText(P_Filepath); 
        JsonObject ADJsonObject = JsonSerializer.Deserialize<JsonObject>(ADJsonString);
        return ADJsonObject;
    }

    static List<string> ADJsonArrayToList(JsonArray P_JsonArray) {
        // Convert a Json Array to a list using S
        return P_JsonArray.Select(node => node.ToString()).ToList();
    } 
    
    static List<object> ADMakeActivityConstructorList(string P_ActivityKey) {
        JsonObject ADActivityData = ADGetJsonObject("ActivityData.json");
        JsonObject ADChosenActivityData = ADActivityData[P_ActivityKey].AsObject();
        List<object> ConstructArgs = new();
        foreach (var ADValue in ADChosenActivityData.AsEnumerable().Select(p => p.Value)) {
            if (ADValue is JsonValue value) {
                if (int.TryParse(value.ToString(), out int ADIntValue)) {
                    ConstructArgs.Add(ADIntValue);
                } else if (double.TryParse(value.ToString(), out double ADDoubleValue)) {
                    ConstructArgs.Add(ADDoubleValue);
                } else {
                    ConstructArgs.Add(value.ToString());
                }
            } else if (ADValue is JsonArray ADManyValue) {
                ConstructArgs.Add(ADJsonArrayToList(ADManyValue.AsArray()));
            }
        }
        return ConstructArgs;
    }

    static void ADRunActivity<T>(string P_ActivityKey) where T : ADActivity, ADActivity.ADIRunnable {
        List<object> ADConstructorArgs = ADMakeActivityConstructorList(P_ActivityKey);
        T ADChosenActivity = (T)Activator.CreateInstance(typeof(T), ADConstructorArgs.ToArray());
        ADChosenActivity.ADRun();
    }

    static void ADDisplayMenu() {
        Console.Clear();
        Console.WriteLine("Please Select a Mindfulness Activity");
        Console.WriteLine("    1. Breathing Activity");
        Console.WriteLine("    2. Reflection Activity");
        Console.WriteLine("    3. Listing Activity");
        Console.WriteLine("    4. Gratitude Activity");
        Console.WriteLine("    5. Quit");
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
        } while (ADOption < 1 || ADOption > 5 || ADInvalidResponse);
        return ADOption;
    }

    static void Main(string[] args) {
        int ADOption;
        do {
            ADDisplayMenu();
            ADOption = ADGetMenuSelection(); 
            switch (ADOption) {
                case 1: 
                    ADRunActivity<ADBreathing>("Breathing");
                    break;
                case 2: 
                    ADRunActivity<ADReflection>("Reflection");
                    break;
                case 3:
                    ADRunActivity<ADListing>("Listing");
                    break;
                case 4:
                    ADRunActivity<ADGratitude>("Gratitude");
                    break;
                case 5:
                    Console.WriteLine("Thanks for using the Mindfulness Activity Program.");
                    break;
                default:
                    Console.WriteLine("Menu option retrieval system is broken.");
                    break;
            }
        } while (ADOption < 5);
    }
}