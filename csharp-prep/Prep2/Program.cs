using System;

class Program
{   
    static char CalculateGradeLetter(float gradePercentage) {
        if (gradePercentage >= 90) {
            return 'A';
        } else if (gradePercentage >= 80) {
            return 'B';
        } else if (gradePercentage >= 70) {
            return 'C';
        } else if (gradePercentage >= 60) {
            return 'D';
        } else {
            return 'F';
        }
    }
    static string CalculateGradeSign(float gradePercentage) {
        if (gradePercentage % 10 <= 3 && gradePercentage >= 60) {
            return "-";
        } else if (gradePercentage % 10 >= 7 && gradePercentage >= 60 && gradePercentage < 90) {
            return "+";
        } else {
            return "";
        }
    }
    static void Main(string[] args) {
        Console.Write("Enter your grade percentage: ");
        float grade = float.Parse(Console.ReadLine());
        char gradeLetter = CalculateGradeLetter(grade);
        string gradeSign = CalculateGradeSign(grade);
        Console.WriteLine($"Your grade letter is an {gradeLetter + gradeSign}");
    }
}