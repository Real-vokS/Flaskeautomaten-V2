﻿using System;
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
                    Console.WriteLine(Producer.Drinks.Count);

                    if (Splitter.BeerDrinks.Count == 18)
                    {
                        Monitor.PulseAll(Splitter.BeerDrinks);
                        Console.WriteLine(Thread.CurrentThread.Name);
                        Monitor.Wait(Splitter.BeerDrinks);
                    }

                    if(Splitter.BeerDrinks.TryDequeue(out Drink drink))
                    {
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
                Console.WriteLine(Producer.Drinks.Count);
                lock (Splitter.SodaDrinks)
                {
                    if (Splitter.SodaDrinks.Count == 0)
                    {
                        Monitor.PulseAll(Splitter.SodaDrinks);
                        Monitor.Wait(Splitter.SodaDrinks);
                    }

                    if(Splitter.SodaDrinks.TryDequeue(out Drink drink))
                    {
                        Console.WriteLine("{0} is Drinking " + drink.Name + drink.SerialNumber, Thread.CurrentThread.Name);
                    }

                }
                Thread.Sleep(500);
            }
        }
    }
}