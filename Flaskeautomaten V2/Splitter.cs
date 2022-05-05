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
                    //If the queue in producer drinks.count is equal to 0
                    //Then it wakes up all threads that was locked with producers drinks queue
                    //And puts this thread to sleep
                    if (Producer.Drinks.Count == 0)
                    {
                        Monitor.PulseAll(Producer.Drinks);
                        Monitor.Wait(Producer.Drinks);
                    }

                    //Tries to take the first object in Producer.Drinks queue
                    //Then check that objects Name and if it contain Beer or Soda
                    //If it does then it enqueue it to the queue suitable for the obejct
                    if (Producer.Drinks.TryDequeue(out Drink drink))
                    {
                        if (drink.Name.Contains("Beer"))
                        {
                            Console.WriteLine("moved " + drink.Name + drink.SerialNumber + " into beerDrinks");
                            beerDrinks.Enqueue(drink);
                        }
                        else if(drink.Name.Contains("Soda"))
                        {
                            Console.WriteLine("moved " + drink.Name + drink.SerialNumber + " into sodaDrinks");
                            sodaDrinks.Enqueue(drink);
                        }
                    }
                }

                //If beerDrinks.Count is equal to 24
                //Then wake all threads up that are sleeping using beerDrinks
                //And put itself to sleep
                lock (beerDrinks)
                {
                    if (beerDrinks.Count == 24)
                    {
                        Monitor.PulseAll(beerDrinks);
                        Monitor.Wait(beerDrinks);
                    }
                }

                //If sodaDrinks.Count is equal to 24
                //Then wake all threads up that are sleeping using sodaDrinks
                //And put itself to sleep
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
