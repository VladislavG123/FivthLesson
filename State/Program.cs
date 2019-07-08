// Паттерн Состояние
//
// Назначение: Позволяет объектам менять поведение в зависимости от своего
// состояния. Извне создаётся впечатление, что изменился класс объекта.

using System;

namespace DesignPatterns.State.Concrete
{
    class Program
    {
        static void Main(string[] args)
        {
            Water water = new Water(new LiquidWaterState());

            water.Heat();
            water.Frost();
            water.Frost();
            water.Heat();
            water.Heat();
            water.Heat();
            water.Heat();
            water.Frost();


            Console.Read();
        }
    }

    class Water
    {
        public IWaterState State { get; set; }

        public Water(IWaterState ws)
        {
            State = ws;
        }

        public void Heat()
        {
            State.Heat(this);
        }
        public void Frost()
        {
            State.Frost(this);
        }
    }

    interface IWaterState
    {
        void Heat(Water water);
        void Frost(Water water);
    }

    class SolidWaterState : IWaterState
    {
        public void Heat(Water water)
        {
            Console.WriteLine("Превращаем лед в жидкость");
            water.State = new LiquidWaterState();
        }

        public void Frost(Water water)
        {
            Console.WriteLine("Продолжаем заморозку льда");
        }
    }

    class LiquidWaterState : IWaterState
    {
        public void Heat(Water water)
        {
            Console.WriteLine("Превращаем жидкость в пар");
            water.State = new GasWaterState();
        }

        public void Frost(Water water)
        {
            Console.WriteLine("Превращаем жидкость в лед");
            water.State = new SolidWaterState();
        }
    }

    class GasWaterState : IWaterState
    {
        public void Heat(Water water)
        {
            Console.WriteLine("Превращаем пар в плазму");
            water.State = new PlazmWaterState();
        }

        public void Frost(Water water)
        {
            Console.WriteLine("Превращаем водяной пар в жидкость");
            water.State = new LiquidWaterState();
        }
    }

    class PlazmWaterState : IWaterState
    {
        public void Frost(Water water)
        {
            Console.WriteLine("Превращаем плазму в пар");
            water.State = new LiquidWaterState();
        }

        public void Heat(Water water)
        {
            Console.WriteLine("Повышаем температуру плазмы");
        }
    }
}
