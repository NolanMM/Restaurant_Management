using Login_Module;
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
                    Change_Forgot_Password();
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
            if (username.CompareTo("Return") == 0) { Login(); }

            Console.WriteLine("Password: ");
            password = Console.ReadLine();

            // Check if the user want to exit the program
            if (password.CompareTo("Exit") == 0) { return; }
            if (username.CompareTo("Return") == 0) { Login(); }

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
        static public bool Send_Verify_Code(string randomCode,string email_to)
        {
            /* @ Create new variable to hold the sender email, password of the sender, and the message is the code */
            String from, pass, messageBody;

            /* @ Input the information of Sender */
            from = "group4sendblackmailtou@gmail.com";
            pass = "bbsmmmsnfhealzte";
            messageBody = " your reset code is " + randomCode;

            /* @ Generate new email to send to the receiver */
            MailMessage email = new MailMessage();
            /* @ Input the information of the Receiver and the information for the email components*/
            email.From = new MailAddress(from);
            email.To.Add(email_to);
            email.Body = messageBody;
            email.Subject = "Password Reseting Code";

            /* @ Generate smtp server to send the verify email */
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            SmtpServer.EnableSsl = true;
            SmtpServer.Port = 587;
            SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
            /* @ Checking the app password of the google email and the email of the sender */
            SmtpServer.Credentials = new NetworkCredential(from, pass);

            try
            {
                SmtpServer.Send(email);
                //Console.WriteLine("Email Successfully Sent");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        static public bool Checking_Verify_code(string randomCode)
        {
            /* @ Checking the verify code 3 times or 
             * @ Else logout and start from beginning due to the security 
             */
            int count = 0;
            while (count >= 0)
            {
                if(count == 2)
                {
                    count = -1;
                    Console.WriteLine("You input wrong too many times. You will be transfer to login...\n");
                    break;
                }
                Console.WriteLine("Please enter the verify code be sent to your email\n");
                Console.WriteLine("Or enter Exit to Exit, Return to Return\n");
                string verify_code_input = Console.ReadLine();
                if (verify_code_input.CompareTo("Return") == 0)
                {
                    // set the flag to -1 value to escapse while loop
                    Login();
                    count = -1;
                }
                if (verify_code_input.CompareTo("Exit") == 0)
                {
                    // set the flag to -1 value to escapse while loop
                    Login();
                    count = -1;
                }
                if(verify_code_input.CompareTo(randomCode) == 0)
                {
                    Console.WriteLine("Change password failed\n");
                    return true;
                }
                else {int times_input_left = 2-count; Console.WriteLine("Wrong Verify code, u have only"+ times_input_left.ToString());count++; }
            }
            return false;
        }
        static public bool Change_Forgot_Password()
        {
            /* @Take the username from staff forgot password */
            Staff_Login temp_login = new Staff_Login();
            Console.WriteLine("Please enter your username \n");
            string username = Console.ReadLine();
            temp_login.setUserName(username);

            /* @ Create new random variable to automatic generate new verify code when it needed */
            Random rand = new Random();
            string randomCode = rand.Next(999999).ToString();
            
            LinkedList<Staff_Login> list = Create_Read_Account_List();

            Staff_Login find_item = Find_Item_by_username_Return_Node(temp_login,list);

            Console.WriteLine("...\n");
            Send_Verify_Code(randomCode,find_item.getRecovery());
            bool verify_result = Checking_Verify_code(randomCode);

            if (verify_result == true)
            {
                Console.WriteLine("Enter new password you want to change\n");
                string new_password = Console.ReadLine();

                // set new password to the node in list
                find_item.setPassword(new_password);

                /* @Write the new information to the file */
                bool flag_file = Write_To_File(list);
                if (flag_file == true)
                {
                    Console.WriteLine("Successfully changing the password.\n" +
                    "The Program will return soon\n");
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else {
                Console.WriteLine("Change password failed\n");
                return false;
            }
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
            Console.WriteLine("6. Change the Password\n");
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
                byte[] byteArray_Email_Recovery_Decrypted = Encoding.ASCII.GetBytes(Split_List_Item[4]);

                string username = Enccypted_Login.Decrypt(byteArray_Username, byteArray_Key_Decrypted, byteArray_IV_Decrypted);
                string password = Enccypted_Login.Decrypt(byteArray_Password, byteArray_Key_Decrypted, byteArray_IV_Decrypted);
                string email_recovery = Enccypted_Login.Decrypt(byteArray_Email_Recovery_Decrypted, byteArray_Key_Decrypted, byteArray_IV_Decrypted);

                temp.setUserName(username);
                temp.setPassword(password);
                temp.setRecovery(email_recovery);

                list_Account.AddFirst(temp);
            }
            return list_Account;
        }
        static Staff_Login Find_Item_by_username_Return_Node(Staff_Login staff_Login, LinkedList<Staff_Login> staff_Login_List)
        {
            Staff_Login find_item = new Staff_Login();
            Staff_Login[] staff_login_list_array = staff_Login_List.ToArray();
            int flag = 0;
            int count = staff_Login_List.Count();

            foreach (Staff_Login item in staff_login_list_array)
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
        static public bool Checking_Password(LinkedList<Staff_Login> staff_Login_List, Staff_Login staff_Login)
        {
            Staff_Login find_item = Find_Item_by_username_Return_Node(staff_Login, staff_Login_List);

            if (staff_Login.getPassword().CompareTo(find_item.getPassword()) == 0) {
                Console.WriteLine("Correct password input\nThank you!!");
                return true;
            }
            else if (staff_Login.getPassword().CompareTo(find_item.getPassword()) != 0) {
                Console.WriteLine("Incorrect password input\nThank you!!");
                return false;
            }
            return true;
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
            //LinkedListNode<Staff_Login> node_change_password = list_account.Find(temp_login);
            Staff_Login node_change_password = Find_Item_by_username_Return_Node(temp_login, list_account);
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
                    // Enter the program again from beginning
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

                if (old_password.CompareTo("Exit") == 0)
                {
                    // set the flag to -1 value to escapse while loop
                    menu_Login_Staff(temp_login, list_account);
                    flag = -1;
                }

                /* @Check the old password
                * @if true allow to change the password
                * @if false ask for input again or exit */

                if (old_password.CompareTo(node_change_password.getPassword()) == 0) {
                    Console.WriteLine("...\n");
                    Console.WriteLine("Enter new password you want to change\n");
                    string new_password = Console.ReadLine();

                    // set new password to the node in list
                    node_change_password.setPassword(new_password);

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

            /*@ Check the number of items inside the list*/
            if (list_account.Count() == 0){
                Console.WriteLine("The list is empty cannot write to file\n");
                return false;
            }

            if (File.Exists(filename)){
                /* Delete the file if it exsist to update new information whenever write to file again */
                File.Delete(filename);
                File.Create(filename);
            }
            else if (!File.Exists(filename))
            {
                File.Create(filename);
            }

            /*@ Loop through each item in the list */
            foreach (Staff_Login staff in list_account)
            {
                using (AesManaged aes = new AesManaged())
                {
                    // Encrypt string
                    byte[] username = Enccypted_Login.Encrypt(staff.getPassword(), aes.Key, aes.IV);
                    byte[] password = Enccypted_Login.Encrypt(staff.getUserName(), aes.Key, aes.IV);
                    byte[] key = aes.Key;
                    byte[] iv = aes.IV;
                    byte[] recovery_email = Enccypted_Login.Encrypt(staff.getRecovery(), aes.Key, aes.IV);

                    // Write encrypted string    
                    string encypted_Username = System.Text.Encoding.UTF8.GetString(username);
                    string encypted_Password = System.Text.Encoding.UTF8.GetString(password);
                    string encypted_key = System.Text.Encoding.UTF8.GetString(key);
                    string encypted_iv = System.Text.Encoding.UTF8.GetString(iv);
                    string encypted_Recovery_Email = System.Text.Encoding.UTF8.GetString(recovery_email);

                    /* @string stored in the file need to in layout 
                     * @encypted_Username-encypted_Password-encypted_key-encypted_iv-emaill_recovery
                     */

                    string Write_to_File_Format = encypted_Username + "-" + encypted_Password + "-"
                        + encypted_key + "-" + encypted_iv + "-" + encypted_Recovery_Email;
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
            string recovery_email;

            Console.WriteLine("Enter the number of Staffs in the company you want to assign\n");
            int number_of_Staffs = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < number_of_Staffs; i++)
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

                Console.WriteLine("Email: ");
                recovery_email = Console.ReadLine();
                if (password.CompareTo("Exit") == 0) { return; }

                temp.setUserName(username);
                temp.setPassword(password);
                temp.setRecovery(recovery_email);
                list_Account.AddFirst(temp);
            }
            /* @ Call the function to encypted and write to the file the information
             * @ in string format */
            bool flag = Write_To_File(list_Account);

            /* @Checking the flag and throw the message */
            if (flag == true) { Console.WriteLine("Write to file successfully\n"); }
            else { Console.WriteLine("Write the list of the staffs false\n"); }
        }
    }
}