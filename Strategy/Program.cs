// Паттерн Стратегия
//
// Назначение: Определяет семейство схожих алгоритмов и помещает каждый из них в
// собственный класс, после чего алгоритмы можно взаимозаменять прямо во время
// исполнения программы.

using System;
using System.Collections.Generic;

namespace DesignPatterns.Strategy.Conceptual
{
    // Контекст определяет интерфейс, представляющий интерес для клиентов.
    class Context
    {
        // Контекст хранит ссылку на один из объектов Стратегии. Контекст не
        // знает конкретного класса стратегии. Он должен работать со всеми
        // стратегиями через интерфейс Стратегии.
        private IStrategy _strategy;

        public Context()
        { }

        // Обычно Контекст принимает стратегию через конструктор, а также
        // предоставляет сеттер для её изменения во время выполнения.
        public Context(IStrategy strategy)
        {
            this._strategy = strategy;
        }

        // Обычно Контекст позволяет заменить объект Стратегии во время
        // выполнения.
        public void SetStrategy(IStrategy strategy)
        {
            this._strategy = strategy;
        }

        // Вместо того, чтобы самостоятельно реализовывать множественные
        // версии алгоритма, Контекст делегирует некоторую работу объекту
        // Стратегии.
        public void DoSomeBusinessLogic()
        {
            Console.WriteLine("Context: Sorting data using the strategy (not sure how it'll do it)");
            var result = this._strategy.DoAlgorithm(new List<string> { "1", "2", "3", "4", "5", "6" });

            string resultStr = string.Empty;
            foreach (var element in result as List<string>)
            {
                resultStr += element + ",";
            }

            Console.WriteLine(resultStr);
        }
    }

    // Интерфейс Стратегии объявляет операции, общие для всех поддерживаемых
    // версий некоторого алгоритма.
    //
    // Контекст использует этот интерфейс для вызова алгоритма, определённого
    // Конкретными Стратегиями.
    public interface IStrategy
    {
        object DoAlgorithm(object data);
    }

    // Конкретные Стратегии реализуют алгоритм, следуя базовому интерфейсу
    // Стратегии. Этот интерфейс делает их взаимозаменяемыми в Контексте.
    class ConcreteStrategyA : IStrategy
    {
        public object DoAlgorithm(object data)
        {
            var list = data as List<string>;
            list.Sort();

            return list;
        }
    }

    class ConcreteStrategyB : IStrategy
    {
        public object DoAlgorithm(object data)
        {
            var list = data as List<string>;
            list.Sort();
            list.Reverse();

            return list;
        }
    }

    class ConcreteStrategyC : IStrategy
    {
        public object DoAlgorithm(object data)
        {
            var list = data as List<string>;
            List<List<string>> pairs = new List<List<string>>();
            pairs.Add(new List<string>());

            for (int i = 0, j = 0; i < list.Count; i++)
            {
                if (i % 2 != 0)
                {
                    pairs[j].Add(list[i]);
                    pairs[j].Reverse();
                    pairs.Add(new List<string>());
                    j++;
                    continue;
                }
                pairs[j].Add(list[i]);
            }

            List<string> result = new List<string>();

            foreach (var pair in pairs)
            {
                foreach (var item in pair)
                {
                    result.Add(item);
                }
            }

            return result;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Клиентский код выбирает конкретную стратегию и передаёт её в
            // контекст. Клиент должен знать о различиях между стратегиями,
            // чтобы сделать правильный выбор.
            var context = new Context();

            Console.WriteLine("Client: Strategy is set to normal sorting.");
            context.SetStrategy(new ConcreteStrategyA());
            context.DoSomeBusinessLogic();

            Console.WriteLine();

            Console.WriteLine("Client: Strategy is set to reverse sorting.");
            context.SetStrategy(new ConcreteStrategyB());
            context.DoSomeBusinessLogic();

            Console.WriteLine();
            context.SetStrategy(new ConcreteStrategyC());
            context.DoSomeBusinessLogic();
        }
    }
}
