using Login_Module;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Login_Module
{
    public class Features
    {
        static public void Login()
        {
            LinkedList<Staff_Login> list = new LinkedList<Staff_Login>();
            Staff_Login temp_login = new Staff_Login();

            Console.WriteLine("Welcome to The restaurant\n");
            Console.WriteLine("Please Choose The Option Below\n");
            Console.WriteLine("1. Login to Restaurant\n"); ;
            Console.WriteLine("2. Change password\n");
            Console.WriteLine("3. Return\n");
            Console.WriteLine("4. Exit\n");

            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 0:
                    string username;
                    string password;
                    Decrypted_To_Linked_List();
                    Console.WriteLine("***** Login *****\n");
                    Console.WriteLine("Username: ");
                    username = Console.ReadLine();
                    Console.WriteLine("Password: ");
                    password = Console.ReadLine();
                    temp_login.setUserName(username);
                    temp_login.setPassword(password);
                    Checking_Password(temp_login, list);
                    break;
                case 1:
                    Change_Password();
                    break;
                case 2:
                    break;
                case 3:
                    Program.menu();
                    break;
                case 4:
                    Console.WriteLine("Thank you for using the program\n");
                    break;
                default:
                    break;
            }
        }
        static public bool Checking_Password(Staff_Login temp_login, LinkedList<Staff_Login> temp)
        {
            if (temp_login.getUserName == null){
                return false;
            }
            else{
                current = temp.Find(temp_login.getUserName());
            }
            return true;
        }
        static public void Encrypted()
        {
            AesManaged aes = new AesManaged();
            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            MemoryStream ms = new MemoryStream();

            using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            {
                // Create StreamWriter and write data to a stream    
                using (StreamWriter sw = new StreamWriter(cs))
                    sw.Write(plainText);
                encrypted = ms.ToArray();
            }

        }

        static public LinkedList<Staff_Login> Decrypted_To_Linked_List()
        {

            Staff_Login temp = new Staff_Login();
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


        static public void Change_Password()
        {

        }
    }
}