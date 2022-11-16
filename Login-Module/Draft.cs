//static public bool Checking_Password(Staff_Login temp_login, LinkedList<Staff_Login> temp)
//{
//    Staff_Login current = new Staff_Login();
//    if (temp_login.getUserName == null){
//        return false;
//    }
//    else{
//        current = temp.Find(temp_login);
//    }
//    return true;
//}
//static public void Encrypted()
//{
//    BinaryTree temp = new BinaryTree();
//    AesManaged aes = new AesManaged();
//    ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
//    MemoryStream ms = new MemoryStream();

//    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
//    {
//        // Create StreamWriter and write data to a stream    
//        using (StreamWriter sw = new StreamWriter(cs))
//            sw.Write(plainText);
//        encrypted = ms.ToArray();
//    }

//}

//static public LinkedList<Staff_Login> Decrypted_To_Linked_List()
//{

//    Staff_Login temp = new Staff_Login();
//    LinkedList<Staff_Login> list_Account = new LinkedList<Staff_Login>();

//    //var lines = File.ReadLines("Login.txt");
//    //foreach (var line in lines)
//    {
//        string[] Split_List_Item = line.Split('-');

//        // Convert string to the byte[] to decrypted it
//        byte[] byteArray_Username = Encoding.ASCII.GetBytes(Split_List_Item[0]);
//        byte[] byteArray_Password = Encoding.ASCII.GetBytes(Split_List_Item[1]);
//        byte[] byteArray_Key_Decrypted = Encoding.ASCII.GetBytes(Split_List_Item[2]);
//        byte[] byteArray_IV_Decrypted = Encoding.ASCII.GetBytes(Split_List_Item[3]);

//        //string username = Enccypted_Login.Decrypt(byteArray_Username, byteArray_Key_Decrypted, byteArray_IV_Decrypted);
//        //string password = Enccypted_Login.Decrypt(byteArray_Password, byteArray_Key_Decrypted, byteArray_IV_Decrypted);


//        //temp.setUserName(username);
//        //temp.setPassword(password);
//        //list_Account.AddFirst(temp);

//    }
//    return list_Account;
//}
//public static void Main()
//{
//    Console.WriteLine("Enter text that needs to be encrypted..");
//    string data = Console.ReadLine();
//    EncryptAesManaged(data);
//    Console.ReadLine();
//}
//static void EncryptAesManaged(string raw)
//{
//    try
//    {
//        // Create Aes that generates a new key and initialization vector (IV).    
//        // Same key must be used in encryption and decryption    
//        using (AesManaged aes = new AesManaged())
//        {
//            // Encrypt string    
//            byte[] encrypted = Encrypt(raw, aes.Key, aes.IV);
//            // Print encrypted string    
//            Console.WriteLine("Encrypted data: " + System.Text.Encoding.UTF8.GetString(encrypted));
//            // Decrypt the bytes to a string.    
//            string decrypted = Decrypt(encrypted, aes.Key, aes.IV);
//            // Print decrypted string. It should be same as raw data    
//            Console.WriteLine("Decrypted data: " + decrypted);
//        }
//    }
//    catch (Exception exp)
//    {
//        Console.WriteLine(exp.Message);
//    }
//    Console.ReadKey();
//}