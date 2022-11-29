using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Management_System
{
    internal class Systems
 
    {

        public abstract class System
        {

            public bool functional;


            public abstract void ViewSystem();


            public abstract void EditSystem(string input);


            public abstract void InitializeSystem();

        }

        public class Alarm : System
        {
            public bool active;

            public override void ViewSystem()
            {
                if (active)
                {
                    Console.WriteLine("Alarm: Active");
                }
                else
                {
                    Console.WriteLine("Alarm: Inactive");
                }
            }

            public override void EditSystem(string input)
            {
                if (input == "true")
                    this.active = true;
                else if (input == "false")
                    this.active = false;
                else
                    Console.WriteLine("Error: Invalid edit input");
            }

            public override void InitializeSystem()
            {
                this.active = false;
            }


        }

        public class DoorLocks : System
        {
            public bool locked;

            public override void ViewSystem()
            {

                {
                    if (locked)
                    {
                        Console.WriteLine("Doors: Unlocked");

                    }
                    else
                    {
                        Console.WriteLine("Doors: Locked");
                    }

                }
            }

            public override void EditSystem(string input)
            {
                if (input == "true")
                    this.locked = true;
                else if (input == "false")
                    this.locked = false;
                else
                    Console.WriteLine("Error: invalid edit input");
            }

            public override void InitializeSystem()
            {
                this.locked = false;
            }
        }

        public class Temperature : System
        {
            public int temperature;
            public override void ViewSystem()
            {
                Console.WriteLine("The temperature is %d", temperature);
            }

            public override void EditSystem(string input)
            {
                try
                {
                    int temp = Int32.Parse(input);
                    this.temperature = temp;
                }
                catch
                {
                    Console.WriteLine("Unable to parse");
                }
            }

            public override void InitializeSystem()
            {
                temperature = 20;
            }

        }

    }
}