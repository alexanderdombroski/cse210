public static class MenuUtility {
    // Methods:
    public static void RunMenu(string menuTitle, List<string> menuOptions, List<Action> menuFunctions) {
        // Function that builds a menu, gets user option, and runs the function associated with it.
        int menuChoice;
        do {
            menuChoice = DisplayMenu(menuTitle, menuOptions);
            if (menuChoice != menuOptions.Count)
                menuFunctions[menuChoice - 1]();
        } while (menuChoice != menuOptions.Count);
    }
    public static int DisplayMenu(string menuTitle, List<string> menuOptions, string prompt = "Which one do you choose? ") {
        // Builds a menu and gets a valid option (by it's number)
        Console.Clear();
        Console.WriteLine(menuTitle);
        for (int i=0; i<menuOptions.Count; i++) {
            Console.WriteLine($"  {i+1}. {menuOptions[i]}");
        }
        return GetValidInt(prompt, 1, menuOptions.Count);
    }
    public static int GetValidInt(string prompt, int lowerBound, int upperBound) {
        // Repeatedly asks user for a menu option choice until a valid answer is given
        bool invalidResponse;
        int returnValue;
        do {
            Console.Write(prompt);
            string response = Console.ReadLine();
            invalidResponse = !int.TryParse(response, out returnValue);
        } while (returnValue < lowerBound || returnValue > upperBound || invalidResponse);
        return returnValue;
    }
    public interface IMenu {
        // Interface closely associated with the static methods of this class
        public void RunMenu();
    }
}