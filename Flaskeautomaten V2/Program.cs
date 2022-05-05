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
            Thread splitter = new Thread(s.Split);
            Thread beerConsumer = new Thread(c.DrinkBeer);
            Thread sodaConsumer = new Thread(c.DrinkSoda);

            beerConsumer.Name = "[Beer Consumer]";
            sodaConsumer.Name = "[Soda Consumer]";

            producer.Start();
            splitter.Start();
            beerConsumer.Start();
            sodaConsumer.Start();

            
        }


        

        

        


       
    }
}
