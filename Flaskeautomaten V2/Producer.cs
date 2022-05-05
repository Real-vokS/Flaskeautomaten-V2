using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Flaskeautomaten_V2
{
    class Producer
    {
        static Queue<Drink> drinks = new Queue<Drink>();


        static int beerCount = 1;
        static int sodaCount = 1;

        Random rnd = new Random();

        public static Queue<Drink> Drinks
        {
            get => drinks;
            set => drinks = value;
        }


        public void ProduceDrink()
        {

            while (true)
            {
                //lock (drinks)
                Monitor.Enter(drinks);
                if (drinks.Count == 10)
                {
                    Console.WriteLine("tesgggggggggggggggggggggggggggggggt");
                    Monitor.Wait(drinks);
                }

                int bottle = rnd.Next(1, 3);

                if (bottle == 1)
                {
                    Drink drink = new Drink("Beer", beerCount);
                    drinks.Enqueue(drink);
                    Console.WriteLine("Added Beer " + beerCount);
                    beerCount++;

                }
                else
                {
                    Drink drink = new Drink("Soda", sodaCount);
                    drinks.Enqueue(drink);
                    Console.WriteLine("Added Soda " + sodaCount);
                    sodaCount++;
                }
                Monitor.PulseAll(drinks);
                Monitor.Exit(drinks);
                Thread.Sleep(100 / 15);
            }
        }
    }
}

