using System;
using System.Net.Mail;
using System.Net;

namespace Assemble___Restaurant_System
{
    public class Forgot_Password
    {
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

            LinkedList<Staff_Login> list = Login_Functions.Create_Read_Account_Login_List();

            Staff_Login find_item = Login_Functions.Find_Item_by_username_Return_Node(temp_login, list);

            Console.WriteLine("...\n");
            Send_Verify_Code(randomCode, find_item.getRecovery());
            bool verify_result = Checking_Verify_code(randomCode);

            if (verify_result == true)
            {
                Console.WriteLine("Enter new password you want to change\n");
                string new_password = Console.ReadLine();

                // set new password to the node in list
                find_item.setPassword(new_password);

                /* @Write the new information to the file */
                bool flag_file = Login_Functions.Write_To_File(list);
                if (flag_file == true)
                {
                    Console.WriteLine("Successfully changing the password.\n" +
                    "The Program will return soon\n");
                    return true;
                }
                else {
                    Console.WriteLine("Change password failed, cannot write to file the new information\n");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Change password failed\n");
                return false;
            }
        }

        static public bool Send_Verify_Code(string randomCode, string email_to)
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
                if (count == 2)
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
                    Menu.Login_Menu();
                    count = -1;
                }
                if (verify_code_input.CompareTo("Exit") == 0)
                {
                    // set the flag to -1 value to escapse while loop
                    Menu.Login_Menu();
                    count = -1;
                }
                if (verify_code_input.CompareTo(randomCode) == 0)
                {
                    Console.WriteLine("Change password failed\n");
                    return true;
                }
                else { int times_input_left = 2 - count; Console.WriteLine("Wrong Verify code, u have only" + times_input_left.ToString()); count++; }
            }
            return false;
        }

    }
}