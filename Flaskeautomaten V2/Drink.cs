using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flaskeautomaten_V2
{
    class Drink
    {
        string name;
        int serialNumber;

        public Drink(string name, int serialNumber)
        {
            this.name = name;
            this.serialNumber = serialNumber;
        }

        public string Name
        {
            get => name;
        }

        public int SerialNumber
        {
            get => serialNumber;
        }
    }
}
