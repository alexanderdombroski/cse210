using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello Sandbox World!");
        Console.Write("What is your first name: ");
        string firstName = Console.ReadLine();
        Console.Write("What is your last name: ");
        string lastName = Console.ReadLine();
        Console.WriteLine("You are " + lastName + ", " + firstName + ' ' + lastName);
    }
}