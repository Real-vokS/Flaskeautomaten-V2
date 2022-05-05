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
            Consumer c = new();

            Thread producer = new Thread(p.ProduceDrink);
            producer.Start();
            Thread splitter = new Thread(s.Split);
            splitter.Start();

            Thread beerConsumer = new Thread(c.DrinkBeer);
            beerConsumer.Name = "[Beer Consumer]";
            beerConsumer.Start();

            Thread sodaConsumer = new Thread(c.DrinkSoda);
            sodaConsumer.Name = "[Soda Consumer]";
            sodaConsumer.Start();

            Thread.Sleep(1000);

            producer.Join();
            splitter.Join();
            Thread.Sleep(200);
            beerConsumer.Join();
            sodaConsumer.Join();
            
        }


        

        

        


       
    }
}
