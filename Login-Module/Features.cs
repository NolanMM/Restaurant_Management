using Login_Module;
using System;
using System.Diagnostics.Metrics;
using System.Security.Cryptography;
using System.Text;

namespace Login_Module
{
    public class Features
    {
        static public void Login()
        {
            Console.WriteLine("Welcome to The restaurant\n");
            Console.WriteLine("Please Choose The number of the Option Below\n");
            Console.WriteLine("1. Login to Restaurant\n"); ;
            Console.WriteLine("2. forgot password\n");
            Console.WriteLine("3. Return\n");
            Console.WriteLine("4. Exit\n");

            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    // Call the function to checking for the password
                    Login_Restaurant();
                    Console.WriteLine("Finish\n");
                    //Checking_Password(temp_login, list);
                    break;
                case 2:
                    // Call the function to help people with the password
                    // This function will take the input of user 
                    // When have time
                    Forgot_Password();
                    
                    break;
                case 3:
                    Program.menu();
                    break;
                case 4:
                    Console.WriteLine("Thank you for using program\n");
                    break;
                default:
                    Login();
                    break;
            }
        }
        static public void Login_Restaurant()
        {
            // Create variable for the input from manager to assign new staff
            string username;
            string password;

            // Create a new list to store the data of new staff 
            LinkedList<Staff_Login> list = Create_Read_Account_List();
            Staff_Login temp_login = new Staff_Login();

            //Decrypted_To_Linked_List();
            Console.WriteLine("***** Login *****\n");
            Console.WriteLine("// You can enter Exit to Exit or Return to return //");
            Console.WriteLine("Username: ");
            username = Console.ReadLine();

            // Check if the user want to exit the program
            if (username.CompareTo("Exit") == 0) { return; }

            Console.WriteLine("Password: ");
            password = Console.ReadLine();

            // Check if the user want to exit the program
            if (password.CompareTo("Exit") == 0) { return; }

            // Assign value to Node in Staff_login list
            temp_login.setUserName(username);
            temp_login.setPassword(password);

            // Call the function for checking the password and return bool ( True or False )
            bool checking = Checking_Password(list, temp_login);

            // Check if the password right or wrong, if {right} --> Interface in app for Staff,
            // else it will force the user to enter again

            if (checking == true)
            {
                menu_Login_Staff(temp_login);
            }
            else
            {
                Login_Restaurant();
            }
        }
        static public void Forgot_Password()
        {

        }
        static public void menu_Login_Staff(Staff_Login temp_login)
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
                    // Inventory
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
                    Change_Password(temp_login);
                    break;
                case 7:
                    // Logout
                    break;
                case 8:
                    break;
                default:
                    break;
            }
        }
        static public LinkedList<Staff_Login> Create_Read_Account_List()
        {
            Staff_Login temp = new Staff_Login();
            Staff_Login temp_login = new Staff_Login();
            LinkedList<Staff_Login> list_Account = new LinkedList<Staff_Login>();
            var lines = File.ReadLines("Login.txt");
            foreach (var line in lines)
            {
                string[] Split_List_Item = line.Split('-');

                // Convert string to the byte[] to decrypted it
                byte[] byteArray_Username = Encoding.ASCII.GetBytes(Split_List_Item[0]);
                byte[] byteArray_Password = Encoding.ASCII.GetBytes(Split_List_Item[1]);
                byte[] byteArray_Key_Decrypted = Encoding.ASCII.GetBytes(Split_List_Item[2]);
                byte[] byteArray_IV_Decrypted = Encoding.ASCII.GetBytes(Split_List_Item[3]);

                string username = Enccypted_Login.Decrypt(byteArray_Username, byteArray_Key_Decrypted, byteArray_IV_Decrypted);
                string password = Enccypted_Login.Decrypt(byteArray_Password, byteArray_Key_Decrypted, byteArray_IV_Decrypted);

                temp.setUserName(username);
                temp.setPassword(password);
                list_Account.AddFirst(temp);
            }
            return list_Account;
        }
        static public bool Checking_Password(LinkedList<Staff_Login> staff_Login_List, Staff_Login staff_Login)
        {
            
            if (staff_Login_List.Contains(staff_Login) == true)
            {
                
                LinkedListNode<Staff_Login> _Account_infor = staff_Login_List.Find(staff_Login);
                if (staff_Login.getPassword().CompareTo(_Account_infor.Value.getPassword()) == 0) //Is new node in left tree?  
                {
                    Console.WriteLine("Correct password input\nThank you!!");
                    return true;
                }
                else if(staff_Login.getPassword().CompareTo(_Account_infor.Value.getPassword()) != 0) //Is new node in right tree?
                {
                    Console.WriteLine("Incorrect password input\nThank you!!");
                }
                return false;
            }
            return false;
        }
        static public void Change_Password(Staff_Login temp_login)
        {
            
        }
    }
}