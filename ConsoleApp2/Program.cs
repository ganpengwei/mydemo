using System;
using System.Collections.Generic;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            Host host = new Host();

            //当前时间，从2008年12月31日23:59:50开始计时
            DateTime now = new DateTime(2015, 12, 31, 23, 59, 50);
            DateTime midnight = new DateTime(2016, 1, 1, 0, 0, 0);

            //等待午夜的到来
            Console.WriteLine("时间一秒一秒地流逝... ");
            while (now < midnight)
            {
                Console.WriteLine("当前时间: " + now);
                System.Threading.Thread.Sleep(1000);    //程序暂停一秒
                now = now.AddSeconds(1);                //时间增加一秒
            }

            //午夜零点小偷到达,看门狗引发Alarm事件
            Console.WriteLine("\n月黑风高的午夜: " + now);
            Console.WriteLine("小偷悄悄地摸进了主人的屋内... ");
            //dog.OnAlarm();
            Console.ReadLine();
        }
    }

    class Dog
    {
        //1.声明关于事件的委托；
        public delegate void AlarmEventHandler(string inData);

        //2.声明事件；   
        public event AlarmEventHandler Alarm;

        //3.编写引发事件的函数；
        public void OnAlarm()
        {
            if (this.Alarm != null)
            {
                Console.WriteLine("\n狗报警: 有小偷进来了,汪汪~~~~~~~");
                this.Alarm("");   //发出警报
            }
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }

    //事件接收者
    class Host
    {
        //４.编写事件处理程序
        void HostHandleAlarm(string inData)
        {
            Console.WriteLine("主人: 抓住了小偷！");
        }

        void A(string inData)
        {
            Console.WriteLine("A");
        }

        //５.注册事件处理程序
        public Host()
        {
            Dog dog = new Dog();
            dog.Alarm += new Dog.AlarmEventHandler(A);
            dog.OnAlarm();
            List<Dog> dogs = new List<Dog>();

            People p = new People();
            p.Name = "aa";
            var nc = p.Name.Clone();
            var c = p.Clone();

            IPeople people = p;
            Men men = new Men("aa");

        }
    }

    public interface IPeople
    {
        string Name { get; set; }
    }

    public class People : IPeople, ICloneable
    {
        public string Name { get; set; }
        public People()
        {
        }

        public People(string Name)
        {
            this.Name = Name;
        }

        object ICloneable.Clone()
        {
            return Clone();
        }

        public People Clone()
        {
            return (People)this.MemberwiseClone();
        }
    }


    public class Men : People
    {
        public Men() { }
        public Men(string name) : base(name)
        {

        }
    }

    public class Clone<T> where T : class, ICloneable
    {

    }
}
