using System;

class Program
{
    static int GetMagicNumber() {
        // Unused part 1 code
        Console.Write("What is the magic integer number? ");
        return int.Parse(Console.ReadLine());
    }
    static int GetIntInput(string prompt) {
        bool isNotInt = true;
        string inputInt;
        do {
            Console.Write(prompt);
            inputInt = Console.ReadLine();
            try {
                int.Parse(inputInt);
                isNotInt = false;
            } catch {
                Console.WriteLine("Please enter a valid integer");
            }
        } while(isNotInt);
        return int.Parse(inputInt);
    }
    static void Main(string[] args)
    {
        Random randGenerator = new Random();
        int secretInt = randGenerator.Next(1, 11);
        int guessedInt;
        do {
            guessedInt = GetIntInput("Guess a number between 1 and 10: ");
            if (guessedInt > secretInt) {
                Console.WriteLine("Guess Lower");
            } else if (guessedInt < secretInt) {
                Console.WriteLine("Guess Higher");
            }
        } while (secretInt != guessedInt);
        Console.WriteLine("You guessed it!");
    }
}