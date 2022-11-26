using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemble___Restaurant_System
{
    public static class Inventory_Stub_Class
    {
        public class Inventory_Food
        {
            private string _Name;
            private int Portions;
            public void setNameFood(string name) { _Name = name; }
            public string getNameFood() { return _Name; }
            public void setPortionFood(int portions) { Portions = portions; }
            public int getPortionFood() { return Portions; }
        }
        public class Items
        {
            // Stub for Items
        }
    }
}
