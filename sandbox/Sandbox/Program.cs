using System;

class Program
{
    static void Main(string[] args)
    {
        // Console.WriteLine("Hello Sandbox World!");
        // Console.Write("What is your first name: ");
        // string firstName = Console.ReadLine();
        // Console.Write("What is your last name: ");
        // string lastName = Console.ReadLine();
        // Console.WriteLine("You are " + lastName + ", " + firstName + ' ' + lastName);
        // Console.WriteLine($"You are {lastName}, {firstName} {lastName}");
        // Console.WriteLine("You are {0}, {1} {0}", lastName, firstName);
        // Console.WriteLine(String.Format("You are {0}, {1} {0}", lastName, firstName));

        int int1 = 5;
        int int2 = 2;
        if (int1 > int2) {
            Console.WriteLine($"{int1} is greater than {int2}");
        } else if (int1 < int2) {
            Console.WriteLine($"{int1} is less than {int2}");
        } else {
            Console.WriteLine($"{int1} equals {int2}");
        }
    }
}