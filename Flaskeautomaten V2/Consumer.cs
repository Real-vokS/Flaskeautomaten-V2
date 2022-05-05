using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Flaskeautomaten_V2
{
    class Consumer
    {
        public void DrinkBeer()
        {
            while (true)
            {
                lock (Splitter.BeerDrinks)
                {
                    //If the queue beerDrinks in the class Splitter is equal to 0
                    //Then wake all threads up that are sleeping using beerDrinks
                    //And put this thread to sleep
                    if (Splitter.BeerDrinks.Count == 0)
                    {
                        Monitor.PulseAll(Splitter.BeerDrinks);
                        Monitor.Wait(Splitter.BeerDrinks);
                    }

                    //Tries to dequeue the first object in beerDrinks and outputs the Drink object as drink
                    //Writes to console that the Beer Consumer is drinking said drink
                    if(Splitter.BeerDrinks.TryDequeue(out Drink drink))
                    {
                        Console.WriteLine(Producer.Drinks.Count + " " + Splitter.BeerDrinks.Count);
                        Console.WriteLine("{0} is Drinking " + drink.Name + drink.SerialNumber, Thread.CurrentThread.Name);
                    }

                }
                Thread.Sleep(500);
            }
        }

        public void DrinkSoda()
        {
            while (true)
            {
                lock (Splitter.SodaDrinks)
                {
                    //If the queue sodaDrinks in the class Splitter is equal to 0
                    //Then wake all threads up that are sleeping using sodaDrinks
                    //And put this thread to sleep
                    if (Splitter.SodaDrinks.Count == 0)
                    {
                        Monitor.PulseAll(Splitter.SodaDrinks);
                        Monitor.Wait(Splitter.SodaDrinks);
                    }

                    //Tries to dequeue the first object in sodaDrinks and outputs the Drink object as drink
                    //Writes to console that the Soda Consumer is drinking said drink
                    if (Splitter.SodaDrinks.TryDequeue(out Drink drink))
                    {
                        Console.WriteLine(Producer.Drinks.Count + " " + Splitter.SodaDrinks.Count);
                        Console.WriteLine("{0} is Drinking " + drink.Name + drink.SerialNumber, Thread.CurrentThread.Name);
                    }

                }
                Thread.Sleep(500);
            }
        }
    }
}
