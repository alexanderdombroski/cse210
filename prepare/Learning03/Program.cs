using System;

class Program
{
    static void Main(string[] args)
    {
        Fraction defaultFraction = new();
        Fraction wholeFraction = new(6);
        Fraction rationalFraction = new(6, 7);

        Console.WriteLine(defaultFraction.GetFractionString());
        Console.WriteLine(defaultFraction.GetDecimalValue());
        Console.WriteLine(wholeFraction.GetFractionString());
        Console.WriteLine(wholeFraction.GetDecimalValue());
        Console.WriteLine(rationalFraction.GetFractionString());
        Console.WriteLine(rationalFraction.GetDecimalValue());
    }
}