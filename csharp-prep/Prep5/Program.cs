using System;

class Program
{
    static void DisplayWelcome() {
        Console.WriteLine("Welcome to the Program!");
    }
    
    static string PromptUserName() {
        Console.Write("Enter your name: ");
        return Console.ReadLine();
    } 

    static int PromptUserNumber() {
        Console.Write("Enter your favorite number: ");
        return int.Parse(Console.ReadLine());
    }
    
    static int SquareNumber(int number) {
        return number * number;
    }

    static void DisplayResult(string name, int number) {
        Console.WriteLine($"{name}, the square of your number is {number}");
    }

    static void Main(string[] args) {
        DisplayWelcome();
        string name = PromptUserName();
        int favNumber = PromptUserNumber();
        int newNumber = SquareNumber(favNumber);
        DisplayResult(name, newNumber);
    }
}