using System;
using System.Collections.Generic;
using System.Threading;

namespace Observer.Conceptual
{
    public interface ISubscriber
    {
        // Получает обновление от издателя
        void Update(IPublisher subject);
    }

    public interface IPublisher
    {
        // Присоединяет наблюдателя к издателю.
        void AttachToNewspaper(ISubscriber observer);
        void AttachToMagazine(ISubscriber observer);

        // Отсоединяет наблюдателя от издателя.
        void DetachFromNewspaper(ISubscriber observer);
        void DetachFromMagazine(ISubscriber observer);

        // Уведомляет всех наблюдателей о событии.
        void NotifyAboutNewspaper();
        void NotifyAboutMagazine();
    }

    // Издатель владеет некоторым важным состоянием и оповещает наблюдателей о
    // его изменениях.
    public class Publisher : IPublisher
    {
        // Для удобства в этой переменной хранится состояние Издателя,
        // необходимое всем подписчикам.
        public int State { get; set; } = -0;

        // Список подписчиков. В реальной жизни список подписчиков может
        // храниться в более подробном виде (классифицируется по типу события и
        // т.д.)
        private List<ISubscriber> _subscribersToNewspaper = new List<ISubscriber>();
        private List<ISubscriber> _subscribersToMagazine = new List<ISubscriber>();

        // Методы управления подпиской.
        public void AttachToNewspaper(ISubscriber observer)
        {
            Console.WriteLine("Клиент подписался к рассылке газет");
            _subscribersToNewspaper.Add(observer);
        }

        public void AttachToMagazine(ISubscriber observer)
        {
            Console.WriteLine("Клиент подписался к рассылке журналов");
            _subscribersToMagazine.Add(observer);
        }

        public void DetachFromNewspaper(ISubscriber observer)
        {
            _subscribersToNewspaper.Remove(observer);
            Console.WriteLine("Клиент отподписался от рассылки газет");
        }

        public void DetachFromMagazine(ISubscriber observer)
        {
            _subscribersToMagazine.Remove(observer);
            Console.WriteLine("Клиент отподписался от рассылки журналов");
        }

        // Запуск обновления в каждом подписчике.
        public void NotifyAboutNewspaper()
        {
            Console.WriteLine("Рассылка газет начата...");

            foreach (var observer in _subscribersToNewspaper)
            {
                observer.Update(this);
            }
        }

        public void NotifyAboutMagazine()
        {
            Console.WriteLine("Рассылка журналов начата...");

            foreach (var observer in _subscribersToMagazine)
            {
                observer.Update(this);
            }
        }

        // Обычно логика подписки – только часть того, что делает Издатель.
        // Издатели часто содержат некоторую важную бизнес-логику, которая
        // запускает метод уведомления всякий раз, когда должно произойти что-то
        // важное (или после этого).
        public void SendMessage()
        {
            Console.WriteLine("\nПоступил новый товар.");
            State = new Random().Next(0, 10);

            if (State <= 5)
            {
                Console.WriteLine("Пришла газета");
                Thread.Sleep(15);

                NotifyAboutNewspaper();
            }
            else
            {
                Console.WriteLine("Пришли журналы");
                Thread.Sleep(15);

                NotifyAboutMagazine();
            }


        }
    }

    // Конкретные Наблюдатели реагируют на обновления, выпущенные Издателем, к
    // которому они прикреплены.
    class SubscriberA : ISubscriber
    {
        public void Update(IPublisher publisher)
        {
            if ((publisher as Publisher).State <= 5)
            {
                Console.WriteLine("SubscriberA: получил газету");
            }
            else
            {
                Console.WriteLine("SubscriberA: получил журнал");
            }
        }
    }

    class SubscriberB : ISubscriber
    {
        public void Update(IPublisher publisher)
        {
            if ((publisher as Publisher).State <= 5)
            {
                Console.WriteLine("SubscriberB: получил газету.");
            }
            else
            {
                Console.WriteLine("SubscriberB: получил журнал");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Клиентский код.
            var subject = new Publisher();
            var observerA = new SubscriberA();
            subject.AttachToNewspaper(observerA);


            var observerB = new SubscriberB();
            subject.AttachToNewspaper(observerB);
            subject.AttachToMagazine(observerB);

            subject.SendMessage();
            subject.SendMessage();

            subject.DetachFromNewspaper(observerB);

            subject.SendMessage();

            Console.ReadLine();
        }
    }
}