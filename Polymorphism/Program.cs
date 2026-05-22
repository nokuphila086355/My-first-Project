using System;
using System.Collections.Generic;
using System.Linq;

namespace Polymorphism
{
    // Abstract base class representing a generic shape.
    // Concrete shapes must provide an implementation for Area() and Perimeter().
    abstract class Shape
    {
        // Abstract method for area: must be implemented by derived types.
        public abstract double Area();

        // Abstract method for perimeter: must be implemented by derived types.
        public abstract double Perimeter();

        // Virtual Describe method provides a default textual representation.
        // It uses the concrete implementation of Area() and Perimeter() via virtual dispatch.
        public virtual string Describe()
        {
            return $"{GetType().Name}: Area={Area():F2}, Perimeter={Perimeter():F2}";
        }
    }

    // Circle implements Shape. It stores a radius and computes area/perimeter accordingly.
    class Circle : Shape
    {
        // Read-only auto-property for the radius.
        public double Radius { get; }

        // Single-argument constructor to initialize radius.
        public Circle(double radius) => Radius = radius;

        // Area formula: πr²
        public override double Area() => Math.PI * Radius * Radius;

        // Perimeter formula (circumference): 2πr
        public override double Perimeter() => 2 * Math.PI * Radius;
    }

    // Rectangle implements Shape with width and height.
    class Rectangle : Shape
    {
        public double Width { get; }
        public double Height { get; }

        // Constructor initializes width and height.
        public Rectangle(double width, double height)
        {
            Width = width;
            Height = height;
        }

        // Area formula: width * height
        public override double Area() => Width * Height;
        public double Area(string width ) => Width * Height;

        // Perimeter formula: 2 * (width + height)
        public override double Perimeter() => 2 * (Width + Height);
    }

    // Triangle implements Shape. For simplicity, the constructor accepts base and height.
    // The Area() uses 0.5 * base * height. Perimeter() assumes a right triangle
    // with legs equal to base and height; if you need a general triangle provide three sides.
    class Triangle : Shape
    {
        // Base length and height (perpendicular to base)
        public double BaseLength { get; }
        public double Height { get; }

        public Triangle(double baseLength, double height)
        {
            BaseLength = baseLength;
            Height = height;
        }

        // Area of triangle: 0.5 * base * height
        public override double Area() => 0.5 * BaseLength * Height;

        // Perimeter for the assumed right triangle: base + height + hypotenuse
        public override double Perimeter()
        {
            // Calculate hypotenuse using Pythagoras
            var hypotenuse = Math.Sqrt(BaseLength * BaseLength + Height * Height);
            return BaseLength + Height + hypotenuse;
        }
    }

    class Program
    {
        // PrintReport iterates a list of Shape references and prints their descriptions.
        // It also demonstrates a LINQ usage to find the shape with the largest area.
        static void PrintReport(List<Shape> shapes)
        {
            Console.WriteLine("Shape report\n--------------");

            // Iterate over the collection. Each 's' is typed as Shape, but at runtime
            // s.Area() and s.Describe() call the concrete implementation due to virtual dispatch.
            foreach (var s in shapes)
            {
                // Describe() is virtual on Shape; the concrete type's methods are invoked.
                Console.WriteLine(s.Describe());
            }

            // Use LINQ to find the shape with the largest area.
            // Explanation:
            // - OrderByDescending(s => s.Area()): sorts the shapes so the one with the highest
            //   Area() value comes first. Area() is evaluated for each element.
            // - FirstOrDefault(): returns the first element from the ordered sequence
            //   or null if the collection is empty.
            var largest = shapes.OrderByDescending(s => s.Area()).FirstOrDefault();

            if (largest != null)
            {
                Console.WriteLine();
                Console.WriteLine("Largest by area:");
                Console.WriteLine(largest.Describe());
            }
        }

        static void Main()
        {
            // Create a mixed list of shapes. Note that the list stores Shape references
            // but the objects are concrete types (Circle, Rectangle, Triangle).
            var shapes = new List<Shape>
            {
                new Circle(3),        // radius = 3
                new Rectangle(4, 5),  // width = 4, height = 5
                new Triangle(3, 4),   // base = 3, height = 4 (right triangle assumed)
                new Circle(5.5)       // radius = 5.5
            };

            // Print the report (demonstrates polymorphism at runtime).
            PrintReport(shapes);

            // Additional demo notes explaining runtime dispatch and the LINQ query.
            Console.WriteLine();
            Console.WriteLine("Demo notes:");
            Console.WriteLine("- When Area() is called on a Shape reference, the runtime performs virtual dispatch and executes the concrete implementation (Circle/Rectangle/Triangle) based on the object's actual type.");
            Console.WriteLine("- The LINQ query used to find the largest shape is: shapes.OrderByDescending(s => s.Area()).FirstOrDefault();");
            Console.WriteLine("  * shapes: the collection to query");
            Console.WriteLine("  * OrderByDescending(s => s.Area()): sorts elements so the largest Area is first (Area() is evaluated for each element)");
            Console.WriteLine("  * FirstOrDefault(): picks the first element from the sorted sequence, or null if empty");
        }
    }
}
