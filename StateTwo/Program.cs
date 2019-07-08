// Паттерн Состояние
//
// Назначение: Позволяет объектам менять поведение в зависимости от своего
// состояния. Извне создаётся впечатление, что изменился класс объекта.

using System;

namespace DesignPatterns.State.Conceptual
{
    // Контекст определяет интерфейс, представляющий интерес для клиентов.
    // Он также хранит ссылку на экземпляр подкласса Состояния, который
    // отображает текущее состояние Контекста.
    class Hero
    {
        // Ссылка на текущее состояние Контекста.
        private HeroState _state = null;
        protected int shootCounter = 0;

        public Hero()
        {
            this.TransitionTo(new CommonState());
        }

        // Контекст позволяет изменять объект Состояния во время выполнения.
        public void TransitionTo(HeroState state)
        {
            //Console.WriteLine($"Context: Transition to {state.GetType().Name}.");
            this._state = state;
            this._state.SetContext(this);
        }

        // Контекст делегирует часть своего поведения текущему объекту
        // Состояния.
        public void Shoot()
        {
            shootCounter++;
            Console.WriteLine("{1} Hero do damage: {0}", this._state.DoDamage(shootCounter), shootCounter);

        }
    }

    // Базовый класс Состояния объявляет методы, которые должны реализовать
    // все Конкретные Состояния, а также предоставляет обратную ссылку на объект
    // Контекст, связанный с Состоянием. Эта обратная ссылка может
    // использоваться Состояниями для передачи Контекста другому Состоянию.
    abstract class HeroState
    {
        protected Hero _hero;

        public void SetContext(Hero hero)
        {
            this._hero = hero;
        }

        public abstract int DoDamage(int shootCounter);
    }

    // Конкретные Состояния реализуют различные модели поведения, связанные
    // с состоянием Контекста.
    class CommonState : HeroState
    {
        public override int DoDamage(int shootCounter)
        {
            if (shootCounter % 8 == 0 || shootCounter == 3)
                this._hero.TransitionTo(new SuperState());

            return 5;
        }
    }

    class SuperState : HeroState
    {
        public override int DoDamage(int shootCounter)
        {
            this._hero.TransitionTo(new HyperState());
            return 10;
        }
    }

    class HyperState : HeroState
    {
        public override int DoDamage(int shootCounter)
        {
            if (shootCounter % 5 == 0)
                this._hero.TransitionTo(new CommonState());

            else
                this._hero.TransitionTo(new HyperState());

            return 15;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Клиентский код.
            var context = new Hero();

            for (int idx = 1; idx < 20; idx++)
            {
                context.Shoot();
            }
            Console.Read();
        }
    }
}
