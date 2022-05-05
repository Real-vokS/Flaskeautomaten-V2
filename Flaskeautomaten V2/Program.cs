using System;
using System.Threading;

namespace Flaskeautomaten_V2
{
    class Program
    {


        static void Main(string[] args)
        {

            Producer p = new();
            Splitter s = new();
            Consumer c1 = new();
            Consumer c2 = new();

            //Creates Threads
            Thread producer = new Thread(p.ProduceDrink);
            Thread splitter = new Thread(s.Split);
            Thread beerConsumer = new Thread(c1.DrinkBeer);
            Thread sodaConsumer = new Thread(c2.DrinkSoda);

            beerConsumer.Name = "[Beer Consumer]";
            sodaConsumer.Name = "[Soda Consumer]";


            //start threads
            producer.Start();
            splitter.Start();
            Thread.Sleep(20);
            beerConsumer.Start();
            sodaConsumer.Start();

        }
    }
}
