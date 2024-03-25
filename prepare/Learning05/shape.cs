
public abstract class Shape {
    // Attributes:
    private string _color;

    public Shape(string color) {
        _color = color;
    }

    // Methods:
    public abstract double GetArea();
    public string GetColor() {
        return _color;
    }
}

public class Rectangle : Shape {
    // Attributes:
    private double _height;
    private double _width;
    
    // Constructors:
    public Rectangle(string color, double height, double width) : base(color) {
        _height = height;
        _width = width;
    }

    // Methods:
    public override double GetArea() {
        return _height * _width;
    }
}

public class Circle : Shape {
    // Attributes:
    private double _radius;

    // Constructors:
    public Circle(string color, double radius) : base(color) {
        _radius = radius;
    }

    // Methods:
    public override double GetArea() {
        return _radius * _radius * Math.PI;
    }
}

public class Triangle : Shape {
    // Attributes:
    private double _height;
    private double _width;
    
    // Constructors:
    public Triangle(string color, double height, double width) : base(color) {
        _height = height;
        _width = width;
    }

    // Methods:
    public override double GetArea() {
        return _height * _width / 2;
    }
}