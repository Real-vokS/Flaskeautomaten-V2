using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Flaskeautomaten_V2
{
    class Splitter
    {
        static Queue<Drink> sodaDrinks = new Queue<Drink>();
        static Queue<Drink> beerDrinks = new Queue<Drink>();

        public static Queue<Drink> SodaDrinks
        {
            get => sodaDrinks;
            set => sodaDrinks = value;
        }

        public static Queue<Drink> BeerDrinks
        {
            get => sodaDrinks;
            set => sodaDrinks = value;
        }

        public void Split()
        {

            while (true)
            {
                lock (Producer.Drinks)
                {
                    if (Producer.Drinks.Count == 0)
                    {
                        Monitor.PulseAll(Producer.Drinks);
                        Monitor.Wait(Producer.Drinks);
                    }

                    if (Producer.Drinks.TryDequeue(out Drink drink))
                    {
                        if (drink.Name.Contains("Beer"))
                        {
                            Console.WriteLine("moved " + drink.Name + drink.SerialNumber + " into beerDrinks");
                            beerDrinks.Enqueue(drink);
                        }
                        else
                        {
                            Console.WriteLine("moved " + drink.Name + drink.SerialNumber + " into sodaDrinks");
                            sodaDrinks.Enqueue(drink);
                        }
                    }
                }

                lock (beerDrinks)
                {
                    if (beerDrinks.Count == 24)
                    {
                        Monitor.PulseAll(beerDrinks);
                        Monitor.Wait(beerDrinks);
                    }
                }

                lock (sodaDrinks)
                {
                    if (sodaDrinks.Count == 24)
                    {
                        Monitor.PulseAll(sodaDrinks);
                        Monitor.Wait(sodaDrinks);
                    }
                }

                Thread.Sleep(100);

            }
        }
    }
}
