using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentlikeConsoleOplossing
{
    public class StudentClassToMake
    {
        public bool IsPizza { get; private set; }

        private string topje;

        public string Toppings
        {
            get { return topje; }
            set { topje = value; }
        }

        private double price;

        public double Price
        {
            get { return price; }
            set {
            if(value>0)    
                price = value; 
            
            }
        }

    }
}
