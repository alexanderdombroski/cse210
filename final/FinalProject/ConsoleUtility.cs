using System;
using System.Text;
using System.IO;
public static class ConsoleUtility {
    // Methods:
    private static void ColorWrite(string output, char delimiter, Dictionary<string, ConsoleColor> colorKey) {
        // Writes a single word in the correct color, and writes the character after the word (ie space)
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
        // Writes a passage of words and colors them
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
        // Prompts the user to press ENTER
        WriteRead("\nPress ENTER to Continue");
    }
    public static void PauseWrite(string prompt, int waitTime) {
        Console.Write(prompt);
        PauseMiliseconds(waitTime);
    }
    public static string WriteRead(string prompt) {
        Console.Write(prompt);
        return Console.ReadLine();
    }
    public static string GetAbsolutePath(string prompt = "Type the file path: ") {
        // Gets a filepath, checks validity, and returns it
        bool InvalidResponse = true;
        string response = "";
        while (InvalidResponse) {
            response = WriteRead(prompt);
            if (!Path.IsPathRooted(response)) {
                response = Path.GetFullPath(response);
            }
            if (Directory.Exists(response)) {
                InvalidResponse = false;
            } else {
                Console.WriteLine("Enter a valid path");
            }
        }
        if (!response.EndsWith('/')) {
            response += '/';
        }
        return response;
    }
}