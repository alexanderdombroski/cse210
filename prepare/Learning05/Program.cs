using System;

class Program {
    static void Main(string[] args) {
        Triangle tri = new("green", 5, 2);
        Circle cir = new("orange", 4);
        Rectangle rect = new("blue", 5, 6);
        List<Shape> shapeList = new() {
            tri, cir, rect
        };

        shapeList.ForEach(s => Console.WriteLine($"{s.GetColor()}: {s.GetArea()}"));

    }
}