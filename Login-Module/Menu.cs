using System;
using System.Collections.Generic;

namespace Assemble___Restaurant_System
{
    public static class Menu
    {
        static public void Login_Menu()
        {
            bool isNumerical = false;
            int choice = -1;
            while (isNumerical == false)
            {
                Console.WriteLine("Welcome to The restaurant\n");
                Console.WriteLine("Please Choose The number of the Option Below\n");
                Console.WriteLine("1. Login to Restaurant\n"); ;
                Console.WriteLine("2. forgot password\n");
                Console.WriteLine("3. Exit\n");

                string input = Console.ReadLine();
                isNumerical = int.TryParse(input, out choice);
                if (choice < 4 && choice > 0)
                {
                    switch (choice)
                    {
                        case 1:
                            // Call the function to checking for the password
                            Login_Functions.Login_Restaurant();
                            Console.WriteLine("Finish\n");
                            //Checking_Password(temp_login, list);
                            break;
                        case 2:
                            // Call the function to help people with the password
                            // This function will take the input of user 
                            // When have time
                            Forgot_Password.Change_Forgot_Password();
                            break;
                        case 3:
                            Console.WriteLine("Thank you for using program\n");
                            System.Environment.Exit(0);
                            break;
                        default:
                            Login_Menu();
                            break;
                    }
                }
                else { Console.WriteLine("Wrong Input. Please enter integer\n"); isNumerical = false; }
            }
        }

        public static void Menu_Manager(Staff_Login temp_Session_Information, LinkedList<Staff_Login> List_of_Staff)
        {
            bool isNumerical = false;
            int choice = -1;

            while (isNumerical == false)
            {
                 // Print out the Options for the Staff
                Console.WriteLine("Welcome to Restaurant\n");
                Console.WriteLine("1. Check the Menu\n");
                Console.WriteLine("2. Check the inventory\n");
                Console.WriteLine("3. Check the security\n");
                Console.WriteLine("4. Check the salary\n");
                Console.WriteLine("5. Check the time table\n");
                Console.WriteLine("6. Change the Password\n");
                Console.WriteLine("7. Assign new Staff\n");
                Console.WriteLine("8. Change the information\n");
                Console.WriteLine("9. Logout\n");
                Console.WriteLine("10. Exit\n");
                Console.WriteLine("Please enter the number of option you choose\n");
                string input = Console.ReadLine();
                isNumerical = int.TryParse(input, out choice);
                if (choice < 11 && choice > 0)
                {
                    switch (choice)
                    {
                        case 1:
                            isNumerical = true;
                            // Menu
                            break;
                        case 2:
                            isNumerical = true;
                            Inventory_Menu(temp_Session_Information, List_of_Staff);
                            break;
                        case 3:
                            isNumerical = true;
                            // Security
                            break;
                        case 4:
                            isNumerical = true;
                            // Salary
                            break;
                        case 5:
                            isNumerical = true;
                            // Timetable
                            break;
                        case 6:
                            isNumerical = true;
                            //Change_Password(temp_login, list);
                            break;
                        case 7:
                            isNumerical = true;
                            LinkedList<Staff_Login> list_Account = new LinkedList<Staff_Login>();
                            Login_Functions.Create_Write_New_Staff_List(list_Account, temp_Session_Information, List_of_Staff);
                            break;
                        case 8:
                            isNumerical = true;
                            break;
                        case 9:
                            isNumerical = true;
                            Login_Menu();
                            break;
                        case 10:
                            System.Environment.Exit(0);
                            break;
                        default:
                            isNumerical = false;
                            break;
                    }
                }
                else { isNumerical = false; Console.WriteLine("Wrong Input. Please enter integer\n"); }
            }    
        }
        public static void Menu_Staff(Staff_Login temp_Session_Information, LinkedList<Staff_Login> List_of_Staff)
        {

            // Print out the Options for the Staff
            Console.WriteLine("Welcome to Restaurant\n");
            Console.WriteLine("1. Check the Menu\n");
            Console.WriteLine("2. Check the inventory\n");
            Console.WriteLine("3. Check the security\n");
            Console.WriteLine("4. Check the salary\n");
            Console.WriteLine("5. Check the time table\n");
            Console.WriteLine("6. Change the Password\n");
            Console.WriteLine("7. Logout\n");
            Console.WriteLine("8. Exit\n");

            int choice;

            Console.WriteLine("Please enter the number of option you choose\n");
            choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    // Menu
                    break;
                case 2:
                    //Inventory.Inventory_Manager();
                    break;
                case 3:
                    // Security
                    break;
                case 4:
                    // Salary
                    break;
                case 5:
                    // Timetable
                    break;
                case 6:
                    //Change_Password(temp_login, list);
                    break;
                case 7:
                    break;
                default:
                    break;
            }
        }
        static public void Inventory_Menu(Staff_Login temp_Session_Information, LinkedList<Staff_Login> List_of_Staff)
        {
            int choice;
            bool isNumerical = false;
            while (isNumerical == false)
            {
                Console.WriteLine("Checking Inventory\n");
                Console.WriteLine("1. Check the List of the Food\n");
                Console.WriteLine("2. Check the Specific Food\n");
                Console.WriteLine("3. Modify the Portion in the List\n");
                Console.WriteLine("4. Logout\n");
                Console.WriteLine("5. Exit\n");
                Console.WriteLine("6. Return\n");
                Console.WriteLine("Please enter the number of option you choose\n");

                string input = Console.ReadLine();
                isNumerical = int.TryParse(input, out choice);
                if (choice < 4 && choice > 0)
                {

                    switch (choice)
                    {
                        case 1:
                            isNumerical = true;
                            Inventory_Functions.Checking_Inventory();
                            break;
                        case 2:
                            isNumerical = true;
                            bool check = Inventory_Functions.Checking_Specific_Item_In_Inventory();
                            break;
                        case 3:
                            isNumerical = true;
                            Inventory_Functions.Modify_The_Inventory();
                            break;
                        case 4:
                            isNumerical = true;
                            Login_Menu();
                            break;
                        case 5:
                            System.Environment.Exit(0);
                            break;
                        case 6:
                            Menu_Manager(temp_Session_Information, List_of_Staff);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}

