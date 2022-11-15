using Login_Module;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Diagnostics.Metrics;
using System.IO;
using System.Runtime.InteropServices;
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
                menu_Login_Staff(temp_login, list);
            }
            else
            {
                Login_Restaurant();
            }
        }
        static public void Forgot_Password()
        {

        }
        static public void menu_Login_Staff(Staff_Login temp_login, LinkedList<Staff_Login> list)
        {
            // Print out the Options for the Staff
            Console.WriteLine("Welcome to Restaurant\n");
            Console.WriteLine("1. Check the Menu\n");
            Console.WriteLine("2. Check the inventory\n");
            Console.WriteLine("3. Check the security\n");
            Console.WriteLine("4. Check the salary\n");
            Console.WriteLine("5. Check the time table\n");
            Console.WriteLine("7. Change the Password\n");
            Console.WriteLine("8. Logout\n");
            Console.WriteLine("9. Exit\n");

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
                    Change_Password(temp_login, list);
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
                else if (staff_Login.getPassword().CompareTo(_Account_infor.Value.getPassword()) != 0) //Is new node in right tree?
                {
                    Console.WriteLine("Incorrect password input\nThank you!!");
                }
                return false;
            }
            return false;
        }
        static public void Change_Password(Staff_Login temp_login, LinkedList<Staff_Login> list_account)
        {
            // Create a list account
            // = Create_Read_Account_List();
            if (list_account.Count == 0)
            {
                Console.WriteLine("List empty!!. Exit\n");
                return;
            }
            // Find the node inside the linked list to change the passwords
            LinkedListNode<Staff_Login> node_change_password = list_account.Find(temp_login);

            int flag = 0;

            
             /* @ Using the while loop and flag method
             *  @ to take and check the input of old password from user
             *  @ if old password is correct return true and break the loop
             *  @ if old password is incorrect will ask for input again
             *  @ if user want to return enter "Return"
             *  @ if user want to exit the program enter "Exit"
             */
            while (flag >=0)
            {
                if (flag > 3)
                {
                    Console.WriteLine("You enter wrong old password too many times\n");
                    Console.WriteLine("Systems will be logout in few seconds for the security\n");
                    list_account.Clear();
                    temp_login = null;
                    flag = -1;
                    // Enter the program again
                    Program.menu();
                }

                Console.WriteLine("Please enter the old password to confirm change password\n");
                string old_password = Console.ReadLine();

                /* @ if user want to return enter "Return"
                *  @ if user want to exit the program enter "Exit" */
                if (old_password.CompareTo("Return") == 0)
                    {
                        // set the flag to -1 value to escapse while loop
                        menu_Login_Staff(temp_login, list_account);
                        flag = -1;
                    }
        
                /* @Check the old password
                * @if true allow to change the password
                * @if false ask for input again or exit */

                if (old_password.CompareTo(node_change_password.Value.getPassword()) == 0) {
                    Console.WriteLine("...\n");
                    Console.WriteLine("Enter new password you want to change\n");
                    string new_password = Console.ReadLine();

                    // set new password to the node in list
                    node_change_password.Value.setPassword(new_password);

                    /* @Write the new information to the file */
                    bool flag_file = Write_To_File(list_account);
                    if(flag_file == true){
                        Console.WriteLine("Successfully changing the password.\n" +
                        "The Program will return soon\n");
                    }
                    else{
                        Console.WriteLine("Error with the write to file\n");
                    }

                    /* change the flag to escapce the loop */
                    flag = -1;
                }
                else{
                    Console.WriteLine("Wrong old password please enter again. ");
                    flag++;
                }

                // return to the menu
                menu_Login_Staff(temp_login, list_account);
            }
        }
        static bool Write_To_File(LinkedList<Staff_Login> list_account)
        {
            /*@ Assign the file */
            string filename = "Login.txt";

            if(list_account.Count() == 0){
                Console.WriteLine("The list is empty cannot write to file\n");
                return false;
            }

            if (File.Exists(filename)){ 
                
                /* Delete the file if it exsist to update new information whenever write to file again */
                File.Delete(filename);
                File.Create(filename);
            }
            else if(!File.Exists(filename)){
                File.Create(filename);
            }

            foreach (Staff_Login staff in list_account)
            {
                using (AesManaged aes = new AesManaged())
                {
                    // Encrypt string
                    byte[] username = Enccypted_Login.Encrypt(staff.getPassword(), aes.Key, aes.IV);
                    byte[] password = Enccypted_Login.Encrypt(staff.getUserName(), aes.Key, aes.IV);
                    byte[] key = aes.Key;
                    byte[] iv = aes.IV;

                    // Write encrypted string    
                    string encypted_Username = System.Text.Encoding.UTF8.GetString(username);
                    string encypted_Password = System.Text.Encoding.UTF8.GetString(password);
                    string encypted_key = System.Text.Encoding.UTF8.GetString(key);
                    string encypted_iv = System.Text.Encoding.UTF8.GetString(iv);

                    /* @string stored in the file need to in layout 
                     * @encypted_Username-encypted_Password-encypted_key-encypted_iv
                     */

                    string Write_to_File_Format = encypted_Username + "-" + encypted_Password + "-"
                        + encypted_key + "-" + encypted_iv;
                    File.WriteAllText(Write_to_File_Format, filename);
                }
            }
            return true;
        }
        static void Create_Write_New_Staff_List()
        {
            Staff_Login temp = new Staff_Login();
            LinkedList<Staff_Login> list_Account = new LinkedList<Staff_Login>();

            string username;
            string password;

            Console.WriteLine("Enter the number of Staffs in the company you want to assign\n");
            int number_of_Staffs = Convert.ToInt32(Console.ReadLine());

            for(int i = 0; i < number_of_Staffs; i++)
            {
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

                temp.setUserName(username);
                temp.setPassword(password);
                list_Account.AddFirst(temp);
            }
            /* @ Call the function to encypted and write to the file the information
             * @ in string format */
            bool flag = Write_To_File(list_Account);

            /* @Checking the flag and throw the message */
            if (flag == true) Console.WriteLine("Write to file successfully\n");
            else Console.WriteLine("Write the list of the staffs false\n");
        }
    }
}