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

                //Waking all Threads up that sleeps using that was locked with drinks, when drinks.Count hits 24
                //Then putting this thread to sleep
                if (drinks.Count == 24)
                {
                    Monitor.PulseAll(drinks);
                    Monitor.Wait(drinks);
                }

                //Creates a int called bottle and gives it a random number between 1 and 2
                int bottle = rnd.Next(1, 3);

                //If bottle is 1, then create a new Drink object give it Beer as name and beerCount as SerialNumber
                //Then enqueue's the object to drinks queue
                //Adds +1 to beerCount
                if (bottle == 1)
                {
                    Drink drink = new Drink("Beer", beerCount);
                    drinks.Enqueue(drink);
                    Console.WriteLine("Added Beer " + beerCount);
                    beerCount++;
                }
                //If bottle is 2, then create a new Drink object give it Soda as name and sodaCount as SerialNumber
                //Then enqueue's the object to drinks queue
                //Adds +1 to sodaCount
                else
                {
                    Drink drink = new Drink("Soda", sodaCount);
                    drinks.Enqueue(drink);
                    Console.WriteLine("Added Soda " + sodaCount);
                    sodaCount++;
                }
                Monitor.Exit(drinks);
                Thread.Sleep(100);
            }
        }
    }
}

