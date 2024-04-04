public class MenuBuilder {
    // Attributes:
    private readonly string _menuTitle;
    private readonly List<string> _menuOptions;
    private readonly List<Action> _menuFunctions;
    
    // Constructors:
    public MenuBuilder(string menuTitle, List<string> menuOptions, List<Action> menuFunctions) {
        _menuTitle = menuTitle;
        _menuOptions = menuOptions;
        _menuFunctions = menuFunctions;
    }

    // Methods:
    public void RunMenu() {
        int menuChoice;
        do {
            menuChoice = DisplayMenu();
            if (menuChoice != _menuOptions.Count)
                _menuFunctions[menuChoice - 1]();
        } while (menuChoice != _menuFunctions.Count);
    }

    private int DisplayMenu() {
        Console.Clear();
        Console.WriteLine(_menuTitle);
        for (int i=0; i<_menuOptions.Count; i++) {
            Console.WriteLine($"  {i+1}. {_menuOptions[i]}");
        }
        return ConsoleUtility.GetValidInt("Which option do you choose?", 1, _menuOptions.Count);
    }
}