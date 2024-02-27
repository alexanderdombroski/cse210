using System;

class Program
{
    static string formatBondName (string firstName, string lastName) {
        return $"{lastName}, {firstName} {lastName}";
    }
    
    static void Main(string[] args) {
        Console.WriteLine("Hello Prep1 World!");
        Console.Write("What is your first name: ");
        string firstName = Console.ReadLine();
        Console.Write("What is your last name: ");
        string lastName = Console.ReadLine();
        Console.WriteLine($"You are {formatBondName(firstName, lastName)}");
    }
}