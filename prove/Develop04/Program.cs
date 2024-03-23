using System;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Linq;
using System.Data;

class Program {

    static JsonObject ADGetJsonObject(string P_Filepath) {
        string ADJsonString = File.ReadAllText(P_Filepath); 
        JsonObject ADJsonObject = JsonSerializer.Deserialize<JsonObject>(ADJsonString);
        return ADJsonObject;
    }
    
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

    static List<string> ADJsonArrayToList(JsonArray P_JsonArray) {
        return P_JsonArray.Select(node => node.ToString()).ToList();
    }

    static void ADRunActivity<T>(JsonObject P_ActivityData, string P_ActivityKey) where T : ADActivity, ADActivity.ADIRunnable {
        List<object> ADConstructorArgs = ADMakeActivityConstructorList(P_ActivityData, P_ActivityKey);
        T ADChosenActivity = (T)Activator.CreateInstance(typeof(T), ADConstructorArgs.ToArray());
        ADChosenActivity.ADRun();
    }

    static List<object> ADMakeActivityConstructorList(JsonObject P_ActivityData, string P_ActivityKey) {
        JsonObject ADChosenActivityData = P_ActivityData[P_ActivityKey].AsObject();
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

    static void Main(string[] args) {
        ADDisplayMenu();
        int ADOption = ADGetMenuSelection(); 
        JsonObject ADActivityData = ADGetJsonObject("ActivityData.json");
        switch (ADOption) {
            case 1: {
                ADRunActivity<ADBreathing>(ADActivityData, "Breathing");
                break;
            }
            case 2: {
                ADRunActivity<ADReflection>(ADActivityData, "Reflection");
                break;
            }
            case 3: {
                ADRunActivity<ADListing>(ADActivityData, "Listing");
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