using System;

class Program
{
    static int FindSmallestPositive(List<int> numbersList) {
        int smallestNumber = numbersList.Max();
        foreach (int number in numbersList) {
            if (number < smallestNumber && number > 0) {
                smallestNumber = number;
            }
        }
        return smallestNumber;
    }
    static void Main(string[] args) {
        Console.WriteLine("Enter a list of numbers. Type zero to stop");
        List<int> numbersList = new();
        int answer;
        do {
            Console.Write("Enter a number: ");
            answer = int.Parse(Console.ReadLine());
            if (answer != 0) {
                numbersList.Add(answer);
            }
        } while(answer != 0);
        int sum = numbersList.Sum();
        Console.WriteLine($"The sum is {sum}");
        Console.WriteLine($"The average is: {sum / numbersList.Count}");
        Console.WriteLine($"The largest number is {numbersList.Max()}");
        Console.WriteLine($"The smallest number is {numbersList.Min()}");
        Console.WriteLine($"The smallest positive number is {FindSmallestPositive(numbersList)}");
        numbersList.Sort();
        Console.Write($"The sorted list is: ");
        numbersList.ForEach(number => Console.Write($"{number} "));
        Console.WriteLine();
    }
}