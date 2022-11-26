using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Diagnostics.Metrics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Reflection.Metadata.Ecma335;
using System.IO.Enumeration;

namespace Assemble___Restaurant_System
{
    public static class Login_Functions
    {
        static public void Login_Restaurant()
        {
            // Create variable for the input from manager to assign new staff
            string username;
            string password;

            // Create a new list to store the data of new staff 
            LinkedList<Staff_Login> list = Create_Read_Account_Login_List();
            Staff_Login temp_login = new Staff_Login();
            Staff_Login found_item = new Staff_Login();
            bool checking = false;
            //Decrypted_To_Linked_List();
            Console.WriteLine("***** Login *****\n");
            Console.WriteLine("// You can enter Exit to Exit or Return to return //");
            Console.WriteLine("Username: ");
            username = Console.ReadLine();

            // Check if the user want to exit the program
            if (username.CompareTo("Exit") == 0) { return; }
            if (username.CompareTo("Return") == 0) { Menu.Login_Menu(); }

            Console.WriteLine("Password: ");
            password = Console.ReadLine();

            // Check if the user want to exit the program
            if (password.CompareTo("Exit") == 0) { return; }
            if (username.CompareTo("Return") == 0) { Menu.Login_Menu(); }

            // Assign value to Node in Staff_login list
            temp_login.setUserName(username);
            temp_login.setPassword(password);

            // Call the function for checking the password and return bool ( True or False )

            (checking, found_item) = Checking_Password(list, temp_login);

            // Check if the password right or wrong, if {right} --> Interface in app for Staff,
            // else it will force the user to enter again

            if (checking == true && found_item.get_Role().CompareTo("Manager") == 0)
            {
                Menu.Menu_Manager(found_item, list);
            }
            else if (checking == true && found_item.get_Role().CompareTo("Server") == 0 || checking == true && found_item.get_Role().CompareTo("Chef") == 0)
            {
                Menu.Menu_Staff(found_item, list);
            }
            else
            {
                Login_Restaurant();
            }
        }
        static public (bool, Staff_Login) Checking_Password(LinkedList<Staff_Login> staff_Login_List, Staff_Login staff_Login)
        {
            Staff_Login find_item = Find_Item_by_username_Return_Node(staff_Login, staff_Login_List);

            if (find_item == null)
            {
                Console.WriteLine("Cannot find username in the system. Please input again\n");
                return (false, find_item);
            }

            if (staff_Login.getPassword().CompareTo(find_item.getPassword()) == 0)
            {
                Console.WriteLine("Correct password input\nThank you!!");
                return (false, find_item);
            }
            else
            {
                Console.WriteLine("Incorrect password input\nPlease input again!!");
                return (false, find_item);
            }
        }

        static public Staff_Login Find_Item_by_username_Return_Node(Staff_Login staff_Login, LinkedList<Staff_Login> staff_Login_List)
        {
            Staff_Login find_item = new Staff_Login();
            int flag = 0;
            int count = staff_Login_List.Count();
            foreach (Staff_Login item in staff_Login_List)
            {
                if (flag <= count)
                {
                    if (staff_Login.getUserName().CompareTo(item.getUserName()) == 0)
                    {
                        find_item = item;
                        return find_item;
                    }
                    else
                    {
                        flag++;
                    }
                }
                else
                {
                    find_item = null;
                    return find_item;
                }
            }
            return find_item;
        }

        static public LinkedList<Staff_Login> Create_Read_Account_Login_List()
        {
            Staff_Login temp = new Staff_Login();
            Staff_Login temp_login = new Staff_Login();
            LinkedList<Staff_Login> list_Account = new LinkedList<Staff_Login>();
            string filename = "Login.txt";
            var lines = File.ReadLines(filename);
            foreach (string line in lines)
                {
                string raw = Encrypted_Login.Decrypt(line);
                string[] Split_List_Item = raw.Split('-');

                string username = Split_List_Item[0];
                string password = Split_List_Item[1];
                string firstname = Split_List_Item[2];
                string lastname = Split_List_Item[3];
                string role = Split_List_Item[4];
                string salary = Split_List_Item[5];
                string email_recovery = Split_List_Item[6];

                temp.setUserName(username);
                temp.setPassword(password);
                temp.set_First_Name(firstname);
                temp.set_Last_Name(lastname);
                temp.set_Role(role);
                temp.set_Salary(Single.Parse(salary));
                temp.setRecovery(email_recovery);

                list_Account.AddFirst(temp);
            }
            Console.WriteLine(temp.getRecovery());
            return list_Account;
        }

        static public void Create_Write_New_Staff_List(LinkedList<Staff_Login> list_Account, Staff_Login temp_Session_Information, LinkedList<Staff_Login> current_version)
        {
            Staff_Login temp = new Staff_Login();
            string username;
            string password;
            string First_Name;
            string Last_Name;
            float Salary = 0.0f;
            string role = "unknown";
            string recovery_email;

            bool isNumerical_1 = false;
            int number_of_Staffs = 0;
            while (isNumerical_1 == false)
            {
                Console.WriteLine("Enter the number of Staffs in the company you want to assign\n");
                string input_numberstaffs = Console.ReadLine();
                isNumerical_1 = int.TryParse(input_numberstaffs, out number_of_Staffs);
                if(number_of_Staffs < 0)
                {
                    isNumerical_1 = false;
                }
                if(isNumerical_1 == false)
                {
                    Console.WriteLine("Wrong input!!!. Please enter Integer\n");
                }
            }

            for (int i = 0; i < number_of_Staffs; i++)
            {
                Console.WriteLine("***** Set Up New Staff *****\n");
                Console.WriteLine("// You can enter Exit to Exit or Return to return //");

                Console.WriteLine("Username: ");
                username = Console.ReadLine();

                // Check if the user want to exit the program
                if (username.CompareTo("Exit") == 0) { System.Environment.Exit(0); return; }
                if (username.CompareTo("Return") == 0) { Menu.Menu_Manager(temp_Session_Information, current_version); return; }

                Console.WriteLine("Password: ");
                password = Console.ReadLine();

                // Check if the user want to exit the program
                if (password.CompareTo("Exit") == 0) { System.Environment.Exit(0); return; }
                if (password.CompareTo("Return") == 0) { Menu.Menu_Manager(temp_Session_Information, current_version); return; }

                Console.WriteLine("First Name: ");
                First_Name = Console.ReadLine();
                if (First_Name.CompareTo("Exit") == 0) { System.Environment.Exit(0); return; }
                if (First_Name.CompareTo("Return") == 0) { Menu.Menu_Manager(temp_Session_Information, current_version); return; }

                Console.WriteLine("Last Name: ");
                Last_Name = Console.ReadLine();
                if (Last_Name.CompareTo("Exit") == 0) { System.Environment.Exit(0); return; }
                if (Last_Name.CompareTo("Return") == 0) { Menu.Menu_Manager(temp_Session_Information, current_version); return; }

                Console.WriteLine("Email: ");
                recovery_email = Console.ReadLine();
                if (recovery_email.CompareTo("Exit") == 0) { System.Environment.Exit(0); return; }
                if (First_Name.CompareTo("Return") == 0) { Menu.Menu_Manager(temp_Session_Information, current_version); return; }

                

                bool isNumerical = false;
                int choice = -1;
                

                while (isNumerical == false)
                {
                    Console.WriteLine("** Role **\n");
                    Console.WriteLine("1. Manager\n");
                    Console.WriteLine("2. Server\n");
                    Console.WriteLine("3. Chef\n");
                    Console.WriteLine("4. Return\n");
                    string? input = Console.ReadLine();
                    isNumerical = int.TryParse(input, out choice);
                    if (choice < 5 && choice > 0)
                    {
                        switch (choice)
                        {
                            case 1:
                                role = "Manager";
                                isNumerical = true;
                                break;
                            case 2:
                                role = "Server";
                                isNumerical = true;
                                break;
                            case 3:
                                role = "Chef";
                                isNumerical = true;
                                break;
                            case 4:
                                isNumerical = true;
                                Menu.Menu_Manager(temp_Session_Information, current_version);
                                break;
                            default:
                                isNumerical = false;
                                break;
                        }
                    }
                    if (isNumerical == false) { Console.WriteLine("Wrong Input. Please enter integer\n"); }
                }

                Console.WriteLine("Salary: ");
                Salary = Single.Parse(Console.ReadLine());
                temp.setUserName(username);
                temp.setPassword(password);
                temp.setRecovery(recovery_email);
                temp.set_First_Name(First_Name);
                temp.set_Last_Name(Last_Name);
                temp.set_Role(role);
                temp.set_Salary(Salary);
                list_Account.AddFirst(temp);
            }
            /* @ Call the function to encypted and write to the file the information
             * @ in string format */
            bool flag = Write_To_File(list_Account);
            current_version = list_Account;
            /* @Checking the flag and throw the message */
            if (flag == true) { Console.WriteLine("Write to file successfully\n"); Menu.Menu_Manager(temp_Session_Information, current_version); return; }
            else { Console.WriteLine("Write the list of the staffs false\n"); Menu.Menu_Manager(temp_Session_Information, current_version); return; }
        }

        static public bool Write_To_File(LinkedList<Staff_Login> list_account)
        {
            /*@ Assign the file */
            string filename = "Login.txt";
            File.Delete(filename);
            /*@ Check the number of items inside the list*/
            if (list_account.Count() == 0)
            {
                Console.WriteLine("The list is empty cannot write to file\n");
                return false;
            }

            /*@ Loop through each item in the list */
            foreach (Staff_Login staff in list_account)
            {
                string Write_to_File_Format = staff.getUserName() + "-" + staff.getPassword() + "-" + staff.get_First_Name() + "-" + staff.get_Last_Name() + "-" + staff.get_Role() + "-" + staff.get_Salary().ToString() + "-"
                   + staff.getRecovery();
                string str_encrypted = Encrypted_Login.Encrypt(Write_to_File_Format);
                    File.WriteAllText(filename, str_encrypted);
            }
            return true;

        }
    }
}