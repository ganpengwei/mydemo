using System;

namespace ConsoleApp
{
    class Program
    {
        public delegate void _event();
        public event _event MyE;
        public delegate void Expression(string str);

        public void _main()
        {
            //ConsoleKey con = ConsoleKey.Escape;
            //while (true)
            //{
            //    while (Console.KeyAvailable)
            //    {
            //        Console.WriteLine("!!!!!");
            //        var k = Console.ReadKey(true);
            //        Console.WriteLine("按下" + k.Key + ";" + k.Modifiers);
            //    }
            //}
            if (MyE != null)
                this.MyE();
        }

        public void _model()
        {
            Console.WriteLine("model");
        }

        public static void _read()
        {
            //var k = Console.ReadLine();
            //Console.WriteLine("输出：" + k + ",");
            Console.WriteLine("_read");
        }

        public static void _main(string str)
        {
            Console.WriteLine("_main");
        }
        static void Main(string[] args)
        {
            Console.WriteLine(" hello world ");
            Program p = new Program();
            p.MyE += p._model;
            p.MyE += new _event(_read);
            An(_main, "AA");
            p._main();
            string v = "b";
            string s = "a";
            p.O(out v, ref s);
            Console.WriteLine(v + "," + s);
            var tm = typeof(myEnum);
            var t = Enum.GetNames(typeof(myEnum));
            var ev = Enum.GetValues(typeof(myEnum));
            foreach (var e in ev)
            {
                Console.WriteLine(Convert.ToInt32(e) + e.ToString());
            }
            new Circle("Red");
            Circle Cir = new Circle("Orange", 3.0);
            Console.WriteLine("Circle area is{1}", Cir.GetColor(), Cir.GetArea());
            Retangular Rect = new Retangular("Red", 13.0, 2.0);
            Console.WriteLine("Retangular Color is {0},Rectangualr area is {1},Rectangualr Perimeter is {2}",
                Rect.GetColor(), Rect.GetArea(), Rect.PerimeterIs());
            Square Squ = new Square("qreen", 5.0);
            Console.WriteLine("Square Color is {0},Square Area is {1},Square Perimeter is {2}", Squ.GetColor(), Squ.GetArea(), Squ.PerimeterIs());

            Console.ReadLine();
        }

        public void O(out string v, ref string s)
        {
            v = "v";
            s = v;
        }

        public static void An(Expression e, string str)
        {
            Console.WriteLine(str);
        }
    }

    public abstract class Shape
    {
        protected string Color;
        public Shape() {; }   //构造函数
        public Shape(string Color) { this.Color = Color; }
        public string GetColor() { return Color; }
        public abstract double GetArea();   //抽象类
    }
    //定义Cirle类，从Shape类中派生
    public class Circle : Shape
    {
        private double Radius;
        public Circle(string Color, double Radius)
        {
            this.Color = Color;
            this.Radius = Radius;
        }
        public override double GetArea()
        {
            return System.Math.PI * Radius * Radius;
        }

        public Circle(string color) : base(color)
        {
            Console.WriteLine("处理后：" + this.GetColor());
        }

    }
    //派生类Rectangular,从Shape类中派生
    public class Retangular : Shape
    {
        protected double Length, Width;
        public Retangular(string Color, double Length, double Width)
        {
            this.Color = Color;
            this.Length = Length;
            this.Width = Width;
        }
        public override double GetArea()
        {
            return (Length * Width);
        }

        public double PerimeterIs()
        {
            return (2 * (Length * Width));

        }
    }
    //派生类Square，从Retangular类中派生
    public class Square : Retangular
    {
        public Square(string Color, double Side) : base(Color, Side, Side) {; }
    }

    public enum myEnum
    {
        正常 = 1,
        错误 = 2
    }

    public class Text
    {

    }
}
