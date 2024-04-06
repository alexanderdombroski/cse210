using System;
using System.Text;

class Program {
    
    static void Main(string[] args) {
        // Chat GPT example of text coloring
        string sentence = "The quick brown fox jumps over the lazy dog";
        string[] words = sentence.Split(' ');

        ConsoleColor[] rainbowColors = {
            ConsoleColor.Red,
            ConsoleColor.Yellow,
            ConsoleColor.Green,
            ConsoleColor.Blue,
            ConsoleColor.Magenta,
            ConsoleColor.Cyan
        };

        int colorIndex = 0;

        foreach (string word in words) {
            // Set text color to the current rainbow color
            Console.ForegroundColor = rainbowColors[colorIndex];

            // Print the word with the current color
            Console.Write(word + " ");

            // Increment color index and wrap around if necessary
            colorIndex = (colorIndex + 1) % rainbowColors.Length;
        }

        // Reset text color to default
        Console.ResetColor();
        Console.WriteLine();

    }
}