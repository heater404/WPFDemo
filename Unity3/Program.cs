using System;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Unity.Resolution;

namespace Unity3
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            SimpleIOC ioc = new SimpleIOC();

            ioc.RegisterType<ICar, Audi>();
            ioc.RegisterType<ICarKey, AudiKey>();

            Driver driver = ioc.Resolve<Driver>();
            driver.RunCar();


            Console.ReadKey();
        }
    }

    public class Driver
    {
        private ICar _car = null;
        private ICarKey _key = null;
        private Guid guid = Guid.NewGuid();
        public Driver(ICar car, ICarKey key)
        {
            _car = car;
            _key = key;
        }

        public Driver(ICar car)
        {
            _car = car;
        }

        public void RunCar()
        {
            Console.WriteLine($"{guid.ToString()} Running {_car?.GetType().Name} with {_key?.GetType().Name} - {_car?.Run()} mile ");
        }
    }
}