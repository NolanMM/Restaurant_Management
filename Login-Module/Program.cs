using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Login_Module
{
    public class Program
    {
        static void Main(string[] args)
        {
            menu();
        }
        static public void menu()
        {
            Console.WriteLine("Please enter the identity you want to access\n");
            Console.WriteLine("1. Guest\n");
            Console.WriteLine("2. Staff\n");
            Console.WriteLine("2. Exit\n");
            // Create variable to take the input from user
            
            Console.WriteLine("Please enter the number you want to choose: ");

            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 0:
                    // The function to link with all the option for the guests ... 
                    break;
                case 1:
                    Features.Login();
                    break;
            }
        }
    }
}
