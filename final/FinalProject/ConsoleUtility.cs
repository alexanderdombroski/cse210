using System;
using System.Text;
public static class ConsoleUtility {
    // Methods:
    private static void ColorWrite(string output, char delimiter, Dictionary<string, ConsoleColor> colorKey) {
        if (colorKey.TryGetValue(output, out ConsoleColor color)) {
            Console.ForegroundColor = color;
            Console.Write(output);
            Console.ResetColor();
        } else {
            Console.Write(output);
        }
        Console.Write(delimiter);
    }

    public static void ColorPassage(string output, Dictionary<string, ConsoleColor> colorKey) {
        List<char> delimiters = new() {' ', ',', ';', ':', '.', '?', '!', '{', '}', '(', ')', '[', ']', '<', '>'};
        StringBuilder currentWord = new();
        foreach (char c in output) {
            if (delimiters.Contains(c)) {
                ColorWrite(currentWord.ToString(), c, colorKey);
                currentWord.Clear();
            } else {
                currentWord.Append(c);
            }
        }
        ColorWrite(currentWord.ToString(), '\n', colorKey);
    }
    public static void PauseMiliseconds(int pauseTime, List<string> animationCharList = null, int animationFrameInterval = 250) {
        // Pauses for a length of time, and displays a given or default animation from a list
        animationCharList ??= new() {"+", "*", "#"};
        int animationCount = pauseTime / animationFrameInterval;
        for (int i=0; i<animationCount; i++) {
            Console.Write(animationCharList[i % animationCharList.Count]);
            Thread.Sleep(animationFrameInterval);
            Console.Write("\b \b");
        }
        Console.WriteLine();
    }
    public static void WaitForUser() {
        Console.Write("\nPress ENTER to Continue");
        Console.ReadLine();
    }
}