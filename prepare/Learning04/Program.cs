using System;

class Program
{
    static void Main(string[] args)
    {
        ADMathAssignment ADMath = new("Mike Smith", "Summations", "4.2", "6-9, 14-15");
        ADWritingAssignment ADWriting = new("Mike Smith", "English", "Argumentative Essay #21");
        Console.WriteLine(ADMath.ADGetHomeworkList());
        Console.WriteLine(ADWriting.ADGetWritingInformation());
    }
}