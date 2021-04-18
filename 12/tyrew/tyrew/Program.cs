using System;

namespace samost
{
    abstract class Mechanism
    {
        public int MaxHeight { get; private set; }
        public int CurrentHeigth { get; private set; }

        public Mechanism(int maxHeigth)
        {
            MaxHeight = maxHeigth;
            CurrentHeigth = 0;
        }

        public void TakeUpper(int delta)
        {
            if (delta <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (CurrentHeigth + delta >= MaxHeight)
            {
                CurrentHeigth = MaxHeight;
            }
            else
            {
                CurrentHeigth += delta;
            }
        }

        public void TakeLower(int delta)
        {
            if (delta <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (CurrentHeigth - delta > 0) CurrentHeigth -= delta;
            else if (CurrentHeigth - delta == 0) CurrentHeigth = 0;
            else if (CurrentHeigth - delta < 0) throw new InvalidOperationException("Crash!");
        }

        abstract public void WriteAllProperties();
    }

    class Helicopter : Mechanism
    {
        public byte BladesCount { get; private set; }

        public Helicopter(int maxHeigth, byte bladesCount) : base(maxHeigth)
        {
            BladesCount = bladesCount;
        }

        public override void WriteAllProperties()
        {
            Console.WriteLine("BladesCount: " + BladesCount);
            Console.WriteLine("CurrentHeigth: " + CurrentHeigth);
            Console.WriteLine("MaxHeight: " + MaxHeight);
        }
    }

    class Plane : Mechanism
    {
        public byte EngineCount { get; private set; }

        public Plane(int maxHeigth, byte engineCount) : base(maxHeigth)
        {
            EngineCount = engineCount;
        }

        public override void WriteAllProperties()
        {
            Console.WriteLine("EngineCount: " + EngineCount);
            Console.WriteLine("CurrentHeigth: " + CurrentHeigth);
            Console.WriteLine("MaxHeight: " + MaxHeight);
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Plane p = new Plane(100, 2);
            Helicopter h = new Helicopter(60, 10);

            p.WriteAllProperties();
            h.WriteAllProperties();
        }
    }
}
